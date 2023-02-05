using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerBrain))]
public class Player : MonoBehaviour {
    [SerializeField] private PlayerBrain e_brain = null;
    [SerializeField] private PlayerMovement e_movement = null;

    public Vector3 InputVector {
        get {
            if(e_movement == null)
                return Vector3.zero;

            return e_movement.InputVector;  
        }
        set {
            if(e_movement == null)
                return;

            e_movement.InputVector = value; 
        }
    }

    public PlayerBrain Brain { get => e_brain; }
    public PlayerMovement Movement {  get => e_movement; }


    private void Awake() {
        e_brain  = GetComponent<PlayerBrain>();
        e_movement = GetComponent<PlayerMovement>();
    }

    public void OnFocus(bool performed, bool sudoExit = false) {
        e_brain.OnFocus(performed, sudoExit);
        e_movement.OnFocus(performed, sudoExit);
    }

    public void MovementTypeChanged(Status type, bool performed, bool canceled, bool isToggle = false) {
        e_movement.MoveInput(type, performed, canceled, isToggle);
    }

    public void OnLeftClick(bool performed) {
        e_brain.OnLeftClick(performed);
    }

    public void OnWheelScroll(float scrollDelta) {
        e_brain.OnWheelScroll(scrollDelta);
    }

    public void ExitInteraction() {
        e_brain.OffInteract();
        e_brain.OnFocus(false, true);
        SwitchPaintUIActive(true);
        e_movement.OnFocus(false, true);
    }

    public void SwitchPaintUIActive(bool sudoExit = false) {
        e_movement.CanInput = e_brain.SwitchUIActivation(sudoExit);
    }
}
