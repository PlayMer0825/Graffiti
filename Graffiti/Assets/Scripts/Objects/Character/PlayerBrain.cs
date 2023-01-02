using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour {
    [Header("Player Cameras")]
    [SerializeField] private CinemachineVirtualCameraBase e_sideCam = null;
    [SerializeField] private CinemachineVirtualCameraBase e_tpsCam = null;

    [Header("Current Interactable Object")]
    [SerializeField] private Interactable e_curInteract = null;

    [Header("OnClickEvent Receiver")]
    [SerializeField] SprayController e_spray = null;


    #region Interaction Event Functions
    public void OnInteract() {
        if(e_curInteract == null)
            return;

        e_curInteract.OnInteract();

        //TODO: 여기서 자기 자신의 컴포넌트들 & 객체들 관리 실행.
        e_sideCam.enabled = false;
    }

    public void OffInteract() {
        if(e_curInteract == null)
            return;

        //TODO: 여기서 자기 자신의 컴포넌트들 & 객체들 관리 우선적으로 실행.
        e_sideCam.enabled = true;

        e_curInteract.OffInteract();
    }

    public void Notify_OnInteractArea(Interactable onRanged) {
        if(e_curInteract == onRanged)
            return;

        e_curInteract = onRanged;
    }

    public void Notify_OffInteractArea(Interactable outRanged) {
        if(e_curInteract != outRanged)
            return;

        e_curInteract = null;
    }

    #endregion

    #region Action Event Functions

    public void OnFocus(bool performed, bool sudoExit = false) {
        if(sudoExit) {
            Cursor.lockState = CursorLockMode.None;
            e_spray.OnFocus(false);
            e_tpsCam.enabled = false;
            return;
        }

        if(Cursor.lockState == CursorLockMode.None) {
            Cursor.lockState = CursorLockMode.Locked;
            e_spray.OnFocus(true);
            e_tpsCam.enabled = true;
        }
            
        else if(Cursor.lockState == CursorLockMode.Locked) {
            Cursor.lockState = CursorLockMode.None;
            e_spray.OnFocus(false);
            e_tpsCam.enabled = false;
        }
    }

    public void OnLeftClick(bool performed) {
        e_spray.OnLeftClick(performed);
    }

    #endregion
}
