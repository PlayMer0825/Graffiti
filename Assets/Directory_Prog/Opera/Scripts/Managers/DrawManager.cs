using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace OperaHouse {
    public enum CanvasType {
        Draw,
        Bag,
        BlackBook,
    }

    public class DrawManager :  Singleton<DrawManager>{
        [SerializeField] private DrawPanel _drawCanvas = null;
        public DrawPanel Draw {
            get {
                if(_drawCanvas == null)
                    _drawCanvas = GetComponentInChildren<DrawPanel>();

                return _drawCanvas;
            }
        }


        [SerializeField] private Bag _bagPanel = null;
        public Bag Bag {
            get {
                if(_bagPanel == null)
                    _bagPanel = GetComponentInChildren<Bag>();

                return _bagPanel;
            }
        }


        [SerializeField] private BlackBook _blackBookPanel = null;
        public BlackBook BlackBook {
            get {
                if(_blackBookPanel == null)
                    _blackBookPanel = GetComponentInChildren<BlackBook>();



                return _blackBookPanel;
            }
        }

        private bool _isSomethingOpened = false;

        protected override void Awake() {
            base.Awake();
        }

        private void Update() {
            if(InteractionManager.Instance.IsInteracting == false)
                return;

            if(Input.GetKeyDown(KeyCode.Tab)) {
                if(_isSomethingOpened) {
                    CloseAllPanels();
                    _isSomethingOpened = false;
                    return;
                }
                    
                OpenCanvas(CanvasType.Draw);
            }
        }

        public void OpenCanvas(CanvasType type) {
            switch(type) {
                case CanvasType.Draw: {
                    _drawCanvas.gameObject.SetActive(true);
                    _bagPanel.gameObject.SetActive(false);
                    _blackBookPanel.gameObject.SetActive(false);
                } break;

                case CanvasType.Bag: {
                    _bagPanel.gameObject.SetActive(true);
                    _drawCanvas.gameObject.SetActive(false);
                    _blackBookPanel.gameObject.SetActive(false);
                } break;

                case CanvasType.BlackBook: {
                    _blackBookPanel.gameObject.SetActive(true);
                    _drawCanvas.gameObject.SetActive(false);
                    _bagPanel.gameObject.SetActive(false);
                } break;

                default:break;
            }

            _isSomethingOpened = true;
        }

        public void CloseAllPanels() {
            _bagPanel.gameObject.SetActive(false);
            _drawCanvas.gameObject.SetActive(false);
            _blackBookPanel.gameObject.SetActive(false);
            _isSomethingOpened = false;
        }
    }
}
