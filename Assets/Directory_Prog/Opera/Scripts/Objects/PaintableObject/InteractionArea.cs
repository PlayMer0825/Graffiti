using Insomnia;
using PaintIn3D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insomnia {
    [RequireComponent(typeof(AudioSource))]
    public class InteractionArea : MonoBehaviour {
        [SerializeField] private AudioSource m_source = null;

        [SerializeField] private string _playerTag = "Player";
        [SerializeField] private Interactable m_itObj = null;
        private Collider m_itTrigger = null;
        private bool m_triggered = false;

        private void Awake() {
            m_source = GetComponent<AudioSource>();
            m_itTrigger = GetComponent<Collider>();
            m_itObj = GetComponentInParent<Interactable>();
            if(m_itObj == null)
                gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag(_playerTag) == false)
                return;

            m_itObj.ReadyInteract(other);
            if(m_triggered)
                return;
            m_triggered = true;

            if(m_source == null || m_source.clip == null)
                return;

            m_source.PlayOneShot(m_source.clip);
        }

        private void OnTriggerExit(Collider other) {
            if(other.CompareTag(_playerTag) == false)
                return;

            if(!gameObject.activeSelf)
                return;

            m_itObj.UnReadyInteract(other);
            m_triggered = false;
        }

        public void OnInteractClicked() {
            m_itObj.StartInteract();
        }

        public void SetColliderActivation(bool isActive) {
            m_itTrigger.enabled = isActive;
        }
    }
}