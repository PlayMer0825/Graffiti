using OperaHouse;
using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class InteractionArea : MonoBehaviour {
        [SerializeField] private string _playerTag = "Player";
        private Paintable_MountedObject _myPaintable = null;

        private void Awake() {
            _myPaintable = GetComponentInParent<Paintable_MountedObject>();
            if(_myPaintable == null)
                gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag(_playerTag) == false)
                return;

            //TODO: 상호작용 UI 활성화
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag(_playerTag) == false)
                return;

            if(!gameObject.activeSelf)
                return;

            //TODO: 상호작용 UI 비활성화
        }

        public void OnInteractClicked() {
            //TODO: 상호작용 UI 비활성화

            _myPaintable.StartInteract();
        }
    }
}