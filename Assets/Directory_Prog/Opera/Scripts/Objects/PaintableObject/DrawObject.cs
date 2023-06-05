using Insomnia;
using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

namespace OperaHouse {
    public class DrawObject : Interactable {
        [Header("P3d Plugins")] 
        [SerializeField] [InspectorName("P3dPaintable")]
        private P3dPaintable _ptble = null;

        [SerializeField]
        private InteractionArea _interactArea = null;

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
            _ptble.enabled = true;
            _interactCanvas.SetActive(false);
            _interactArea.SetColliderActivation(false);
            InteractionManager.Instance.StartedInteract(this);
            if(m_isFisrtDraw) 
                DrawManager.Instance.onDrawStart.AddListener(() => { DrawManager.Instance.BlackBook.OpenPanel(); });

            DrawManager.Instance.curDrawing = this;
            DrawManager.Instance.StartDrawing();
        }

        public override void FinishInteract() {
            //TODO: 카메라 종료 및 그림 그리기 기능 OFF
            _ptble.enabled = false;
            _interactCanvas.SetActive(true);
            _interactArea.SetColliderActivation(true);
            if(DrawManager.Instance.curDrawing == this)
                DrawManager.Instance.curDrawing = null;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
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

    }
}