using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ImageAlphaController : MonoBehaviour, IDragHandler
{
    public float alphaDecreaseSpeed = 0.1f; // 알파값을 감소시키는 속도
    public float minAlphaPercentage = 0.2f; // 알파값이 0인 픽셀의 비율이 이 값 이상이면 게임 종료
    public int eraseRange = 5; // 마우스 드래그로 알파값을 조절할 범위

    private Image image;
    private Texture2D texture;
    private Color32[] originalPixels;
    private bool isGameRunning = true;

    private void Awake()
    {
        image = GetComponent<Image>();
        texture = (Texture2D)image.mainTexture;
        originalPixels = texture.GetPixels32();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isGameRunning)
            return;

        // 드래그한 마우스의 위치에서 픽셀 좌표 계산
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            image.rectTransform, eventData.position, null, out Vector2 localPoint);

        // 이미지의 텍스처 공간에서의 픽셀 좌표 계산
        float normalizedX = (localPoint.x + image.rectTransform.rect.width * 0.5f) / image.rectTransform.rect.width;
        float normalizedY = (localPoint.y + image.rectTransform.rect.height * 0.5f) / image.rectTransform.rect.height;
        int pixelX = Mathf.FloorToInt(normalizedX * texture.width);
        int pixelY = Mathf.FloorToInt(normalizedY * texture.height);

        // 알파값을 조절할 범위 계산
        int startX = Mathf.Clamp(pixelX - eraseRange, 0, texture.width - 1);
        int endX = Mathf.Clamp(pixelX + eraseRange, 0, texture.width - 1);
        int startY = Mathf.Clamp(pixelY - eraseRange, 0, texture.height - 1);
        int endY = Mathf.Clamp(pixelY + eraseRange, 0, texture.height - 1);

        // 일정 범위 내에서 알파값 감소
        for (int x = startX; x <= endX; x++)
        {
            for (int y = startY; y <= endY; y++)
            {
                int index = y * texture.width + x;
                Color32 pixel = texture.GetPixel(x, y);
                pixel.a = (byte)Mathf.Max(pixel.a - Mathf.FloorToInt(alphaDecreaseSpeed * 255), 0);
                texture.SetPixel(x, y, pixel);
            }
        }

        texture.Apply();

        // 알파값이 0인 픽셀의 비율 검사
        float totalPixels = texture.width * texture.height;
        float transparentPixels = 0f;
        foreach (Color32 pixel in texture.GetPixels32())
        {
            if (pixel.a == 0)
                transparentPixels++;
        }

        float alphaPercentage = transparentPixels / totalPixels;
        if (alphaPercentage >= minAlphaPercentage)
        {
            // 게임 종료 로직 추가
            Debug.Log("Alpha threshold reached. Game over!");
            isGameRunning = false;
        }
    }

    public void ResetAlpha()
    {
        texture.SetPixels32(originalPixels);
        texture.Apply();
        isGameRunning = true;
    }

    private void OnDisable()
    {
        ResetAlpha();
    }
}
