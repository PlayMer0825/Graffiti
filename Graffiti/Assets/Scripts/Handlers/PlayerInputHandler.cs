using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem;
using static Define;
using System.Linq;

public class PlayerInputHandler : MonoBehaviour {
    #region Components
    [SerializeField] private Player e_player = null;
    [SerializeField] private PlayerInput e_input = null;
    private List<InputActionMap> i_actionMaps = new List<InputActionMap>();

    bool i_isInitialized = false;
    #endregion

    #region Unity Event Functions
    private void Awake() {
        e_player = GetComponent<Player>();
        if(e_player == null)
            return;

        e_input = GetComponent<PlayerInput>();
        if(e_input == null)
            return;

        i_actionMaps = e_input.actions.actionMaps.ToList();

        i_isInitialized = true;
    }

    #endregion

    #region User Defined Functions
    public void ChangeInputState(Define.InputType type) {
        for(int i = 0; i < i_actionMaps.Count; i++) {
            i_actionMaps[i].Disable();
        }

        i_actionMaps[(int)type].Enable();
    }

    #endregion

    #region InputSystem General Events
    public void IS_General_OnMove(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        Vector3 inputRaw = value.ReadValue<Vector2>();
        e_player.InputVector = inputRaw;
    }

    public void IS_General_OnRun(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        e_player.MovementTypeChanged(Status.Run, value.performed, value.canceled);
    }

    public void IS_General_OnCrouch(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        e_player.MovementTypeChanged(Status.Crouch, value.performed, value.canceled);
    }

    public void IS_General_OnJump(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;


    }

    public void IS_General_OnEscape(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        if(value.phase != InputActionPhase.Started && value.phase != InputActionPhase.Canceled)
            return;

        //TODO: 이 기능은 Wander에서 게임 종료 메뉴를 띄우는게 맞을 듯?
    }

    #endregion

    #region InputSystem Draw Events
    public void IS_Draw_OnFocus(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        e_player.OnFocus(value.performed);
    }

    public void IS_Draw_OnLeftClick(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        e_player.OnLeftClick(value.performed);
    }

    public void IS_Draw_OnRightClick(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        Debug.LogError($"IS_Draw_OnRightClick: Not Implemented!");
    }

    public void IS_Draw_OnMiddleClick(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        Debug.LogError($"IS_Draw_OnMiddleClick: Not Implemented!");
    }

    public void IS_Draw_OnScroll(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        if(value.phase != InputActionPhase.Started)
            return;

        float scrollDelta = value.ReadValue<float>() < 0 ? -0.1f : 0.1f;
        e_player.OnWheelScroll(scrollDelta);
    }

    public void IS_Draw_SwitchUIActivation(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        if(!value.performed)
            return;

        e_player.SwitchPaintUIActive();
    }

    #endregion

    #region Player_PaintUI
    public void IS_PaintUI_OnEscape(InputAction.CallbackContext value) {
        if(i_isInitialized == false)
            return;

        e_player.SwitchPaintUIActive(true);
    }

    #endregion
}
