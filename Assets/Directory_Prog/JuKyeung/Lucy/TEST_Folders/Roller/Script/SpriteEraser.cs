using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpriteEraser : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Image spriteImage; // 스프라이트 이미지
    public Color eraserColor = new Color(1f, 1f, 1f, 0f); // 지우개로 지워지는 부분의 색상 (투명)
    public float eraserSize = 50f; // 지우개의 크기

    private Texture2D originalTexture; // 원래의 텍스처 저장
    private Sprite originalSprite; // 원래의 스프라이트 저장

    private void Awake()
    {
        // 게임 시작 시 원래의 텍스처와 스프라이트를 저장
        originalTexture = Instantiate(spriteImage.sprite.texture);
        originalSprite = spriteImage.sprite;
    }

    private void OnDestroy()
    {
        // 게임 종료 시 원래의 텍스처와 스프라이트로 복구
        spriteImage.sprite = originalSprite;
        Destroy(originalTexture);
    }

    private void EraseSprite(Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(spriteImage.rectTransform, position, null, out Vector2 localPos);

        // 스프라이트를 지울 범위 계산
        float normalizedX = (localPos.x + spriteImage.rectTransform.rect.width / 2) / spriteImage.rectTransform.rect.width;
        float normalizedY = (localPos.y + spriteImage.rectTransform.rect.height / 2) / spriteImage.rectTransform.rect.height;
        float offsetX = normalizedX * spriteImage.sprite.texture.width;
        float offsetY = normalizedY * spriteImage.sprite.texture.height;
        float halfSize = eraserSize / 2f;
        Rect eraserRect = new Rect(offsetX - halfSize, offsetY - halfSize, eraserSize, eraserSize);

        // 스프라이트 이미지의 Texture2D 가져오기
        Texture2D spriteTexture = spriteImage.sprite.texture;

        // 복사된 원래의 텍스처를 수정할 텍스처로 설정
        spriteTexture.SetPixels(originalTexture.GetPixels());
        spriteTexture.Apply();

        // 픽셀 색상 변경
        Color[] colors = spriteTexture.GetPixels();
        for (int x = 0; x < spriteTexture.width; x++)
        {
            for (int y = 0; y < spriteTexture.height; y++)
            {
                if (eraserRect.Contains(new Vector2(x, y)))
                {
                    float distanceX = Mathf.Abs(x - offsetX);
                    float distanceY = Mathf.Abs(y - offsetY);
                    float distance = Mathf.Sqrt(distanceX * distanceX + distanceY * distanceY);
                    float alpha = Mathf.Clamp01(1f - (distance / halfSize));
                    Color color = colors[y * spriteTexture.width + x];
                    colors[y * spriteTexture.width + x] = Color.Lerp(color, eraserColor, alpha);
                }
            }
        }

        // 변경된 픽셀 색상을 Texture2D에 적용
        spriteTexture.SetPixels(colors);
        spriteTexture.Apply();

        // 스프라이트 이미지 업데이트
        spriteImage.sprite = Sprite.Create(spriteTexture, spriteImage.sprite.rect, spriteImage.sprite.pivot);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EraseSprite(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        EraseSprite(eventData.position);
    }
}
