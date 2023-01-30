using System;
using UnityEngine;
using static Define;

public class PlayerMovement : MonoBehaviour {
    #region Components
    [SerializeField] private CharacterController e_Controller = null;

    #endregion

    #region External Objects
    [Header("Target Direction Object")]
    [SerializeField] private Transform e_targetTransform = null;

    #endregion

    #region Variables
    [SerializeField] private MovementStat i_moveStat;
                     private Status i_curMoveType    = Status.Walk;
    [SerializeField] private Vector3      i_inputDirNormal = Vector3.zero;
    [SerializeField] private Vector3      i_moveDir        = Vector3.zero;
                     private Vector3 i_verticalForce  = Vector3.down;

    [SerializeField] private float        i_currentSpeed   = 0.0f;
                     private byte         i_isBraking = 0;

    [SerializeField] private bool i_isFocusing = false;


    #endregion

    #region Properties
    public Vector3 InputVector { get; set; }
    public Status CurrentMovementType { get => i_curMoveType; set { i_curMoveType = value; } }
    public bool CanInput = true;

    #endregion

    #region Unity Event Functions
    private void Awake() {
        //Cursor.lockState= CursorLockMode.Locked;
    }
    private void Update() {
        if(CanInput == false)
            return;

        if(i_isFocusing) {
            transform.rotation = Quaternion.Euler(0f, e_targetTransform.eulerAngles.y, 0f);

            i_moveDir = transform.rotation * new Vector3(InputVector.x, 0, InputVector.y);
            i_currentSpeed = i_moveStat.moveType[(int)i_curMoveType].speed;
        }
        else {
            Vector3 inputRaw = InputVector;
            inputRaw = Quaternion.Euler(new Vector3(0, e_targetTransform.eulerAngles.y, 0)) * new Vector3(inputRaw.x, 0, inputRaw.y);
            inputRaw.x = (float)Math.Round(inputRaw.x, 0);
            inputRaw.y = (float)Math.Round(inputRaw.y, 0);
            inputRaw.z = (float)Math.Round(inputRaw.z, 0);

            i_inputDirNormal = inputRaw.normalized;

            //TODO: 점프 추가 시 기능 수정 필요.
            byte isInputVoid   = (byte)( 1 - i_inputDirNormal.magnitude );
            // Input값이 Vector3.zero일 경우 true, 나머지는 false.

            byte isNotOpposite = (byte)( i_moveDir.normalized + i_inputDirNormal ).normalized.magnitude;
            // 현재 Input값과 현재 m_moveDir값이 정반대 방향일 경우 true, 나머지는 false.

            i_isBraking = (byte)( ( i_isBraking.Not() * isInputVoid.Not() * isNotOpposite.Not() * i_currentSpeed > i_moveStat.moveType[(byte)Status.Walk].speed ? 1 : 0 )
                                  + ( i_isBraking * i_currentSpeed > 0.1f ? 1 : 0 )
                                );
            //Input값과 m_moveDir방향이 정반대거나 아직 Braking 상태일 경우 true, 나머지는 false.

            byte isDeceleration = (byte)Mathf.Min((i_currentSpeed > i_moveStat.moveType[(byte)i_curMoveType].speed ? 1 : 0) + isNotOpposite.Not(), 1);
            //높은 속도의 MovementType에서 낮은 속도의 MovementType으로 넘어왔을 때 현재 속도가 현재 MovementType의 최고 속도보다 높을 경우 true, 나머지는 false.

            {
                float targetAngle = Mathf.Atan2(isInputVoid.Not() * i_isBraking.Not() * i_inputDirNormal.x + isInputVoid * i_moveDir.x + i_isBraking * -i_moveDir.x,
                                                isInputVoid.Not() * i_isBraking.Not() * i_inputDirNormal.z + isInputVoid * i_moveDir.z + i_isBraking * -i_moveDir.z
                                                ) * Mathf.Rad2Deg + e_targetTransform.eulerAngles.y;
                targetAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref i_moveStat.turnSmoothVelocity, i_moveStat.turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            }// Player Rotation By Input Direction

            {
                i_currentSpeed += i_moveStat.moveType[(byte)i_curMoveType].acceleration * isInputVoid.Not() * isNotOpposite * isDeceleration.Not() * i_isBraking.Not()              // Input값이 없을 경우 0, 있을 경우 현재 이동방향과 비교해서 반대방향일 경우 음수, 나머지는 전부 양수.
                                + i_moveStat.moveType[(byte)i_curMoveType].damping * isInputVoid * -1   // Input값이 없을 경우 -damping 속도로 감속.
                                + i_moveStat.moveType[(byte)i_curMoveType].damping * isInputVoid.Not() * isNotOpposite.Not() * i_isBraking.Not() * -1
                                + i_moveStat.moveType[(byte)i_curMoveType].damping * i_isBraking * -1
                                + i_moveStat.moveType[(byte)i_curMoveType].damping * isDeceleration * -1;
                i_currentSpeed = Mathf.Clamp(i_currentSpeed, 0.0f, i_moveStat.moveType[(byte)Status.Run].speed);

                i_moveDir = Vector3.zero * isInputVoid * ( i_currentSpeed <= 0 ? 1 : 0 ) +
                            i_moveDir * isInputVoid +                                                                // Input값이 없을 땐 m_moveDir을 그대로 유지한다.
                            i_inputDirNormal * isInputVoid.Not() * isNotOpposite +                                   // Input값이 현재 방향과 비슷한 흐름일 경우 Input방향으로 바꾼다.
                            i_moveDir * isInputVoid.Not() * isNotOpposite.Not() +                                    // Input값이 정반대일 경우 그대로 유지한다.
                            i_inputDirNormal * isInputVoid.Not() * isNotOpposite.Not() * ( 1 - i_isBraking ) * ( i_currentSpeed <= 0.1f ? 1 : 0 );   // 방향이 반대일 때 Damping되어서 현재 속도가 1f 이하로 떨어지면 Input방향으로 바꾼다.
            }// Calculating Player's Current Speed and Absolute moving direction 
        }

        e_Controller.Move(i_moveDir.normalized * i_currentSpeed * Time.deltaTime);
    }

    #endregion

    #region User Defined Functions
    public void MoveInput(Status type, bool performed, bool canceled, bool isToggle = false) {
        if(i_curMoveType != Status.Walk && i_curMoveType != type)
            return;

        i_curMoveType = performed ? type : Status.Walk;
    }

    public void OnFocus(bool performed, bool sudoExit = false) {
        if(sudoExit) {
            i_isFocusing = false;
            return;
        }

        if(performed == false)
            return;

        i_isFocusing = performed;
    }

    #endregion
}
