using UnityEngine;
using UnityEngine.Events;

public class ImageMask : MonoBehaviour
{
    public Texture2D maskTexture;  // 마스크로 사용할 이미지 텍스처
    public UnityEvent onStartMasking;  // 마스킹 시작시 호출할 이벤트
    public UnityEvent onEndMasking;  // 마스킹 종료시 호출할 이벤트

    private Material maskMaterial;  // 마스크를 적용할 재질
    private RenderTexture maskRenderTexture;  // 마스크를 그릴 렌더 텍스처
    private bool isMasking = false;  // 마스킹 중인지 여부
    private Vector2 previousMousePosition;  // 이전 마우스 위치

    private void Start()
    {
        // 마스크를 적용할 재질 생성
        maskMaterial = new Material(Shader.Find("Unlit/Transparent"));

        // 마스크 텍스처를 재질에 할당
        maskMaterial.SetTexture("_MainTex", maskTexture);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 마스킹 시작
            StartMasking();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // 마스킹 종료
            EndMasking();
        }

        // 마스킹 중일 때, 마우스 움직임을 감지하여 마스크를 업데이트합니다.
        if (isMasking)
        {
            Vector2 currentMousePosition = Input.mousePosition;
            UpdateMask(currentMousePosition);
            previousMousePosition = currentMousePosition;
        }
    }

    private void StartMasking()
    {
        if (!isMasking)
        {
            isMasking = true;
            onStartMasking.Invoke();
            maskRenderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            maskRenderTexture.Create();
            maskMaterial.SetTexture("_MaskTex", maskRenderTexture);
            Graphics.Blit(maskTexture, maskRenderTexture, maskMaterial);
            previousMousePosition = Input.mousePosition;
        }
    }

    private void EndMasking()
    {
        if (isMasking)
        {
            isMasking = false;
            onEndMasking.Invoke();
            Destroy(maskRenderTexture);
            maskMaterial.SetTexture("_MaskTex", null);
        }
    }

    private void UpdateMask(Vector2 currentMousePosition)
    {
        Vector2 deltaMousePosition = currentMousePosition - previousMousePosition;
        Vector2 normalizedMousePosition = new Vector2(currentMousePosition.x / Screen.width, currentMousePosition.y / Screen.height);
        maskMaterial.SetVector("_MaskPos", normalizedMousePosition);
        maskMaterial.SetVector("_MaskSize", deltaMousePosition);
        Graphics.Blit(maskTexture, maskRenderTexture, maskMaterial);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
    }
}
