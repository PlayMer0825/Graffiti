using OperaHouse;
using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class InteractionArea : MonoBehaviour {
        [SerializeField] private string _playerTag = "Player";
        [SerializeField] private Interactable _interactObj = null;
        private Collider _interactTrigger = null;


        private void Awake() {
            _interactTrigger = GetComponent<Collider>();
            _interactObj = GetComponentInParent<Interactable>();
            if(_interactObj == null)
                gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag(_playerTag) == false)
                return;

            _interactObj.ReadyInteract(other);
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag(_playerTag) == false)
                return;

            if(!gameObject.activeSelf)
                return;

            _interactObj.UnReadyInteract(other);
        }

        public void OnInteractClicked() {
            _interactObj.StartInteract();
        }

        public void SetColliderActivation(bool isActive) {
            _interactTrigger.enabled = isActive;
        }
    }
}