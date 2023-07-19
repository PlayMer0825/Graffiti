using UnityEngine;
using UnityEngine.UI;

public class ScratchImage : MonoBehaviour
{
    public Image targetImage;
    public Texture2D scratchTexture;
    public float scratchThreshold = 0.5f;

    private bool isScratching = false;
    private Color[] scratchColors;
    private Sprite originalSprite;

    private void Start()
    {
        // 초기화
        originalSprite = targetImage.sprite;
        scratchColors = new Color[scratchTexture.width * scratchTexture.height];
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isScratching)
            {
                Scratch();
            }
            else
            {
                StartScratching();
            }
        }
        else
        {
            StopScratching();
        }
    }

    private void StartScratching()
    {
        isScratching = true;
        targetImage.sprite = Sprite.Create(scratchTexture, new Rect(0, 0, scratchTexture.width, scratchTexture.height), Vector2.one * 0.5f);
    }

    private void StopScratching()
    {
        if (isScratching)
        {
            isScratching = false;
            targetImage.sprite = originalSprite;
        }
    }

    private void Scratch()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(targetImage.rectTransform, Input.mousePosition, null, out localPoint);

        Vector2 uv = new Vector2(localPoint.x / targetImage.rectTransform.rect.width + 0.5f, localPoint.y / targetImage.rectTransform.rect.height + 0.5f);
        uv = new Vector2(uv.x * scratchTexture.width, uv.y * scratchTexture.height);

        int x = Mathf.RoundToInt(uv.x);
        int y = Mathf.RoundToInt(uv.y);

        if (x >= 0 && x < scratchTexture.width && y >= 0 && y < scratchTexture.height)
        {
            scratchColors[y * scratchTexture.width + x] = Color.clear;
            scratchTexture.SetPixels(scratchColors);
            scratchTexture.Apply();

            float erasedPercentage = CalculateErasedPercentage();
            if (erasedPercentage >= scratchThreshold)
            {
                StopScratching();
                // 지우기 완료 시 실행할 코드 작성
            }
        }
    }

    private float CalculateErasedPercentage()
    {
        float erasedPixels = 0;
        int totalPixels = scratchColors.Length;

        for (int i = 0; i < totalPixels; i++)
        {
            if (scratchColors[i].a <= 0)
            {
                erasedPixels++;
            }
        }

        return erasedPixels / totalPixels;
    }
}
