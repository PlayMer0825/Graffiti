using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Composites;

public class TPSMovement : MonoBehaviour
{
    [SerializeField] private Transform e_Cam = null;

    [SerializeField] private CharacterController i_controller = null;

    private Vector3 i_inputDir = Vector3.zero;

    private void Awake() {
        i_controller = GetComponent<CharacterController>();

    }

    private void Update() {
        Vector3 dir = e_Cam.transform.rotation * i_inputDir;

        i_controller.Move(dir * 5 * Time.deltaTime);
    }

    public void IS_Input_Movement(InputAction.CallbackContext value) {
        Vector2 rawInput = value.ReadValue<Vector2>();
        i_inputDir = new Vector3(rawInput.x, 0, rawInput.y);
        //Debug.Log($"value Type: {value.valueType}");
        //Debug.Log($"value: {value.ReadValue<Vector2>()}");
    }
}
