using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OperaHouse {
    public class CircleColorPicker : Singleton<CircleColorPicker>{
        public Image circlePalette;
        public Image picker;
        public Color selectedColor;
        public Spray e_spray = null;

        [SerializeField] private Vector2 sizeOfPalette;
        [SerializeField] private CircleCollider2D paletteCollider;

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

            e_spray.Color = getColor();
            //e_spray.ChangeColorTo(getColor());
        }
    }
}