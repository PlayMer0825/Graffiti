using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleColorPicker : MonoBehaviour {
    public Image circlePalette;
    public Image picker;
    public Color selectedColor;
    public SprayController e_spray = null;

    [SerializeField] private Vector2 sizeOfPalette;
    [SerializeField] private CircleCollider2D paletteCollider;

    private static CircleColorPicker instance = null;
    public static CircleColorPicker Instance {
        get {
            if(null == instance) instance = FindObjectOfType<CircleColorPicker>();
            return instance;
        }
    }

    private void Awake() {
        if(null == instance) instance = this;
    }

    void Start() {
        paletteCollider = circlePalette.GetComponent<CircleCollider2D>();

        sizeOfPalette = new Vector2(
            circlePalette.GetComponent<RectTransform>().rect.width,
            circlePalette.GetComponent<RectTransform>().rect.height);
    }

    public void mousePointerDown() {
        selectColor();
    }

    public void mouseDrag() {
        selectColor();
    }

    private Color getColor() {
        Vector2 circlePalettePosition = circlePalette.transform.position;
        Vector2 pickerPosition = picker.transform.position;

        Vector2 position = pickerPosition - circlePalettePosition + sizeOfPalette * 0.5f;
        Vector2 normalized = new Vector2(
            (position.x / (circlePalette.GetComponent<RectTransform>().rect.width)),
            (position.y / (circlePalette.GetComponent<RectTransform>().rect.height)));

        Texture2D texture = circlePalette.mainTexture as Texture2D;
        Color circularSelectedColor = texture.GetPixelBilinear(normalized.x, normalized.y);

        return circularSelectedColor;
    }

    private void selectColor() {
        if(e_spray == null)
            return;

        Vector3 offset = Input.mousePosition - transform.position;
        Vector3 diff = Vector3.ClampMagnitude(offset, paletteCollider.radius);

        picker.transform.position = transform.position + diff;

        selectedColor = getColor();

        e_spray.ChangeColorTo(getColor());
    }
}