using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace OperaHouse {
    public class BagPanel : UIPanel {
        [SerializeField] RectTransform _panel = null;

        [SerializeField] RectTransform _buttonGroup = null;

        [SerializeField] Button _clearButton = null;
        [SerializeField] Button _maskRemoveButton = null;

        TweenerCore<Vector3, Vector3, VectorOptions> _panelTweener = null;
        TweenerCore<Vector3, Vector3, VectorOptions> _buttonTweener = null;

        public override bool IsPlayingAnimation {
            get {
                if(_panelTweener == null || _buttonTweener == null)
                    return false;

                return _panelTweener.IsComplete() && _buttonTweener.IsComplete();
            }
        }

        protected override void Init() {
            _panel.position = _panel.position - new Vector3(_panel.rect.width, 0, 0);
            _panel.gameObject.SetActive(true);

            _buttonGroup.position = _buttonGroup.position + new Vector3(0, _buttonGroup.rect.height, 0);
            _buttonGroup.gameObject.SetActive(true);
        }

        public override void OpenPanel() {
            if(DrawManager.Instance.IsAnyPanelOpened() && IsOpened == false)
                return;

            if(_panelTweener != null)
                _panelTweener.Kill(true);

            if(_buttonTweener != null)
                _buttonTweener.Kill(true);

            if(IsOpened)
                base.ClosePanel();
            else
                base.OpenPanel();
        }

        public override void ClosePanel() {
            if(_panelTweener != null)
                _panelTweener.Kill(true);

            if(_buttonTweener != null)
                _buttonTweener.Kill(true);


            base.ClosePanel();
        }

        protected override void OnEnablePanel() {
            _panelTweener = _panel.DOMove(_panel.position + new Vector3(_panel.rect.width, 0, 0), 1f);
            _buttonTweener = _buttonGroup.DOMove(_buttonGroup.position - new Vector3(0, _buttonGroup.rect.height, 0), 1f);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            base.OnEnablePanel();
        }

        protected override void OnDisablePanel() {
            _panelTweener = _panel.DOMove(_panel.position - new Vector3(_panel.rect.width, 0, 0), 1f);
            _buttonTweener = _buttonGroup.DOMove(_buttonGroup.position + new Vector3(0, _buttonGroup.rect.height, 0), 1f);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            base.OnDisablePanel();
        }


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
            //TODO: 여기서도 그림 다 그렸는지 체크

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

            //TODO: 여기서 그림 다 그렸는지 체크

            interacted.FinishInteract();
            DrawManager.Instance.FinishDrawing();
        }
    }
}