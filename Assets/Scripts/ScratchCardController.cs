using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScratchCardEvent : UnityEvent
{
}

public class ScratchCardEvent<T> : UnityEvent<T>
{
}

public class ScratchCardController : MonoBehaviour
{
    public ScratchCardEvent<float> onScratching { get; set; } = new ScratchCardEvent<float>();

    public Image Brush;

    public RectTransform ScratchArea;
    public RenderTexture CoverRenderTexture;

    private Texture2D tempMaskTexture;
    private bool isTouched;

    private int Count;

    private bool[,] matrix;

    public Text progressText;
    private bool _isprogressTextNull;

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
        _isprogressTextNull = null == progressText;
        CoverRenderTexture.width = Mathf.RoundToInt(ScratchArea.rect.width) / 2;
        CoverRenderTexture.height = Mathf.RoundToInt(ScratchArea.rect.height) / 2;
        GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);
        matrix = new bool[CoverRenderTexture.width, CoverRenderTexture.height];
    }

    private void Update()
    {
        if (_isprogressTextNull)
        {
            return;
        }

        float percent = calculatePercent();

        progressText.text = $"퍼센트 {(percent * 100):#0.00} %";
    }

    private void OnEnable()
    {
        StartCoroutine(YieldScratching());
    }

    private void OnDisable()
    {
        StopCoroutine(YieldScratching());
    }

    IEnumerator YieldScratching()
    {
        while (true)
        {
            if (isTouched)
            {
                yield return YieldInstantiateScratch();
            }

            if (Input.GetMouseButton(0))
            {
                if (!isTouched)
                {
                    isTouched = true;
                    Debug.Log("Touch down");
                    yield return YieldInstantiateScratch();
                }
            }
            else
            {
                isTouched = false;
            }

            yield return null;
        }
    }

    IEnumerator YieldInstantiateScratch()
    {
        Vector2 screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        yield return Instantiate(Brush, screenPos, Quaternion.identity, transform);

        yield return YieldCheckPixelColor();
    }

    IEnumerator YieldCheckPixelColor()
    {
        yield return tempMaskTexture = toTexture2D(CoverRenderTexture);

        for (int x = 0; x < tempMaskTexture.width; x++)
        {
            for (int y = 0; y < tempMaskTexture.height; y++)
            {
                if (!matrix[x, y])
                {
                    if (tempMaskTexture.GetPixel(x, y).r > 0f &&
                        tempMaskTexture.GetPixel(x, y).g > 0f &&
                        tempMaskTexture.GetPixel(x, y).b > 0f &&
                        tempMaskTexture.GetPixel(x, y).a > 0f)
                    {
                        Count++;
                        matrix[x, y] = true;
                    }
                }
            }
        }

        yield return null;
    }

    private void OnGUI()
    {
//        GUIStyle style = new GUIStyle()
//            {fontSize = 20, fontStyle = FontStyle.Bold, normal = new GUIStyleState() {textColor = Color.black}};
//
//        float percent = calculatePercent();
//
//        GUILayout.Label($"總共刮了 {(percent * 100).ToString("#0.00")} %", style);
    }

    private float calculatePercent()
    {
        return (float) Count / (CoverRenderTexture.width * CoverRenderTexture.height);
    }
}