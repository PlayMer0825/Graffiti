using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.Remoting.Messaging;
using UnityEditor.UIElements;
using UnityEngine;

/// <summary> 상호작용 오브젝트 상위 클래스 </summary>
public class Interactable : MonoBehaviour {
    #region Components
    [SerializeField]
    protected Collider m_interactionTrigger = null;
    public BoxCollider test = null;
    #endregion

    #region External Objects
    private static InteractionManager s_intManager = null;
    public CinemachineVirtualCamera _onInteractCam = null;
    #endregion

    #region Unity Event Functions
    private void OnValidate() {
        s_intManager = FindObjectOfType<InteractionManager>();

        if(s_intManager == null)
            Debug.LogError($"Interactable: {gameObject.name}'s InteractionManager is not Validated");
    }

    private void Awake() {
        if(m_interactionTrigger == null) {
            Debug.LogError($"Interaction Object {gameObject.name}'s InteractionArea is not declared");
            return;
        }

        m_interactionTrigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        s_intManager.ChangeCurIntObj(this);
    }

    private void OnTriggerExit(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        s_intManager.ExtractFromCurIntObj(this);
    }

    private void OnDisable() { DisableObject(); }
    private void OnApplicationQuit() { DisableObject(); }

    #endregion


    #region Virtual Functions
    protected virtual void OnInteract() { m_interactionTrigger.enabled = false; }
    protected virtual void OffInteract() { m_interactionTrigger.enabled = true; }
    protected virtual void DisableObject() {
        if(s_intManager == null)
            return;

        s_intManager.ExtractFromCurIntObj(this);

        s_intManager = null; 
    }

    #endregion
}
