using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ScratchCardController : MonoBehaviour
{
    public Image Brush; // 스크래치에 사용되는 브러시 이미지를 위한 참조 변수.

    public RectTransform ScratchArea; // 스크래치 영역을 나타내는 RectTransform 변수.
    public RenderTexture CoverRenderTexture; // 스크래치 영역을 덮는 RenderTexture 변수.

    private Texture2D tempMaskTexture; // 스크래치 영역의 마스크를 임시로 저장하는 Texture2D 변수.
    private bool isTouched; // 화면 터치 여부를 확인하는 플래그 변수.

     private int Count; // 스크래치한 픽셀의 수를 카운트하는 변수.

    private bool[,] matrix; // 스크래치한 픽셀을 추적하기 위한 2D 배열.

    // public Text progressText; // 스크래치 진행 상태를 표시하는 Text 컴포넌트의 참조 변수.
    private bool _isprogressTextNull; // progressText가 null인지 확인하는 플래그 변수.

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject puzzleCamera;

    public UnityEvent scratchEndEvent; // 스크래치 완료 시 트리거되는 UnityEvent.

    public int targetBrushCount; // 몇번의 브러쉬를 찍으면의 조건
    [SerializeField] private int brushCount;
   // [SerializeField] private  Texture2D mouseCurser_Brush;
    //private  Texture2D mouseCurser_Original;

    [SerializeField] private Vector2 currentScreenPoint; // 현재 마우스의 위치를 저장
    [SerializeField] private float brushDelayTime = 0.0f;
    // 텍스처를 복제하여 새로운 Texture2D를 생성하는 도우미 메서드.

    // 롤러 이미지가 마우스를 따라다니게
    [SerializeField] private Image rollerImage;


    Texture2D duplicateTexture(Texture source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
            source.width,
            source.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;

        Texture2D readableText = new Texture2D(source.width, source.height);
        readableText.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        readableText.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);
        return readableText;
    }

    // RenderTexture를 Texture2D로 변환하는 도우미 메서드.

    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(rTex.width, rTex.height, TextureFormat.RGB24, false);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    private void Start()
    {
        brushCount = 0;
        brushDelayTime = 0.0f;

        //mouseCurser_Brush = Resources.Load<Texture2D>("Cursor/RollerCursor");
        //mouseCurser_Original = Resources.Load<Texture2D>("Cursor/OriginalCursor");

        // CoverRenderTexture의 크기를 ScratchArea의 크기와 일치하도록 설정합니다.
        CoverRenderTexture.width = Mathf.RoundToInt(ScratchArea.rect.width) / 2;
        CoverRenderTexture.height = Mathf.RoundToInt(ScratchArea.rect.height) / 2;

        // CanvasScaler의 참조 해상도를 화면 크기와 일치하도록 설정합니다.
        GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

        // 스크래치한 픽셀을 추적하기 위한 matrix 배열을 초기화합니다.
        matrix = new bool[CoverRenderTexture.width, CoverRenderTexture.height];
    }

    private void Update()
    {
        if(brushCount >= targetBrushCount)
        {
            scratchEndEvent.Invoke();
        }

        //if(isTouched /*|| Input.GetMouseButtonDown(0)*/)
        //{
            UpdateRollerImagePosition();
        //}

    }

    private void OnEnable()
    {
        // 객체가 활성화될 때 YieldScratching 코루틴을 시작합니다.
        StartCoroutine(YieldScratching());
        mainCamera.SetActive(false);
        // Cursor.SetCursor(mouseCurser_Brush, Vector2.zero, CursorMode.Auto);
        Cursor.visible= false;
    }

    private void OnDisable()
    {
        // 객체가 비활성화될 때 YieldScratching 코루틴을 정지합니다.
        StopCoroutine(YieldScratching());
        mainCamera.SetActive(true);
        // Cursor.SetCursor(mouseCurser_Original, Vector2.zero, CursorMode.Auto);
        Cursor.visible = true;
    }
    private void UpdateRollerImagePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = rollerImage.transform.position.z - Camera.main.transform.position.z;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        worldPos.y = worldPos.y - 1.2f; // Adjust the roller image position
        worldPos.z = rollerImage.transform.position.z; // Keep the original Z position
        rollerImage.transform.position = worldPos;
    }
    // 스크래치 과정을 처리하는 코루틴.   
    IEnumerator YieldScratching()
    {

        while (true)
        {
            if (isTouched)
            {
                // 화면이 터치되면 스크래치 과정을 계속합니다.
                brushDelayTime -= Time.deltaTime;
                yield return YieldInstantiateScratch();
            }

            if (Input.GetMouseButton(0))
            {
                if (!isTouched)
                {
                    // 터치가 시작되면 isTouched 플래그를 true로 설정하고 스크래치를 시작합니다.
                    isTouched = true;
                    Debug.Log("Touch down");

                    yield return YieldInstantiateScratch();
                }
            }
            else
            {
                // 터치가 끝나면 isTouched 플래그를 false로 설정합니다.
                isTouched = false;
            }

            // 다음 프레임까지 대기합니다.
            yield return null;
        }
    }

    // 브러시를 생성하고 스크래치한 픽셀을 체크하는 코루틴.
    IEnumerator YieldInstantiateScratch()
    {
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (currentScreenPoint != screenPos)
        {
            // 브러시를 화면 터치 위치에 생성합니다.
            yield return Instantiate(Brush, screenPos, Quaternion.identity, transform);

            // 스크래치한 픽셀을 체크합니다.
            yield return YieldCheckPixelColor();

            if(brushDelayTime <= 0)
            {
                // 브러쉬의 카운트 증가합니다. 
                brushCount += 1;
                brushDelayTime = 0.1f;
            }
        }

        currentScreenPoint = screenPos;
        yield return currentScreenPoint;
    }

    // CoverRenderTexture의 픽셀 색상을 체크하는 코루틴.
    IEnumerator YieldCheckPixelColor()
    {
        // 픽셀 색상 체크를 위해 RenderTexture를 Texture2D로 변환합니다.
        yield return tempMaskTexture = toTexture2D(CoverRenderTexture);

        // 각 픽셀을 순회하면서 스크래치 여부를 체크합니다.
        for (int x = 0; x < tempMaskTexture.width; x++)
        {
            for (int y = 0; y < tempMaskTexture.height; y++)
            {
                if (!matrix[x, y])
                {
                    // 아직 스크래치되지 않은 픽셀인 경우, 투명한지(스크래치된 것인지) 체크합니다.
                    if (tempMaskTexture.GetPixel(x, y).r > 0f &&
                        tempMaskTexture.GetPixel(x, y).g > 0f &&
                        tempMaskTexture.GetPixel(x, y).b > 0f &&
                        tempMaskTexture.GetPixel(x, y).a > 0f)
                    {
                        // 투명하다면 Count를 증가시키고 해당 픽셀을 스크래치된 것으로 표시합니다.
                        Count++;
                        matrix[x, y] = true;
                    }
                }
            }
        }

        // 다음 프레임까지 대기합니다.
        yield return null;
    }

    private void OnGUI()
    {
        //GUIStyle style = new GUIStyle()
        //{ fontSize = 20, fontStyle = FontStyle.Bold, normal = new GUIStyleState() { textColor = Color.black } };

        //float percent = calculatePercent();
        //
        //        GUILayout.Label($"總共刮了 {(percent * 100).ToString("#0.00")} %", style);
    }

    // 스크래치 영역의 백분율을 계산합니다.
    //private float calculatePercent()
    //{
    //    return (float)Count / (CoverRenderTexture.width * CoverRenderTexture.height);
    //}


}