using OperaHouse;
using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class InteractionArea : MonoBehaviour {
        [SerializeField] private string _playerTag = "Player";
        private Interactable _interactObj = null;

        private void Awake() {
            _interactObj = GetComponentInParent<Interactable>();
            if(_interactObj == null)
                gameObject.SetActive(false);
        }

        private void OnValidate() {
            _interactObj = GetComponentInParent<Interactable>();
            if(_interactObj == null)
                gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag(_playerTag) == false)
                return;

            _interactObj.ReadyInteract();
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag(_playerTag) == false)
                return;

            if(!gameObject.activeSelf)
                return;

            _interactObj.UnReadyInteract();
        }

        public void OnInteractClicked() {
            _interactObj.StartInteract();
        }
    }
}