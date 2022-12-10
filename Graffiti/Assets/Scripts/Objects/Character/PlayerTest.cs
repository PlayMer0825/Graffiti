using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Define;


public class PlayerTest : MonoBehaviour {
    #region Components
    [SerializeField] private CharacterController _controller = null;

    #endregion

    #region External Objects
    [SerializeField] Transform _camTransform = null;

    #endregion

    #region Variables
    [SerializeField] private MovementStat m_Stat;
    [SerializeField] private Vector3 m_inputDirNormal = Vector3.zero;
    [SerializeField] private Vector3 m_moveDir = Vector3.zero;

    [SerializeField] private float m_currentSpeed = 0.0f;

    private byte isInputVoid = 0;
    private byte isNotOpposite = 0;
    #endregion


    #region Unity Event Functions
    private void Update() {
        isInputVoid   = (byte)( 1 - m_inputDirNormal.magnitude );
        isNotOpposite = (byte)( ( m_moveDir.normalized + m_inputDirNormal ).normalized.magnitude );

        float targetAngle = Mathf.Atan2(isInputVoid.Not() * m_inputDirNormal.x + 
                                        isInputVoid * m_moveDir.x, 
                                        isInputVoid.Not() * m_inputDirNormal.z + 
                                        isInputVoid * m_moveDir.z) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
        targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_Stat.turnSmoothVelocity, m_Stat.turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        m_currentSpeed += m_Stat.acceleration * isInputVoid.Not() * isNotOpposite              // Input값이 없을 경우 0, 있을 경우 현재 이동방향과 비교해서 반대방향일 경우 음수, 나머지는 전부 양수.
                        + m_Stat.damping      * isInputVoid                             * -1   // Input값이 없을 경우 -damping 속도로 감속.
                        + m_Stat.damping      * isInputVoid.Not() * isNotOpposite.Not() * -1;

        m_currentSpeed = Mathf.Clamp(m_currentSpeed, 0.0f, m_Stat.speed);

        m_moveDir = m_moveDir        * isInputVoid +                                                                // Input값이 없을 땐 m_moveDir을 그대로 유지한다.
                    m_inputDirNormal * isInputVoid.Not() * isNotOpposite +                                          // Input값이 현재 방향과 비슷한 흐름일 경우 Input방향으로 바꾼다.
                    m_moveDir        * isInputVoid.Not() * isNotOpposite.Not() +                                    // Input값이 정반대일 경우 그대로 유지한다.
                    m_inputDirNormal * isInputVoid.Not() * isNotOpposite.Not() * ( m_currentSpeed <= 1f ? 1 : 0);   // 방향이 반대일 때 Damping되어서 현재 속도가 1f 이하로 떨어지면 Input방향으로 바꾼다.

        if(m_currentSpeed <= 0.1f)
            return;
            
        _controller.Move(m_moveDir.normalized * m_currentSpeed * Time.deltaTime);
    }

    #endregion

    #region User Defined Functions
    /// <summary>
    /// 입력된 값을 <see cref="UnityEvent"/>에게서 받아옵니다.
    /// </summary>
    /// <param name="value"></param>
    public void OnMove(InputAction.CallbackContext value) {
        if(_camTransform == null)
            return;

        Vector3 inputRaw = value.ReadValue<Vector2>();
        inputRaw = Quaternion.Euler(new Vector3(0, _camTransform.eulerAngles.y, 0)) * new Vector3(inputRaw.x, 0, inputRaw.y);
        inputRaw.x = (float)Math.Round(inputRaw.x, 0);
        inputRaw.y = (float)Math.Round(inputRaw.y, 0);
        inputRaw.z = (float)Math.Round(inputRaw.z, 0);

        m_inputDirNormal = inputRaw.normalized;

    }
    #endregion
}
