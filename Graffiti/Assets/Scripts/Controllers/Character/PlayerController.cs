using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : InputableObject {
    #region Components
    public CharacterController _controller = null;

    #endregion

    #region External Objects
    public Transform _camTransform = null;

    #endregion

    #region Variables
    [SerializeField] private MovementStat m_Stat;

    #endregion

    #region Unity Event Functions
    private void Awake() {
        if(_controller == null)
            _controller = GetComponent<CharacterController>();

        
    }

    private void Start() {
        InputManager.Instance.InterruptHandleInput(this);
    }

    #endregion

    #region Override Functions
    public override void HandleInput() {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        if(direction != Vector3.zero) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
            targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_Stat._turnSmoothVelocity, m_Stat._turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = transform.rotation * Vector3.forward;
            _controller.Move(moveDir.normalized * m_Stat._speed* Time.deltaTime);
        }
    }

    #endregion


}
