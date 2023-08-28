using PaintIn3D;
using UnityEngine;

namespace Insomnia {
    public class DrawObject : Interactable {
        [Header("P3d Plugins")] 
        [SerializeField] [InspectorName("P3dPaintable")]
        private P3dPaintable _ptble = null;

        [SerializeField]
        private InteractionArea _interactArea = null;

        [SerializeField] private GameObject m_areaConstraint = null;

        

        private bool m_isDrawing = false;

        private bool m_isFisrtDraw = true;
        public bool IsFirstDraw { get => m_isFisrtDraw; }
        public int MaxPixelCount { get; private set; }
        
        #region Unity Event Functions
        private void Awake() {
            _ptble = GetComponentInChildren<P3dPaintable>();

            _interactArea = GetComponentInChildren<InteractionArea>();
            if(_interactArea == null)
                return;

            MeshRenderer renderer = _ptble.gameObject.GetComponent<MeshRenderer>();
            MaxPixelCount = GetPixelCount((Texture2D)renderer.material.mainTexture);
            Debug.Log($"DrawObject MaxPixelCount: {MaxPixelCount}");
        }

        #endregion

        public override void ReadyInteract(Collider other) {
            base.ReadyInteract(other);
        }

        public override void UnReadyInteract(Collider other) {
            base.UnReadyInteract(other);
        }

        public override void StartInteract() {
            //TODO: 카메라 이동 및 그림 그리기 기능 ON
            if (interactable == false)
                return;
            m_isDrawing = true;
            _ptble.enabled = true;
            _interactCanvas.SetActive(false);
            _interactArea.SetColliderActivation(false);
            InteractionManager.Instance.StartedInteract(this);
            if(m_isFisrtDraw) 
                DrawManager.Instance.onDrawStart.AddListener(() => { DrawManager.Instance.BlackBook.OpenPanel(); });


            if(m_areaConstraint != null)
                m_areaConstraint.SetActive(true);
            DrawManager.Instance.curDrawing = this;
            DrawManager.Instance.StartDrawing();
            onStartInteract?.Invoke();
        }

        public override void FinishInteract() {
            m_isDrawing = false;
            //TODO: 카메라 종료 및 그림 그리기 기능 OFF
            _ptble.enabled = false;
            _interactCanvas.SetActive(true);
            _interactArea.SetColliderActivation(true);

            if(m_areaConstraint != null)
                m_areaConstraint.SetActive(false);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            onFinishInteract?.Invoke();
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

            return (int)( result * 0.1f );
        }

        private void Update() {
            if(IsStandby == false)
                return;

            if(m_isDrawing)
                return;//

            if(Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
                StartInteract();
        }
    }
}