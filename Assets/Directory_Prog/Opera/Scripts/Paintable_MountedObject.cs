using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OperaHouse {
    public class Paintable_MountedObject : Paintable, Interactable {
        [SerializeField] private GameObject _fixedCam = null;

        public override void Awake() {
            //base.Awake();
            //_fixedCam = GetComponentInChildren<CinemachineVirtualCamera>().gameObject;
            //_fixedCam.SetActive(false);
        }

        public void StartInteract() {
            SetP3dPaintable(true);
            _fixedCam.SetActive(true);
        }

        public void FinishInteract() {
            SetP3dPaintable(false);
            _fixedCam.SetActive(true);
        }

        public override void QuestFinished() {
            
        }
    }
}