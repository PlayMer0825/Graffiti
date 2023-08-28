using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace Insomnia {
    public class DrawPanel : UIPanel {
        [SerializeField] private Slider _remainSlider = null;
        [SerializeField] private Image[] _hotKeyImages;
        [SerializeField] private Image _stencilVisualizer = null;
        [SerializeField] private Sprite[] _stencilVisualizerSprite;
        [SerializeField] private StencilMask _stencil = null;
        [SerializeField] private PercentageUI _percent = null;
        public PercentageUI Percent { get => _percent; }

        private bool[] _hotKeyFilled;
        private bool isVisualOpened = false;

        TweenerCore<Vector3, Vector3,VectorOptions> _stencilTweener = null;

        public Slider RemainSlider { get => _remainSlider; }

        protected override void Init() {
            if(_hotKeyImages.Length <= 0)
                return;

            _hotKeyFilled = new bool[_hotKeyImages.Length];
            _hotKeyFilled.Initialize();

            _stencilVisualizer.gameObject.SetActive(false);
            _stencilVisualizer.rectTransform.position = _stencilVisualizer.rectTransform.position - new Vector3(0, _stencilVisualizer.rectTransform.rect.height, 0);
            _stencilVisualizer.gameObject.SetActive(true);
        }

        protected override void OnEnablePanel() {
            //DrawPanel은 그림 그릴 땐 상시 출력되어야하기 때문에 아무 것도 하지 않는다.
            gameObject.SetActive(true);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            base.OnEnablePanel();
        }

        protected override void OnDisablePanel() {
            //DrawPanel은 그림 그릴 땐 상시 출력되어야하기 때문에 아무 것도 하지 않는다.
            gameObject.SetActive(false);
            base.OnDisablePanel();
        }

        public void AddSelectedColor(Color color) {
            if(_hotKeyImages.Length <= 0)
                return;

            for(int i = 0; i < _hotKeyImages.Length; i++) {
                if(_hotKeyFilled[i] == false) {
                    _hotKeyImages[i].color = color;
                    _hotKeyFilled[i] = true;
                    return;
                }
                else {
                    if(_hotKeyImages[i].color == color)
                        return;
                }
            }

            _hotKeyImages[0].color = color;

            return;
        }

        public void OpenStencilVisualizer() {
            if(isVisualOpened)
                return;

            if(_stencilTweener != null)
                _stencilTweener.Kill(true);

            _stencilTweener = _stencilVisualizer.transform.DOMove(_stencilVisualizer.transform.position + new Vector3(0, _stencilVisualizer.rectTransform.rect.height, 0), 1f);
            isVisualOpened = true;
        }

        public void CloseStencilVisualizer() {
            if(isVisualOpened == false)
                return;

            if(_stencilTweener != null)
                _stencilTweener.Kill(true);

            _stencilTweener = _stencilVisualizer.transform.DOMove(_stencilVisualizer.transform.position - new Vector3(0, _stencilVisualizer.rectTransform.rect.height, 0), 1f);
            isVisualOpened = false;
        }

        /// <summary>
        /// false(Green) : out true(Alpha) : in
        /// </summary>
        /// <param name="isActive"></param>
        public void SetStencilVisible(bool isActive) {
            if(_stencilVisualizerSprite.Length < 2)
                return;

            int res = isActive ? 1 : 0;
            _stencilVisualizer.sprite = _stencilVisualizerSprite[res];
        }
    }
}