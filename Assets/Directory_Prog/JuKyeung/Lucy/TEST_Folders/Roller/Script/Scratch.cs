using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scratch : MonoBehaviour
{
    public SpriteMask spriteMask;
    public SpriteRenderer targetImage;
    public UnityEngine.Events.UnityEvent endEvent;

    private bool isImageErased = false;
    private List<SpriteRenderer> scratchedImages = new List<SpriteRenderer>();

    private void Update()
    {
        if (!isImageErased)
        {
            if (Input.GetMouseButton(0))
            {
                ScratchImage();
            }

            // 이미지가 전부 지워졌는지 확인
            if (scratchedImages.Count > 0 && !IsAnyImageVisible())
            {
                isImageErased = true;
                endEvent.Invoke();
            }
        }
    }

    private void ScratchImage()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>() == targetImage)
        {
            SpriteRenderer scratchedImage = hit.collider.GetComponent<SpriteRenderer>();
            if (!scratchedImages.Contains(scratchedImage))
            {
                scratchedImages.Add(scratchedImage);
                scratchedImage.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
            }
        }
    }

    private bool IsAnyImageVisible()
    {
        foreach (SpriteRenderer scratchedImage in scratchedImages)
        {
            if (scratchedImage.isVisible)
            {
                return true;
            }
        }
        return false;
    }
}
