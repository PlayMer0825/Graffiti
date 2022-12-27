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
    [SerializeField] private PlayerInput _inputSystem = null;
    [HideInInspector] private InputActionMap _actionWander = null;
    [HideInInspector] private InputActionMap _actionDraw = null;
    

    #endregion

    #region External Objects
    [Header("Camera Objects")]
    [SerializeField] private Transform t_mainCam = null;

    [Header("Owned Objects")]
    [SerializeField] private SprayController o_Spray = null;

    [Header("NonSerialized Objects")]
    [SerializeField] private InteractionManager _interManager = null;

    [Header("Current Interactable Object")]
    [SerializeField] private Interactable _interactable = null;
    //public Interactable 
    #endregion

    #region Variables
    [SerializeField] private MovementStat m_Stat;
    private MovementType m_curMoveType    = MovementType.Walk;
    private Vector3      m_inputDirNormal = Vector3.zero;
    private Vector3      m_moveDir        = Vector3.zero;
    private Vector3      m_VerticalForce  = Vector3.down;

    private float        m_currentSpeed   = 0.0f;
    private byte         _isBraking = 0;
    #endregion


    #region Unity Event Functions

    private void OnValidate() {
        _inputSystem = GetComponent<PlayerInput>();

        if(_inputSystem == null)
            Debug.LogError($"{this.name}: Can't FInd {typeof(PlayerInput)}");

        _actionWander = _inputSystem.actions.FindActionMap("Player_Wander");
        _actionDraw = _inputSystem.actions.FindActionMap("Player_Draw");
    }

    private void Awake() {
        _inputSystem = GetComponent<PlayerInput>();

        if(_inputSystem == null)
            Debug.LogError($"{this.name}: Can't FInd {typeof(PlayerInput)}");

    }

    private void Start() {
        _interManager = Managers.Interact;

        if(_interManager == null) {
            Debug.LogError($"{this.name}: Can't Find {typeof(InteractionManager)}");
            gameObject.SetActive(false);
            return;
        }
            
        _actionWander = _inputSystem.actions.FindActionMap("Player_Wander");
        _actionDraw = _inputSystem.actions.FindActionMap("Player_Draw");
    }

    private void Update() {
        //TODO: 점프 추가 시 기능 수정 필요.

        byte isInputVoid   = (byte)( 1 - m_inputDirNormal.magnitude );  
        // Input값이 Vector3.zero일 경우 true, 나머지는 false.

        byte isNotOpposite = (byte)( m_moveDir.normalized + m_inputDirNormal ).normalized.magnitude;    
        // 현재 Input값과 현재 m_moveDir값이 정반대 방향일 경우 true, 나머지는 false.

        _isBraking = (byte)(( _isBraking.Not() * isInputVoid.Not() * isNotOpposite.Not() * m_currentSpeed > m_Stat.moveType[(byte)MovementType.Walk].speed ? 1 : 0 ) + 
                            ( _isBraking       * m_currentSpeed > 1f ? 1 : 0)); 
        //Input값과 m_moveDir방향이 정반대거나 아직 Braking 상태일 경우 true, 나머지는 false.

        byte isDeceleration = (byte)Mathf.Min((m_currentSpeed > m_Stat.moveType[(byte)m_curMoveType].speed ? 1 : 0) + isNotOpposite.Not(), 1);  
        //높은 속도의 MovementType에서 낮은 속도의 MovementType으로 넘어왔을 때 현재 속도가 현재 MovementType의 최고 속도보다 높을 경우 true, 나머지는 false.

        {
            float targetAngle = Mathf.Atan2(isInputVoid.Not() * _isBraking.Not() * m_inputDirNormal.x + isInputVoid * m_moveDir.x + _isBraking * -m_moveDir.x,
                                        isInputVoid.Not() * _isBraking.Not() * m_inputDirNormal.z + isInputVoid * m_moveDir.z + _isBraking * -m_moveDir.z
                                        ) * Mathf.Rad2Deg + t_mainCam.eulerAngles.y;
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

        _controller.Move(m_moveDir.normalized * m_currentSpeed * Time.deltaTime + m_VerticalForce * 9.8f * Time.deltaTime);
    }

    #endregion

    #region User Defined Functions

    public void ChangeInputTypeTo(InputType type) {
        switch(type) {
            case InputType.Player_Wander: {
                _actionWander.Enable();
                _actionDraw.Disable();
            }break;
            case InputType.Player_Draw: {
                _actionWander.Disable();
                _actionDraw.Enable();
            }break;
        }
    }

    public void Notify_OnInteractArea(Interactable onRanged) {
        if(_interactable == onRanged)
            return;

        _interactable = onRanged;
    }

    public void Notify_OffInteractArea(Interactable outRanged) {
        if(_interactable != outRanged)
            return;

        _interactable = null;
    }


    #endregion

    #region InputSystem General Events
    public void IS_General_OnMove(InputAction.CallbackContext value) {
        if(t_mainCam == null)
            return;

        Vector3 inputRaw = value.ReadValue<Vector2>();
        inputRaw = Quaternion.Euler(new Vector3(0, t_mainCam.eulerAngles.y, 0)) * new Vector3(inputRaw.x, 0, inputRaw.y);
        inputRaw.x = (float)Math.Round(inputRaw.x, 0);
        inputRaw.y = (float)Math.Round(inputRaw.y, 0);
        inputRaw.z = (float)Math.Round(inputRaw.z, 0);

        m_inputDirNormal = inputRaw.normalized;
    }

    public void IS_General_OnRun(InputAction.CallbackContext value) {
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

    public void IS_General_OnCrouch(InputAction.CallbackContext value) {
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

    public void IS_General_OnJump(InputAction.CallbackContext value) {
        if(!_controller.isGrounded)
            return;

        //TODO: 점프 기능 구현 필요
        bool isJump = value.ReadValueAsButton();
    }

    #endregion

    #region InputSystem Wander Events
    public void IS_Wander_OnInteract(InputAction.CallbackContext value) {
        //if(value.phase != InputActionPhase.Performed && value.phase != InputActionPhase.Canceled)
        //    return;

        //if(_interManager == null)
        //    return;

        //bool success = _interManager.EnterInteractWithCurObj();
        ////TODO: InputActionMap 변경하기
        //if(!success)
        //    return;

        //_inputManager.SwitchCurrentActionMap("Player_Draw");
        //_actionWander.Disable();
        //_actionDraw.Enable();

        Debug.LogWarning($"IS_Wander_OnInteract: Testing Function");

        _interactable.OnInteract();

        //TODO: 여기서 자기 자신의 컴포넌트들 & 객체들 관리 우선적으로 실행.
    }

    #endregion

    #region InputSystem Draw Events
    public void IS_Draw_OnFocus(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnFocus: Not Implemented!");
    }

    public void IS_Draw_OnLeftClick(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnFocus: Not Implemented!");
    }

    public void IS_Draw_OnRightClick(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnFocus: Not Implemented!");
    }

    public void IS_Draw_OnMiddleClick(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnFocus: Not Implemented!");
    }

    public void IS_Draw_OnScroll(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnFocus: Not Implemented!");
    }

    public void IS_General_OnEscape(InputAction.CallbackContext value) {
        if(value.phase != InputActionPhase.Performed && value.phase != InputActionPhase.Canceled)
            return;

        Debug.LogWarning($"IS_General_OnEscape: Just Implemented for OffInteract");

        //TODO: 여기서 자기 자신의 컴포넌트들 & 객체들 관리 우선적으로 실행.

        _interactable.OffInteract();
    }

    public void IS_Draw_OnTab(InputAction.CallbackContext value) {
        Debug.LogError($"IS_Draw_OnFocus: Not Implemented!");
    }

    #endregion
}
