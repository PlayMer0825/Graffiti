using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMove_TPS : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    [SerializeField]
    private CinemachineVirtualCamera theCamera;

    private Rigidbody myRigid;

    public float jumpHeight = 3f;
    private bool ground = false;
    public LayerMask layer;

    public float animMoveWeightSpeed;
    private float animationMoveWeight;

    Vector3 _velocity;

    Animator animator;


    // Use this for initialization
    void Start()
    {
        myRigid = GetComponent<Rigidbody>();
        animationMoveWeight = 0f;
        animator=GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        //CameraRotation();
        //if(Input.GetKeyDown(KeyCode.LeftAlt))
        //CharacterRotation();

        CheckGround();

        if (Input.GetButtonDown("Jump") && ground)
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            myRigid.AddForce(jumpPower, ForceMode.VelocityChange);
        }

        AnimationUpdate();
    }

    void FixedUpdate()
    {
        CharacterRotation();
        CameraRotation();
    }
    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.smoothDeltaTime);
    }
    private void CharacterRotation()
    {
        Vector3 _characterRotationY;
        // 좌우 캐릭터 회전
        float _yRotation = Input.GetAxis("Mouse X");
        _characterRotationY = //Vector3.Lerp(_characterRotationY, new Vector3(0f, _yRotation, 0f) * lookSensitivity, 0f);
            lookSensitivity * Time.fixedDeltaTime * new Vector3(0f, _yRotation, 0f);
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        //Debug.Log(myRigid.rotation);
        //Debug.Log(myRigid.rotation.eulerAngles);
    }

    private void CameraRotation()
    {
        // 상하 카메라 회전
        float _xRotation = Input.GetAxis("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity*Time.fixedDeltaTime;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        //new Vector3(currentCameraRotationX, 0f, 0f);
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.4f, layer))
        {
            ground = true;
        }
        else
        {
            ground = false;
        }
    }

    void AnimationUpdate()
    {
        bool front = Input.GetKey(KeyCode.W);
        bool back=Input.GetKey(KeyCode.S);
        bool left=Input.GetKey(KeyCode.A);
        bool right=Input.GetKey(KeyCode.D);

        bool isMove = _velocity.x != 0 || _velocity.z != 0;
        
        if(isMove)
        {
            if(left)
            {
                animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight < 0f)
                    animationMoveWeight = 0f;
            }
            else if(right)
            {
                if (animationMoveWeight < 0.25f)
                {
                    animationMoveWeight += Time.deltaTime * animMoveWeightSpeed;
                    if (animationMoveWeight > 0.25f)
                        animationMoveWeight = 0.25f;
                }
                else if (animationMoveWeight > 0.25)
                {
                    animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
                    if (animationMoveWeight < 0.25f)
                        animationMoveWeight = 0.25f;
                }
            }
            else if (front) 
            {
                if (animationMoveWeight < 0.75f)
                {
                    animationMoveWeight += Time.deltaTime * animMoveWeightSpeed;
                    if (animationMoveWeight > 0.75f)
                        animationMoveWeight = 0.75f;
                }
                else if (animationMoveWeight > 0.75)
                {
                    animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
                    if (animationMoveWeight < 0.75f)
                        animationMoveWeight = 0.75f;
                }
            }
            else if (back)
            {
                animationMoveWeight += Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight > 1f)
                    animationMoveWeight = 1f;
            }
        }
        else
        {
            if(animationMoveWeight>0.5f)
            {
                animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight < 0.5f)
                    animationMoveWeight = 0.5f;
            }
            else if(animationMoveWeight<0.5f)
            {
                animationMoveWeight += Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight > 0.5f)
                    animationMoveWeight = 0.5f;
            }
            
        }

        animator.SetFloat("moveWeight_Tps", animationMoveWeight);

    }
}
