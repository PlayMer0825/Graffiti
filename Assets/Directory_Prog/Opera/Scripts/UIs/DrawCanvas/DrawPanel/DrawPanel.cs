using OperaHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class DrawPanel : UIPanel {
        [SerializeField] private Slider _remainSlider = null;
        [SerializeField] private Image[] _hotKeyImages;
        private bool[] _hotKeyFilled;

        public Slider RemainSlider { get => _remainSlider; }

        protected override void Init() {
            if(_hotKeyImages.Length <= 0)
                return;

            _hotKeyFilled = new bool[_hotKeyImages.Length];
            _hotKeyFilled.Initialize();

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

        private void Update() {
            if(_hotKeyImages.Length <= 0)
                return;

            for(int i = 0; i < _hotKeyImages.Length; i++) {
                if(Input.GetKeyDown((KeyCode)(49 + i ))){
                    if(_hotKeyFilled[i] == false)
                        break;

                    DrawManager.Instance.Spray.Color = _hotKeyImages[i].color;
                }
            }
        }
    }
}