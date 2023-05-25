using PaintIn3D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace OperaHouse {

    public class DrawManager :  Singleton<DrawManager>{
        [SerializeField] private DrawPanel _drawPanel = null;
        public DrawPanel Draw {
            get {
                if(_drawPanel == null)
                    _drawPanel = GetComponentInChildren<DrawPanel>();

                return _drawPanel;
            }
        }

        [SerializeField] private BagPanel _bagPanel = null;
        public BagPanel Bag {
            get {
                if(_bagPanel == null)
                    _bagPanel = GetComponentInChildren<BagPanel>();

                return _bagPanel;
            }
        }

        [SerializeField] private BlackBookPanel _blackBookPanel = null;
        public BlackBookPanel BlackBook {
            get {
                if(_blackBookPanel == null)
                    _blackBookPanel = GetComponentInChildren<BlackBookPanel>();



                return _blackBookPanel;
            }
        }

        [SerializeField] private Spray _spray = null;
        public Spray Spray { get => _spray; }

        [SerializeField]private StencilMask _stencil = null;
        public StencilMask Stencil { get => _stencil; }


        #region Melamine Works
        [SerializeField] private Point_Of_View _pointOfView = null;

        #endregion

        private bool _isDrawing = false;
        public bool IsDrawing { get => _isDrawing; }

        protected override void Awake() {
            base.Awake();
            _pointOfView = GameObject.Find("Player").GetComponent<Point_Of_View>();
        }

        private void Update() {
            if(InteractionManager.Instance.IsInteracting == false)
                return;

            if(Input.GetKeyDown(KeyCode.Tab)) {
                _bagPanel.OpenPanel();
            }

            if(Input.GetKeyDown(KeyCode.B)) {
                _blackBookPanel.OpenPanel();
            }
        }

        public void StartDrawing() {
            _isDrawing = true;
            _blackBookPanel.gameObject.SetActive(true);
            _blackBookPanel.ClosePanel();
            _bagPanel.gameObject.SetActive(true);
            _bagPanel.ClosePanel();
            _drawPanel.gameObject.SetActive(true);
            _drawPanel.OpenPanel();
            _pointOfView.ForceChangeToTps();

            P3dChangeCounter counter = InteractionManager.Instance.CurInteracting.gameObject.GetComponentInChildren<P3dChangeCounter>();
            if(counter == null) return;
            PercentageUI ui = _drawPanel.Percent;
            if(ui == null) return;
            ui.RegisterForChangeCounter(counter);
        }

        /// <summary>
        /// 상시 켜진 DrawPanel을 제외한 다른 패널이 켜져있는지 확인 후 없다면 true, 있다면 false를 반환.
        /// </summary>
        /// <returns></returns>
        public bool IsAnyPanelOpened() {
            return _bagPanel.IsOpened || _blackBookPanel.IsOpened;
        }

        public void FinishDrawing() {
            _isDrawing = false;
            _blackBookPanel.ClosePanel();
            _blackBookPanel.gameObject.SetActive(false);
            _bagPanel.ClosePanel();
            _bagPanel.gameObject.SetActive(false);
            _drawPanel.ClosePanel();
            _drawPanel.gameObject.SetActive(false);
            _spray.OnClickMouseLeft(false);
            _pointOfView.ForceChangeToSide();
            _drawPanel.Percent.ReleaseCounter();
            GameObject.Find("Player").GetComponent<Point_Of_View>().ForceChangeToSide();
        }
    }
}
