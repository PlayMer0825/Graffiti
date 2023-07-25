using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Insomnia {

    public class BlackBookPanel : UIPanel {
        [SerializeField] private RectTransform _blackBookGroup = null;
        [SerializeField] private TextMeshProUGUI _pageText = null;
        [SerializeField] private StencilInstaller _stencilInstaller = null;
        [SerializeField] private PageGroupUI _leftPage = null;
        [SerializeField] private PageGroupUI _rightPage = null;
        private int _curPageNum = 0;

#if UNITY_EDITOR
        [SerializeField] private StencilData m_graffitiStencil = null;
        [SerializeField] private StencilData m_shapeStencil = null;
#endif

        private StencilData _curStencils = null;

        TweenerCore<Vector3, Vector3, VectorOptions> _blackBookTweener = null;

        public override bool IsPlayingAnimation { get {
                if(_blackBookTweener == null)
                    return false;

                return _blackBookTweener.IsComplete();
            }
        }

        public StencilInstaller StencilInstaller { get => _stencilInstaller; }

        private void Update() {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                OnClick_ExitBlackBook();
            }
        }

        protected override void Init() {
            _blackBookGroup.position =_blackBookGroup.position + new Vector3(0, _blackBookGroup.rect.height, 0);
            _blackBookGroup.gameObject.SetActive(true);
        }

        public override void OpenPanel() {
            if(DrawManager.Instance.IsAnyPanelOpened() && IsOpened == false)
                return;

            if(_blackBookTweener != null)
                _blackBookTweener.Kill(true);

            if(IsOpened) {
                base.ClosePanel();
                DrawManager.Instance.DrawSpeaker.PlayOneShot(SFX_GraffitiUI.Blackbook_Close);
            }
            else {
                base.OpenPanel();
                DrawManager.Instance.DrawSpeaker.PlayOneShot(SFX_GraffitiUI.Blackbook_Open);
            }
        }

        public override void ClosePanel() {
            if(_blackBookTweener != null)
                _blackBookTweener.Kill(true);

            base.ClosePanel();
        }

        protected override void OnEnablePanel() {
            _blackBookTweener = _blackBookGroup.DOMove(_blackBookGroup.position - new Vector3(0, _blackBookGroup.rect.height, 0), 1f);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            base.OnEnablePanel();
        }

        protected override void OnDisablePanel() {
            _blackBookTweener = _blackBookGroup.DOMove(_blackBookGroup.position + new Vector3(0, _blackBookGroup.rect.height, 0), 1f);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            base.OnDisablePanel();
        }

        public void OnClick_NextPage() {
            if(_curStencils.m_stencils.Count <= ( _curPageNum + 1 ) * 12)
                return;

            _curPageNum++;
            SetPageGroupsWithCurrentPage();
            DrawManager.Instance.DrawSpeaker.PlayOneShot(SFX_GraffitiUI.Blackbook_PageShift);
        }

        public void OnClick_PrevPage() {
            if(_curPageNum == 0)
                return;

            _curPageNum--;
            SetPageGroupsWithCurrentPage();
            DrawManager.Instance.DrawSpeaker.PlayOneShot(SFX_GraffitiUI.Blackbook_PageShift);
        }

        /// <summary>
        /// 블랙북의 태그를 선택했을 때 처음 접근하는 함수
        /// </summary>
        /// <param name="data"></param>
        public void OnClick_SetBlackBookTag(StencilData data) {
            if(data == null)
                return;

            _curStencils = data;
            _curPageNum = 0;

            SetPageGroupsWithCurrentPage();
            DrawManager.Instance.DrawSpeaker.PlayOneShot(SFX_GraffitiUI.Blackbook_PageShift);
        }

        public void OnClick_ExitBlackBook() {
            ClosePanel();
        }

        /// <summary>
        /// 왼쪽/오른쪽 PageGroup을 현재 페이지의 데이터로 초기화하는 함수.
        /// </summary>
        private void SetPageGroupsWithCurrentPage() {
            SetCurrentPageUI();
            _leftPage.InitializePage(GetStencilsInPage(_curPageNum * 12));
            _rightPage.InitializePage(GetStencilsInPage(_curPageNum * 12 + 6));
        }

        /// <summary>
        /// 스텐실을 선택했을 때 처음 접근하는 함수.
        /// </summary>
        /// <param name="data"></param>
        public void InstallStencil(Stencil data) {
            if(data.MaskSprite == null)
                return;

            _stencilInstaller.StartInstallStencil(data.MaskSprite, data.MaskOutlineSprite);
            ClosePanel();
        }

        /// <summary>
        /// _curStencils._stencils이 가지고 있는 Stencil데이터를 최대 6개까지 Slice해서 반환하는 함수.
        /// </summary>
        /// <param name="offset"> 가져올 Stencil의 offset. </param>
        /// <returns> offset에서 최대 6개의 Stencil을 가지는 List를 반환 <returns>
        private List<Stencil> GetStencilsInPage(int offset) {
            if(_curStencils.m_stencils.Count <= offset)
                return null;

            int count = Mathf.Min(_curStencils.m_stencils.Count - offset, 6);
            List<Stencil> ret = _curStencils.m_stencils.GetRange(offset, count);

            return ret;
        }

        /// <summary>
        /// 현재 페이지를 출력하는 TMPro TextUI를 수정하는 함수.
        /// </summary>
        private void SetCurrentPageUI() {
            int lastPage = _curStencils.m_stencils.Count % 12 > 0 ? _curStencils.m_stencils.Count / 12 + 1 : _curStencils.m_stencils.Count / 12;
            _pageText.text = $"{_curPageNum + 1}/{lastPage}";
        }

        public void UnlockStencil(StencilData stencilSO, string name) {
            Stencil unlocked = stencilSO.UnlockStencil(name);
            if(unlocked == null)
                return;

            DrawManager.Instance.Notify.Initialize(unlocked);
        }

#if UNITY_EDITOR
        public void UnlockStencil(int index) {
            Stencil unlocked = m_graffitiStencil.UnlockStencil(index);
            if(unlocked == null)
                return;

            DrawManager.Instance.Notify.Initialize(unlocked);
        }
        public void InitializeStencil() {
            m_graffitiStencil.InitializeAll();
        }
#endif
    }
}
