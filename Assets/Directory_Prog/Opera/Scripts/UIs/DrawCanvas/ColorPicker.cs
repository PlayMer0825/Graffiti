using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour {
    [SerializeField] private Image circlePalette = null;
    [SerializeField] private Image picker = null;
    [SerializeField] private Color selectedColor = Color.black;
    public Color SelectedColor { get => selectedColor; }

    [SerializeField] private Vector2 sizeOfPalette;
    [SerializeField] private Collider2D paletteCollider;

    private void Start() {
        sizeOfPalette = new Vector2(circlePalette.rectTransform.rect.width, circlePalette.rectTransform.rect.height);
    }

    public void MousePointerDown() {
        SelectColor();
    }

    public void MouseDrag() {
        SelectColor();
    }

    private Color GetColor(Vector2 point) {
        Vector2 circlePalettePosition = circlePalette.transform.position;

        Vector2 position = point - circlePalettePosition + sizeOfPalette * 0.5f;

        Vector2 normalized = new Vector2(
            (position.x / (circlePalette.GetComponent<RectTransform>().rect.width)),
            (position.y / (circlePalette.GetComponent<RectTransform>().rect.height)));

        Texture2D texture = circlePalette.mainTexture as Texture2D;
        Color circularSelectedColor = texture.GetPixelBilinear(normalized.x, normalized.y);

        return circularSelectedColor;
    }

    private void SelectColor() {
        Vector3 offset = Input.mousePosition - circlePalette.transform.position;
        Vector3 diff = Vector3.ClampMagnitude(offset, sizeOfPalette.magnitude);

        Vector3 point = circlePalette.transform.position + diff;

        Color picked = GetColor(point);
        if(picked == Color.black)
            return;

        picker.transform.position = point;
        selectedColor = picked;
    }
}
