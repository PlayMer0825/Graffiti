using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class StencilMask : MonoBehaviour {
        [SerializeField] MeshRenderer _maskPreview = null;
        [SerializeField] private P3dMask _mask = null;
        [SerializeField] private SpriteRenderer _maskVisual = null;
        [SerializeField] private DrawPanel _drawPanel = null;
        private Material _mat = null;
        private int _curMaskPixelCount = 0;
        bool _isMaskEnabled = false;

        public bool MaskEnabled { get => _isMaskEnabled; }
        public int MaskPixelCount { get => _curMaskPixelCount; }
        public float ScaleRatio { get => transform.localScale.x; }

        [SerializeField] private Color availColor, nonAvailColor;

        private void Awake() {
            _mat = _maskPreview.material;
        }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Z)) {
                bool isActive = _mask.enabled;
                _mask.enabled = !isActive;
                _maskVisual.gameObject.SetActive(!isActive);
                _drawPanel.SetStencilVisible(_mask.enabled);
            }
        }

        /// <summary>
        /// 마스크를 처음 생성할 때 초기화하는 함수
        /// </summary>
        /// <param name="maskTexture">Texture타입의 이미지파일 전달</param>
        /// <param name="maskVisualSprite">Sprite타입의 이미지파일 전달</param>
        public void InitializeMask(Texture2D maskTexture, Sprite maskVisualSprite) {
            if(MaskEnabled)
                ReleaseMask();
            _mask.Texture = maskTexture;
            _curMaskPixelCount = GetPixelCount(maskTexture);
            _maskVisual.sprite = maskVisualSprite;
            _maskPreview.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 설치 범위를 벗어났을 때 호출하는 함수.
        /// </summary>
        /// <param name="isVisible"></param>
        public void SetMaskVisible(bool isVisible) {
            if(isVisible) {
                _maskPreview.gameObject.SetActive(true);
            }
            else {
                _maskPreview.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 마스크를 설치할 때 설치 가능할 경우 녹색, 불가능할 경우 빨간색을 띄도록 설정해주는 함수.
        /// </summary>
        /// <param name="isAvailable">true: Green / false: Red</param>
        public void SetMaskInstallationAvailable(bool isAvailable) {
            if(isAvailable == true)
                _mat.color = availColor;
            else
                _mat.color = nonAvailColor;
        }

        /// <summary>
        /// 마스크의 위치를 수정하거나 고정할 때 호출하는 함수.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void SetMaskTransform(Vector3 position, Vector3 rotation) {
            transform.position = position + new Vector3(0, 0, -0.03f);
            transform.rotation = Quaternion.Euler(rotation);
        }

        /// <summary>
        /// 마스크를 최종적으로 설치할 때 호출하는 함수.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void InstallMask(Vector3 position, Vector3 rotation) {
            _isMaskEnabled = true;
            SetMaskTransform(position, rotation);
            _maskPreview.gameObject.SetActive(false);
            DrawManager.Instance.Bag.ActivateRemoveButtonWithMask(this);
            _drawPanel.OpenStencilVisualizer();
            DrawManager.Instance.Draw.Percent.SetDrawFinishMode(true);
        }

        /// <summary>
        /// 마스크를 전부 사용했거나 마스크 생성을 취소했을 때 호출하는 함수.
        /// </summary>
        public void ReleaseMask() {
            _isMaskEnabled = false;
            _mask.Texture = null;
            _maskVisual.sprite = null;
            _maskPreview.gameObject.SetActive(false);
            gameObject.SetActive(false);
            _drawPanel.CloseStencilVisualizer();
            DrawManager.Instance.Draw.Percent.SetDrawFinishMode(false);
        }

        private int GetPixelCount(Texture2D texture) {
            var tw = texture.width;
            var th = texture.height;
            var source = texture.GetPixels();
            var pixels = texture.GetPixels();
            int result = 0;

            int i1 = 0;

            for(int iy = 0; iy < th; iy++) {
                for(int ix = 0; ix < tw; ix++) {
                    if(source[i1++].a >= 0.9f)
                        result++;
                }
            }

            return (int)(result * 0.1f);
        }
    }
}