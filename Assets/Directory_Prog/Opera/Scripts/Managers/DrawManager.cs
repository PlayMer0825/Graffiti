using PaintIn3D;
using UnityEngine;
using UnityEngine.Events;

namespace Insomnia {
    /// Finish가 그래피티 종료
    /// Clear가 스텐실 종료
    public class DrawManager :  Singleton<DrawManager>{
        #region UI Panel
        [SerializeField] private DrawPanel m_draw = null;
        public DrawPanel Draw {
            get {
                if(m_draw == null)
                    m_draw = GetComponentInChildren<DrawPanel>();

                return m_draw;
            }
        }

        [SerializeField] private BagPanel m_bag = null;
        public BagPanel Bag {
            get {
                if(m_bag == null)
                    m_bag = GetComponentInChildren<BagPanel>();

                return m_bag;
            }
        }

        [SerializeField] private BlackBookPanel m_blackbook = null;
        public BlackBookPanel BlackBook {
            get {
                if(m_blackbook == null)
                    m_blackbook = GetComponentInChildren<BlackBookPanel>();

                return m_blackbook;
            }
        }

        #endregion

        [SerializeField] private Spray m_spray = null;
        public Spray Spray { get => m_spray; }

        [SerializeField]private StencilMask m_stencil = null;
        public StencilMask Stencil { get => m_stencil; }

        [SerializeField] private Speaker m_speaker = null;
        public Speaker DrawSpeaker { get => m_speaker; }

        public UnityEvent onDrawStart = null;
        public UnityEvent onDrawFinished = null;
        public DrawObject curDrawing = null;


        #region Melamine Works
        public Point_Of_View _pointOfView = null;

        #endregion

        private bool m_isSwitching = false;

        private bool _isDrawing = false;
        public bool IsDrawing { get => _isDrawing; }

        public bool CanMove { get => m_isSwitching == false; }

        protected override void Awake() {
            base.Awake();
            _pointOfView = GameObject.Find("Player").GetComponent<Point_Of_View>();
        }

        private void Update() {
            if(InteractionManager.Instance.IsInteracting == false)
                return;

            if(Input.GetKeyDown(KeyCode.Tab)) {
                m_bag.OpenPanel();
            }

            if(Input.GetKeyDown(KeyCode.B)) {
                m_blackbook.OpenPanel();
            }
        }

        private void OnDisable() {
            _instance = null;
        }

        private void OnDestroy() {
            _instance = null;
        }

        public void StartDrawing() {
            _isDrawing = true;
            m_blackbook.gameObject.SetActive(true);
            m_blackbook.ClosePanel();
            m_bag.gameObject.SetActive(true);
            m_bag.ClosePanel();
            m_draw.gameObject.SetActive(true);
            m_draw.OpenPanel();
            _pointOfView.ForceChangeToTps();

            P3dChangeCounter counter = InteractionManager.Instance.CurInteracting.gameObject.GetComponentInChildren<P3dChangeCounter>();
            if(counter == null) return;
            PercentageUI ui = m_draw.Percent;
            if(ui == null) return;
            ui.RegisterForChangeCounter(counter);
            m_isSwitching = true;
            Invoke("AfterOneSecSetBool", 2.2f);

            onDrawStart?.Invoke();
        }

        /// <summary>
        /// 상시 켜진 DrawPanel을 제외한 다른 패널이 켜져있는지 확인 후 없다면 true, 있다면 false를 반환.
        /// </summary>
        /// <returns></returns>
        public bool IsAnyPanelOpened() {
            return m_bag.IsOpened || m_blackbook.IsOpened;
        }

        public void FinishDrawing() {
            _isDrawing = false;
            m_blackbook.ClosePanel();
            m_blackbook.gameObject.SetActive(false);
            m_bag.ClosePanel();
            m_bag.gameObject.SetActive(false);
            m_draw.ClosePanel();
            m_draw.gameObject.SetActive(false);
            m_spray.OnClickMouseLeft(false);
            _pointOfView.ForceChangeToSide();
            m_draw.Percent.ReleaseCounter();
            curDrawing.FinishInteract();
            curDrawing = null;

            m_isSwitching = true;
            Invoke("AfterOneSecSetBool", 2.2f);

            onDrawFinished?.Invoke();
            GameObject.Find("Player").GetComponent<Point_Of_View>().ForceChangeToSide();
        }

        private void AfterOneSecSetBool() {
            m_isSwitching = false;
        }
    }
}
