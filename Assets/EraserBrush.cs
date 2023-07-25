using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EraserBrush : MonoBehaviour, IPointerClickHandler
{
    public RawImage rawImage; // RawImage를 사용합니다.
    public float eraserBrushSize = 40f;
    private bool isImageErased = false;

    private Texture2D originalTexture; // 기존 텍스처를 저장하는 변수

    private void Awake()
    {
        if (rawImage == null)
        {
            rawImage = GetComponent<RawImage>();
        }
    }

    // 기존 텍스처를 복사하여 작업 텍스처를 초기화합니다.
    private void InitTexture()
    {
        originalTexture = (Texture2D)rawImage.texture;
        Texture2D workingTexture = new Texture2D(originalTexture.width, originalTexture.height);
        workingTexture.SetPixels(originalTexture.GetPixels());
        workingTexture.Apply();

        rawImage.texture = workingTexture;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Eraser(eventData.position);
    }

    private void Eraser(Vector2 clickPosition)
    {
        if (isImageErased)
            return;

        // 작업 텍스처를 초기화합니다.
        if (originalTexture == null || rawImage.texture == null)
            InitTexture();

        Texture2D workingTexture = (Texture2D)rawImage.texture;

        // 클릭한 지점을 UV 좌표로 변환합니다.
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rawImage.rectTransform, clickPosition, null, out localPoint);
        Vector2 normalizedPoint = new Vector2((localPoint.x + rawImage.rectTransform.pivot.x * rawImage.rectTransform.rect.width) / rawImage.rectTransform.rect.width,
                                               (localPoint.y + rawImage.rectTransform.pivot.y * rawImage.rectTransform.rect.height) / rawImage.rectTransform.rect.height);

        // 클릭한 지점을 중심으로 지우개 크기를 적용합니다.
        int centerX = Mathf.FloorToInt(normalizedPoint.x * workingTexture.width);
        int centerY = Mathf.FloorToInt(normalizedPoint.y * workingTexture.height);

        int halfEraserSize = Mathf.FloorToInt(eraserBrushSize / 2f);

        // 픽셀을 순회하면서 클릭한 지점과 지우개 크기 내의 픽셀을 투명하게 만듭니다.
        for (int y = centerY - halfEraserSize; y < centerY + halfEraserSize; y++)
        {
            for (int x = centerX - halfEraserSize; x < centerX + halfEraserSize; x++)
            {
                if (x >= 0 && x < workingTexture.width && y >= 0 && y < workingTexture.height)
                {
                    workingTexture.SetPixel(x, y, Color.clear);
                }
            }
        }

        workingTexture.Apply();

        isImageErased = true;
    }
}
