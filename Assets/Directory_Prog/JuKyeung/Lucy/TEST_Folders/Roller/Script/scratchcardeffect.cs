using UnityEngine;

public class scratchcardeffect : MonoBehaviour
{
    public GameObject maskPrefab;
    public GameObject targetObject;
    private GameObject currentMask;
    private bool isDrawing = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDrawing();
        }

        if (isDrawing)
        {
            CreateMask();
        }

        CheckCompletion();
    }

    void StartDrawing()
    {
        isDrawing = true;
    }

    void StopDrawing()
    {
        isDrawing = false;
    }

    void CreateMask()
    {
        Vector3 mousePos = GetMouseWorldPosition();

        if (IsMaskExists(mousePos))
        {
            return;
        }

        currentMask = Instantiate(maskPrefab, mousePos, Quaternion.identity);
        currentMask.transform.parent = transform;
        currentMask.transform.position = new Vector3(currentMask.transform.position.x, currentMask.transform.position.y, targetObject.transform.position.z);

        CheckCompletion();
    }

    bool IsMaskExists(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Mask"))
            {
                return true;
            }
        }
        return false;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    void CheckCompletion()
    {
        SpriteRenderer targetRenderer = targetObject.GetComponent<SpriteRenderer>();
        Texture2D targetTexture = targetRenderer.sprite.texture;

        int totalPixels = targetTexture.width * targetTexture.height;
        int maskedPixels = 0;

        if (currentMask != null)
        {
            SpriteRenderer maskRenderer = currentMask.GetComponent<SpriteRenderer>();
            if (maskRenderer != null)
            {
                Texture2D maskTexture = maskRenderer.sprite.texture;
                Color32[] pixels = maskTexture.GetPixels32();

                foreach (Color32 pixel in pixels)
                {
                    if (pixel.a > 0)
                    {
                        maskedPixels++;
                    }
                }

                float maskedRatio = (float)maskedPixels / totalPixels;
                Debug.Log("Masked Ratio: " + maskedRatio);
            }
        }
    }
}
