using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class DrawObject : Interactable {
        [Header("P3d Plugins")] 
        [SerializeField] [InspectorName("P3dPaintable")]
        private P3dPaintable _ptble = null;
        
        #region Unity Event Functions
        private void Awake() {
            _ptble = GetComponentInChildren<P3dPaintable>();


        }

        #endregion

        #region Interactable override functions
        public override void ReadyInteract() {
            base.ReadyInteract();

        }

        public override void UnReadyInteract() {
            base.UnReadyInteract();
        }

        public override void StartInteract() {
            //TODO: 카메라 이동 및 그림 그리기 기능 ON
            _ptble.enabled = true;
        }

        public override void FinishInteract() {
            //TODO: 카메라 종료 및 그림 그리기 기능 OFF
            _ptble.enabled = false;
        }

        #endregion
    }
}