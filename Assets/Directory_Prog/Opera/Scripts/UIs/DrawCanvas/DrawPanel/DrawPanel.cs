using OperaHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class DrawPanel : UIPanel {
        [SerializeField] private Slider _remainSlider = null;

        public Slider RemainSlider { get => _remainSlider; }

        protected override void OnEnablePanel() {
            //DrawPanel은 그림 그릴 땐 상시 출력되어야하기 때문에 아무 것도 하지 않는다.
            gameObject.SetActive(true);
        }

        protected override void OnDisablePanel() {
            //DrawPanel은 그림 그릴 땐 상시 출력되어야하기 때문에 아무 것도 하지 않는다.
            gameObject.SetActive(false);
        }
    }
}