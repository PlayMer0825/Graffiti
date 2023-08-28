using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace Insomnia {
    public class BagPanel : UIPanel {
        [SerializeField] RectTransform _palette = null;
        private Vector3 _paletteClosedPos, _paletteOpenPos;
        //[SerializeField] RectTransform _paletteArea = null;
        [SerializeField] RectTransform _paletteOpen = null;
        [SerializeField] RectTransform _paletteClose = null;


        TweenerCore<Vector3, Vector3, VectorOptions> _paletteTweener = null;

        public override bool IsPlayingAnimation {
            get {
                if(_paletteTweener == null)
                    return false;

                return _paletteTweener.IsComplete();
            }
        }

        protected override void Init() {
            //_palette.localPosition = new Vector3(-(Screen.width / 2), _palette.localPosition.y, 0);
            //_paletteClosedPos = _palette.position;
            //_paletteOpenPos = _palette.position + new Vector3(_paletteArea.rect.width, 0f, 0f);
            _palette.gameObject.SetActive(true);
        }

        public override void OpenPanel() {
            if(DrawManager.Instance.IsAnyPanelOpened() && IsOpened == false)
                return;

            if(_paletteTweener != null)
                _paletteTweener.Kill();

            if(IsOpened) {
                base.ClosePanel();
                DrawManager.Instance.DrawSpeaker.PlayOneShot(SFX_GraffitiUI.Bag_Close);
            }
            else {
                base.OpenPanel();
                DrawManager.Instance.DrawSpeaker.PlayOneShot(SFX_GraffitiUI.Bag_Open);
            }   
        }

        public override void ClosePanel() {
            if(_paletteTweener != null)
                _paletteTweener.Kill();

            base.ClosePanel();
        }

        protected override void OnEnablePanel() {
            //_paletteTweener = _palette.DOMove(_paletteOpenPos, 1f);

            _paletteTweener = _palette.DOMove(_paletteOpen.position, 1f);

            _palette.gameObject.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            base.OnEnablePanel();
        }

        protected override void OnDisablePanel() {
            //_paletteTweener = _palette.DOMove(_paletteClosedPos, 1f);
            _paletteTweener = _palette.DOMove(_paletteClose.position, 1f);
            //_paletteTweener.onComplete.
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            base.OnDisablePanel();
        }


        public void ActivateRemoveButtonWithMask(StencilMask mask) {
            //if(_maskRemoveButton == null)
            //    return;

            //_maskRemoveButton.onClick.RemoveAllListeners();
            //_maskRemoveButton.onClick.AddListener(mask.ReleaseMask);

            //_maskRemoveButton.gameObject.SetActive(true);
            //_clearButton.gameObject.SetActive(false);
        }

        /// <summary>
        /// 마스크 오브젝트 삭제 버튼을 눌렀을 때 호출하는 함수.
        /// </summary>
        public void OnClick_RemoveButton() {
            //TODO: 여기서도 그림 다 그렸는지 체크

            //_clearButton.gameObject.SetActive(true);
            //_maskRemoveButton.gameObject.SetActive(false);
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