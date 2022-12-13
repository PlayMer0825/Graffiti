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
    protected InteractArea m_interactArea = null;

    #endregion

    #region External Objects
    public CinemachineVirtualCamera _focusCam = null;
    private static InteractionManager _interactMgr = null;

    #endregion

    #region Unity Event Functions
    private void OnValidate() {
        _focusCam = GetComponentInChildren<CinemachineVirtualCamera>();

        if(_focusCam == null) {
            Debug.LogError($"Interactable: {gameObject.name}'s FocusCam is not Validated");
            gameObject.SetActive(false);
        }

        if(m_interactArea == null) {
            Debug.LogError($"Interaction Object {gameObject.name}'s InteractionArea is not declared");
            gameObject.SetActive(false);
            return;
        }
    }

    private void Awake() {
        _focusCam = GetComponentInChildren<CinemachineVirtualCamera>();

        if(_focusCam == null) {
            Debug.LogError($"Interactable: {gameObject.name}'s FocusCam is not Validated");
            gameObject.SetActive(false);
            return;
        }

        if(m_interactArea == null) {
            Debug.LogError($"Interaction Object {gameObject.name}'s InteractionArea is not declared");
            gameObject.SetActive(false);
            return;
        }

        SubscribeCallback_InteractArea();
    }

    private void Start() {
        _interactMgr = Managers.Interact;

        if(_interactMgr == null) {
            Debug.LogError($"Interactable: {gameObject.name}'s InteractManager is not Validated");
            gameObject.SetActive(false);
            return;
        }
    }

    private void OnDisable() {
        UnSubscribeCallback_InteractArea();
        DisableObject(); 
    }

    private void OnApplicationQuit() {
        UnSubscribeCallback_InteractArea();
        DisableObject(); 
    }

    #endregion

    #region Virtual Functions
    public virtual bool OnInteract(bool successed = true) {
        m_interactArea.enabled = false;
        _focusCam.enabled = true;
        return true & successed;
    }

    public virtual bool OffInteract(bool successed = true) {
        m_interactArea.enabled = true;
        _focusCam.enabled = false;
        return true & successed;
    }

    /// <summary>
    /// 플레이어가 트리거에 진입했을 때 해당 오브젝트가 처리할 내용
    /// </summary>
    protected virtual void OnPlayerTriggered() { }

    /// <summary>
    /// 플레이어가 트리거에서 탈출했을 때 해당 오브젝트가 처리할 내용
    /// </summary>
    protected virtual void OnPlayerUntriggered() { }

    protected virtual void DisableObject() {
        _interactMgr.ResetCurIntObj();
        UnSubscribeCallback_InteractArea();
    }

    #endregion

    #region User Defined Functions
    private void SubscribeCallback_InteractArea() {
        UnSubscribeCallback_InteractArea();
        m_interactArea.OnTriggerEnterAction += OnTriggerEnterAction;
        m_interactArea.OnTriggerExitAction += OnTriggerExitAction;
    }

    private void UnSubscribeCallback_InteractArea() {
        m_interactArea.OnTriggerEnterAction -= OnTriggerEnterAction;
        m_interactArea.OnTriggerExitAction -= OnTriggerExitAction;
    }

    private void OnTriggerEnterAction(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        _interactMgr.ChangeCurIntObj(this);
        OnPlayerTriggered();
    }

    private void OnTriggerExitAction(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        _interactMgr.ExtractFromCurIntObj(this);
        OnPlayerUntriggered();
    }
    #endregion
}
