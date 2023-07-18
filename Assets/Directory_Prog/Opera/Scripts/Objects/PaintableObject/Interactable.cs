using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Events;

namespace Insomnia {
    /// <summary>
    /// 상호작용 가능한 오브젝트에 대한 부모 클래스
    /// </summary>
    public class Interactable : MonoBehaviour {
        [Header("Interactable UIs")]
        [SerializeField] protected GameObject _interactCanvas = null;

        [Header("Interactable: Status")]
        [SerializeField] private bool m_isStandby = false;
        protected bool interactable = true;

        [SerializeField] protected UnityEvent onStartInteract = new UnityEvent();
        [SerializeField] protected UnityEvent onFinishInteract = new UnityEvent();
        
        protected bool IsStandby { get => m_isStandby; }

        public bool IsInteractable { get => interactable; set => interactable = value; }


        /// <summary>
        /// OnTriggerEnter()에서 호출하는 함수.
        /// </summary>
        public virtual void ReadyInteract(Collider other) {
            _interactCanvas.SetActive(true);
            m_isStandby = true;
        }

        /// <summary>
        /// OnTriggerExit()에서 호출하는 함수.
        /// </summary>
        public virtual void UnReadyInteract(Collider other) {
            _interactCanvas.SetActive(false);
            m_isStandby = false;
        }

        /// <summary>
        /// 상호작용 시작 버튼 클릭 시 호출하는 함수.
        /// </summary>
        public virtual void StartInteract() {
            Debug.Log("Interactable.StartInteract(): Not overrided");
        }

        /// <summary>
        /// 상호작용 종료 버튼 클릭 시 호출하는 함수.
        /// </summary>
        public virtual void FinishInteract() {
            Debug.Log("Interactable.FinishInteract(): Not overrided");
        }
    }
}
