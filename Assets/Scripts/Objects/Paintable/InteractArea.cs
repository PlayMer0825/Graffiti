using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractArea : MonoBehaviour {
    [Tooltip("This Collider Must be checked [isTrigger]")]
    [SerializeField] private Collider _interactRange = null;
    public Action<Collider> OnTriggerEnterAction = null;
    public Action<Collider> OnTriggerExitAction = null;

    #region Unity Event Functions
    private void OnValidate() {
        _interactRange= GetComponent<Collider>();

        if(_interactRange == null) {
            Debug.LogError($"InteractArea: {gameObject.name}'s InteractRange Trigger is not Validated");
            gameObject.SetActive(false);
        }
    }
    private void Awake() {
        _interactRange = GetComponent<Collider>();

        if(_interactRange == null) {
            Debug.LogError($"InteractArea: {gameObject.name}'s InteractRange Trigger is not Validated");
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other) { OnTriggerEnterAction?.Invoke(other); }
    private void OnTriggerExit(Collider other) { OnTriggerExitAction?.Invoke(other); }

    #endregion

    #region User Defined Functions
    public void SetColliderActive(bool isActive) {
        _interactRange.enabled = isActive;
    }

    #endregion
}
