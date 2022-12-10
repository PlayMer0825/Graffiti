using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static Define;

public class PlayerTest : MonoBehaviour {
    #region Components
    [SerializeField] private CapsuleCollider _col = null;
    [SerializeField] private Rigidbody _rigid = null;
    [SerializeField] private CharacterController _controller = null;

    #endregion

    #region External Objects
    [SerializeField] Transform _camTransform = null;

    #endregion

    #region Variables
    [SerializeField] private MovementStat m_Stat;
    [SerializeField] private Vector3 m_inputDir = Vector3.zero;
    [SerializeField] private Vector3 m_moveDir = Vector3.zero;

    private float m_currentSpeed = 0.0f;
    #endregion


    #region Unity Event Functions
    private void Update() {
        //TODO: Rotation ÇÊ¿ä
        m_currentSpeed += ( -1 + 2 * (m_moveDir.normalized + m_inputDir).normalized.magnitude * m_inputDir.magnitude) * m_Stat.acceleration;
        m_currentSpeed = Mathf.Clamp(m_currentSpeed, 0.0f, m_Stat.speed);

        m_moveDir += (m_inputDir.magnitude * m_inputDir * m_Stat.acceleration) + ((m_inputDir.magnitude - 1) * m_moveDir.normalized * m_Stat.acceleration);
        
        Debug.Log($"m_currentSpeed: {m_currentSpeed}");
        m_moveDir = Vector3.ClampMagnitude(m_moveDir, m_Stat.speed);
        _controller.Move(m_moveDir.normalized * m_currentSpeed * Time.deltaTime);
    }

    #endregion

    #region User Defined Functions
    public void OnMove(InputAction.CallbackContext value) {
        if(_camTransform == null)
            return;

        Vector2 input = value.ReadValue<Vector2>();
        float targetAngle = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
        //targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_Stat.turnSmoothVelocity, m_Stat.turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        m_inputDir = Quaternion.Euler(new Vector3(0f, _camTransform.eulerAngles.y, 0f)) * new Vector3(input.x, 0f, input.y);
    }
    #endregion
}
