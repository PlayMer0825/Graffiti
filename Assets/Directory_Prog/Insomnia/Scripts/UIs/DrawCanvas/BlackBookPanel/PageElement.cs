using UnityEngine;
using UnityEngine.UI;

namespace Insomnia {
    public class PageElement : MonoBehaviour {
        private Button _elemButton = null;
        [SerializeField] private Image _elemImage = null;

        private Stencil _stencil;

        private Texture _maskTexture = null;
        private Sprite _maskVisual = null;

        private bool _isInitialized = false;

        private void Awake() {
            _elemButton = GetComponent<Button>();
        }

        public void InitElement(Stencil data) {
            if(data.MaskSprite == null) {
                _elemImage.sprite = null;
                _stencil = data;
                _isInitialized = false;
                _elemButton.interactable = false;
            }
            else if(data.IsUnlocked == false) {
                _elemImage.sprite = data.MaskVisualSprite;
                _stencil = data;
                _isInitialized = true;
                _elemButton.interactable = false;
            }
            else {
                _elemImage.sprite = data.MaskVisualSprite;
                _stencil = data;
                _isInitialized = true;
                _elemButton.interactable = true;
            }
        }

        public void OnClick_SelectMask() {
            if(_isInitialized == false && _stencil.IsUnlocked == false)
                return;

            if(TutorialObject.IsAnyTutorialPlaying)
                return;

            DrawManager.Instance.BlackBook.InstallStencil(_stencil);
        }
    }
}
