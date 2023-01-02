using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using static Define;

public class PlayerInputHandler : MonoBehaviour {
    [SerializeField] private PlayerMovement e_playerMovement = null;
    [SerializeField] private PlayerBrain    e_playerBrain    = null;

    #region InputSystem General Events
    public void IS_General_OnMove(InputAction.CallbackContext value) {
        if(e_playerMovement == null)
            return;
        Vector3 inputRaw = value.ReadValue<Vector2>();
        e_playerMovement.InputVector = inputRaw;
    }

    public void IS_General_OnRun(InputAction.CallbackContext value) {
        //TODO: 2022-12-28 입력받을 때 토글인지, 활성화인지 체크할 건데 performed를 주는게 맞는지 체크하기
        e_playerMovement.ControlMovementType(MovementType.Run, value.performed, value.canceled);
    }

    public void IS_General_OnCrouch(InputAction.CallbackContext value) {
        //TODO: 2022-12-28 입력받을 때 토글인지, 활성화인지 체크할 건데 performed를 주는게 맞는지 체크하기
        e_playerMovement.ControlMovementType(MovementType.Crouch, value.performed, value.canceled);
    }

    public void IS_General_OnJump(InputAction.CallbackContext value) {

    }

    #endregion

    #region InputSystem Wander Events

    #endregion

    #region InputSystem Draw Events
    public void IS_Draw_OnFocus(InputAction.CallbackContext value) {
        e_playerBrain.OnFocus(value.performed);
    }

    public void IS_Draw_OnLeftClick(InputAction.CallbackContext value) {
        //Debug.LogError($"IS_Draw_OnLeftClick: Not Implemented!");
        Debug.LogWarning($"IS_Draw_OnLeftClick Should be Changed!");

        e_playerBrain.OnLeftClick(value.performed);
    }

    public void IS_Draw_OnRightClick(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnRightClick: Not Implemented!");
    }

    public void IS_Draw_OnMiddleClick(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnMiddleClick: Not Implemented!");
    }

    public void IS_Draw_OnScroll(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnScroll: Not Implemented!");
    }

    public void IS_General_OnEscape(InputAction.CallbackContext value) {
        if(value.phase != InputActionPhase.Started && value.phase != InputActionPhase.Canceled)
            return;

        e_playerBrain.OffInteract();
        e_playerBrain.OnFocus(false, true);
    }

    public void IS_Draw_OnTab(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnTab: Not Implemented!");
    }

    #endregion
}
