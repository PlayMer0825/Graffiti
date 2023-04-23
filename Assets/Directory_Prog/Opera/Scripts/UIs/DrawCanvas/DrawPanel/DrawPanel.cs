using OperaHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class DrawPanel : MonoBehaviour {
        [SerializeField] private Button _clearButton = null;
        [SerializeField] private Button _maskRemoveButton = null;
        [SerializeField] private Slider _remainSlider = null;

        public Button ClearButton { get => _clearButton; }
        public Button MaskRemoveButton { get => _maskRemoveButton; }
        public Slider RemainSlider { get => _remainSlider; }

        public void ActivateRemoveButtonWithMask(StencilMask mask) {
            if(_maskRemoveButton == null)
                return;

            _maskRemoveButton.onClick.RemoveAllListeners();
            _maskRemoveButton.onClick.AddListener(mask.ReleaseMask);

            _maskRemoveButton.gameObject.SetActive(true);
            _clearButton.gameObject.SetActive(false);
        }


        /// <summary>
        /// 마스크 오브젝트 삭제 버튼을 눌렀을 때 호출하는 함수.
        /// </summary>
        public void OnClick_RemoveButton() {
            _clearButton.gameObject.SetActive(true);
            _maskRemoveButton.gameObject.SetActive(false);
        }


        /// <summary>
        /// 그래피티를 종료버튼을 눌렀을 때 호출하는 함수.
        /// </summary>
        public void OnClick_ClearButton() {
            Interactable interacted = InteractionManager.Instance.CurInteracting;

            if(interacted == null)
                return;

            interacted.FinishInteract();
            DrawManager.Instance.CloseAllPanels();
        }
    }
}