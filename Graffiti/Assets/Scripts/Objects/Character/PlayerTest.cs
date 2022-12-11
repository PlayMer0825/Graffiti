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
    [Header("Camera Objects")]
    [SerializeField] Transform _camTransform = null;

    [Header("Belonging Objects")]
    [SerializeField] SprayController _spray = null;

    #endregion

    #region Variables
    [SerializeField] private MovementStat m_Stat;
    [SerializeField] private MovementType m_curMoveType = MovementType.Walk;
    [SerializeField] private Vector3 m_inputDirNormal = Vector3.zero;
    [SerializeField] private Vector3 m_moveDir = Vector3.zero;
    [SerializeField] private Vector3 m_VerticalForce = Vector3.down;
    [SerializeField] private float m_currentSpeed = 0.0f;

    

    public bool _isRunning = false;
    public bool _isCrounching = false;
    [SerializeField] private byte _isBraking = 0;
    #endregion


    #region Unity Event Functions

    private void Update() {
        // PreCalculated Variables with byte Type for bit operating.
        byte isInputVoid   = (byte)( 1 - m_inputDirNormal.magnitude );
        byte isNotOpposite = (byte)( m_moveDir.normalized + m_inputDirNormal ).normalized.magnitude;
        _isBraking = (byte)(( _isBraking.Not() * isInputVoid.Not() * isNotOpposite.Not() * m_currentSpeed > m_Stat.moveType[(byte)MovementType.Walk].speed ? 1 : 0 ) + 
                            ( _isBraking       * m_currentSpeed > 1f ? 1 : 0));
        byte isDeceleration = (byte)Mathf.Min((m_currentSpeed > m_Stat.moveType[(byte)m_curMoveType].speed ? 1 : 0) + isNotOpposite.Not(), 1);

        {
            float targetAngle = Mathf.Atan2(isInputVoid.Not() * m_inputDirNormal.x + isInputVoid * m_moveDir.x,
                                        isInputVoid.Not() * m_inputDirNormal.z + isInputVoid * m_moveDir.z
                                        ) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
            targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_Stat.turnSmoothVelocity, m_Stat.turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }// Player Rotation By Input Direction

        {
            m_currentSpeed += m_Stat.moveType[(byte)m_curMoveType].acceleration * isInputVoid.Not() * isNotOpposite * isDeceleration.Not() * _isBraking.Not()              // Input값이 없을 경우 0, 있을 경우 현재 이동방향과 비교해서 반대방향일 경우 음수, 나머지는 전부 양수.
                            + m_Stat.moveType[(byte)m_curMoveType].damping * isInputVoid * -1   // Input값이 없을 경우 -damping 속도로 감속.
                            + m_Stat.moveType[(byte)m_curMoveType].damping * isInputVoid.Not() * isNotOpposite.Not() * _isBraking.Not() * -1
                            + m_Stat.moveType[(byte)m_curMoveType].damping * _isBraking * -1
                            + m_Stat.moveType[(byte)m_curMoveType].damping * isDeceleration * -1;
            m_currentSpeed = Mathf.Clamp(m_currentSpeed, 0.0f, m_Stat.moveType[(byte)MovementType.Run].speed);
            

            m_moveDir = m_moveDir * isInputVoid +                                                                // Input값이 없을 땐 m_moveDir을 그대로 유지한다.
                        m_inputDirNormal * isInputVoid.Not() * isNotOpposite +                                          // Input값이 현재 방향과 비슷한 흐름일 경우 Input방향으로 바꾼다.
                        m_moveDir * isInputVoid.Not() * isNotOpposite.Not() +                                    // Input값이 정반대일 경우 그대로 유지한다.
                        m_inputDirNormal * isInputVoid.Not() * isNotOpposite.Not() * (1 - _isBraking) * ( m_currentSpeed <= 0.5f ? 1 : 0 );   // 방향이 반대일 때 Damping되어서 현재 속도가 1f 이하로 떨어지면 Input방향으로 바꾼다.
        }// Calculating Player's Current Speed and Absolute moving direction 

        Debug.Log($"IsBraking: {_isBraking}");

        _controller.Move(m_moveDir.normalized * m_currentSpeed * Time.deltaTime + m_VerticalForce * 9.8f * Time.deltaTime);
    }

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

    public void OnRunning(InputAction.CallbackContext value) {
        //TODO: 지금은 Continuous 타입이지만 나중에 인게임에서 키매핑 수정할 수 있으면 Toggle도 추가해야됨.
        if(m_curMoveType == MovementType.Crouch)
            return;

        if(value.performed) {
            m_curMoveType = MovementType.Run;
        }
        else if(value.canceled) {
            m_curMoveType = MovementType.Walk;
        }
    }

    public void OnCrouch(InputAction.CallbackContext value) {
        //TODO: 지금은 Continuous 타입이지만 나중에 인게임에서 키매핑 수정할 수 있으면 Toggle도 추가해야됨.
        if(m_curMoveType == MovementType.Run)
            return;

        if(value.performed) {
            m_curMoveType = MovementType.Crouch;
        }
        else if(value.canceled) {
            m_curMoveType = MovementType.Walk;
        }
    }

    public void OnJump(InputAction.CallbackContext value) {
        if(!_controller.isGrounded)
            return;

        bool isJump = value.ReadValueAsButton();
        //TODO: 애니메이션 트리거를 여기 넣을까?

    }

    #endregion

    #region User Defined Functions



    #endregion
}
