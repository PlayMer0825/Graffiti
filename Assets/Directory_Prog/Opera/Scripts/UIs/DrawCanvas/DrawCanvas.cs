using OperaHouse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OperaHouse {
    public class DrawCanvas : MonoBehaviour {
        [Header("Spray")]
        [SerializeField] private Spray _spray = null;

        [Header("UIPanels")]
        [SerializeField] private GameObject _drawPanel = null;
        [SerializeField] private Bag _bagPanel = null;
        [SerializeField] private BlackBook _blackPanel = null;

        public Spray Spray { get => _spray; }

        private void Awake() {
            _bagPanel = GetComponentInChildren<Bag>();
            _blackPanel = GetComponentInChildren<BlackBook>();

        }

        public void OpenBlackBook() {
            _drawPanel.gameObject.SetActive(false);
            _blackPanel.gameObject.SetActive(true);
        }

        public void OpenBagPanel() {
            _drawPanel.gameObject.SetActive(false);
            _bagPanel.gameObject.SetActive(true);
        }

        public bool ClosePanel() {
            if(_bagPanel.isActiveAndEnabled) {
                _bagPanel.gameObject.SetActive(false);
                _drawPanel.gameObject.SetActive(true);
                return false;
            }
            if(_blackPanel.isActiveAndEnabled) {
                _blackPanel.gameObject.SetActive(false);
                _drawPanel.gameObject.SetActive(true);
                return false;
            }

            return true;
        }
    }
}