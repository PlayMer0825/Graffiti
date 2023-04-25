using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class UIPanel : MonoBehaviour {
        [SerializeField] private bool _isOpened = false;
        public bool IsOpened { get => _isOpened; }

        private void Awake() {
            InitPos();
        }

        public virtual void OpenPanel() {
            OnEnablePanel();
        }

        public virtual void ClosePanel() {
            if(IsOpened == false)
                return;

            OnDisablePanel();
        }

        protected virtual void OnEnablePanel() {
            _isOpened = true;
        }

        protected virtual void OnDisablePanel() {
            _isOpened = false;
        }

        protected virtual void InitPos() {

        }
    }
}