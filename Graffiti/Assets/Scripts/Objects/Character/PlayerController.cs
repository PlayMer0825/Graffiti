using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.InputSystem;

using static Define;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    #region Components
    public CharacterController _controller = null;

    #endregion

    #region External Objects
    public Transform _camTransform = null;

    #endregion

    #region Variables
    [SerializeField] private MovementStat m_Stat;
    private Vector3 m_moveDir = Vector3.zero;
    #endregion

    #region Unity Event Functions
    private void Awake() {
        if(_controller == null)
            _controller = GetComponent<CharacterController>();

    }

    private void Start() {
        //InputManager.Instance.InterruptHandleInput(this);
    }

    private void Update() {
        //_controller.Move(m_moveDir * m_Stat._speed * Time.deltaTime);
        Vector3 velocity = _controller.velocity;
        //Vector3 addForce = m_moveDir * m_Stat.walkSpeed * Time.deltaTime;
        //_controller.velocity.Set(velocity.x + addForce.x, velocity.y + addForce.y, velocity.z + addForce.z);
    }

    #endregion

    #region User Defined Functions
    private void Move() {
        if(_camTransform == null)
            return;

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if(direction != Vector3.zero) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
            targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_Stat.turnSmoothVelocity, m_Stat.turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = transform.rotation * Vector3.forward;
            //_controller.Move(moveDir.normalized * m_Stat.walkSpeed * Time.deltaTime);
        }
    }

    public void Move(InputAction.CallbackContext value) {
        if(_camTransform == null)
            return;

        Vector2 input = value.ReadValue<Vector2>();
        float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
        targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_Stat.turnSmoothVelocity, m_Stat.turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        m_moveDir = Quaternion.Euler(new Vector3(0f, _camTransform.eulerAngles.y, 0f)) * new Vector3(input.x, 0f, input.y);
    }

    private void Interact() {
        if(Input.GetKey(KeyCode.F)) {

        }
    }

    #endregion
}
