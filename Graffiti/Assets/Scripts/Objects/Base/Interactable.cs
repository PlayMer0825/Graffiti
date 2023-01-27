using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.Remoting.Messaging;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary> 상호작용 오브젝트 상위 클래스 </summary>
public class Interactable : MonoBehaviour {
    #region Components
    [Header("Interaction_GameObject")]
    [SerializeField] protected InteractArea m_interactArea = null;

    [Header("Interaction_UI")]
    [SerializeField] private Canvas m_interactCanvas = null;
    [SerializeField] private Button m_interactButton = null;

    #endregion

    #region External Objects
    public CinemachineVirtualCameraBase _focusCam = null;

    private static PlayerBrain _player = null;
    public PlayerBrain Player { get {
            if(_player == null) {
                Debug.LogError($"Interaction Object {gameObject.name}'s PlayerTest is not declared");
                gameObject.SetActive(false);
                return null;
            }
            return _player;
        } 
    }
    #endregion

    #region Variables
    private UnityAction m_OnInteract;

    #endregion

    #region Unity Event Functions
    private void OnValidate() {
        if(_focusCam == null) {
            Debug.LogError($"Interactable: {gameObject.name}'s FocusCam is not Validated!");
            gameObject.SetActive(false);
            return;
        }

        if(m_interactArea == null) {
            Debug.LogError($"Interaction Object {gameObject.name}'s InteractionArea is not Validated!");
            gameObject.SetActive(false);
            return;
        }

        if(m_interactButton == null) {
            Debug.LogError($"Interaction Object {gameObject.name}'s Button is not Validated!");
        }
    }

    private void Awake() {
        _player = FindObjectOfType<PlayerBrain>();

        if(_focusCam == null) {
            Debug.LogError($"Interactable: {gameObject.name}'s FocusCam is not Validated!");
            gameObject.SetActive(false);
            return;
        }

        if(m_interactArea == null) {
            Debug.LogError($"Interaction Object {gameObject.name}'s InteractionArea is not Validated!");
            gameObject.SetActive(false);
            return;
        }

        if(m_interactButton == null) {
            Debug.LogError($"Interaction Object {gameObject.name}'s Button is not Validated!");
        }

        SubscribeCallback_InteractArea();
        m_OnInteract = () => OnInteract();
        m_interactButton.onClick.AddListener(m_OnInteract);
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
    public virtual void OnInteract() {
        //bool success = _interactMgr.EnterInteractWithCurObj(this);
        //if(success == false)
        //    return;

        m_interactArea.SetColliderActive(false);
        m_interactCanvas.gameObject.SetActive(false);
        _focusCam.enabled = true;
    }

    public virtual void OffInteract() {
        m_interactArea.SetColliderActive(true);
        m_interactCanvas.gameObject.SetActive(true);
        _focusCam.enabled = false;
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
        m_interactButton.onClick.RemoveListener(m_OnInteract);
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

        //_interactMgr.ChangeCurIntObj(this);
        _player.Notify_OnInteractArea(this);
        m_interactButton.gameObject.SetActive(true);
        OnPlayerTriggered();
    }

    private void OnTriggerExitAction(Collider other) {
        if(!other.CompareTag("Player"))
            return;

        //_interactMgr.ExtractFromCurIntObj(this);
        _player.Notify_OffInteractArea(this);
        m_interactButton.gameObject.SetActive(false);
        OnPlayerUntriggered();
    }
    #endregion
}
