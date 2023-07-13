using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using OperaHouse;
using DG.Tweening;
using System;
using UnityEngine.Events;

public class PlayerMove_TPS : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField] private float _curSensitivity = 100;
    [SerializeField] private float lookSensitivity = 100;
    [SerializeField] private float onAimSensitivity = 25f;
    [SerializeField] private float shakeSensitivity = 10f;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    [SerializeField]
    private GameObject theCamera;

    private Rigidbody myRigid;

    public float jumpHeight = 3f;
    private bool ground = false;
    public LayerMask layer;

    public float animMoveWeightSpeed;
    private float animationMoveWeight;

    Vector3 _velocity;

    private Animator animator;
    private DrawManager _drawManager;

    private CinemachineBrain _cam = null;

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
        animationMoveWeight = 0f;
        animator = GetComponentInChildren<Animator>();

        _cam = Camera.main.GetComponent<CinemachineBrain>();
    }

    void Start()
    {
        _drawManager = DrawManager.Instance;
    }

    private void OnEnable()
    {
        theCamera.SetActive(true);
    }

    private void OnDisable()
    {
        theCamera.SetActive(false);
    }

    void Update()
    {
        if (_drawManager.IsAnyPanelOpened())
            return;

        if(_drawManager.CanMove == false)
            return;

        Move();
        //CameraRotation();
        //if(Input.GetKeyDown(KeyCode.LeftAlt))
        //CharacterRotation();

        CheckGround();
        /*if (Input.GetButtonDown("Jump") && ground)
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            myRigid.AddForce(jumpPower, ForceMode.VelocityChange);
        }*/

        AnimationUpdate();

        if(Input.GetMouseButton(2))
            _curSensitivity = shakeSensitivity;
        else if(Input.GetMouseButton(1))
            _curSensitivity = onAimSensitivity;
        else
            _curSensitivity = lookSensitivity;

        CharacterRotation();
        CameraRotation();
    }

    void FixedUpdate()
    {
        if (_drawManager.IsAnyPanelOpened())
            return;

        if(_drawManager.CanMove == false)
            return;

        //_curSensitivity = Input.GetMouseButton(1) ? onAimSensitivity : lookSensitivity;

        //CharacterRotation();
        //CameraRotation();

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
        // �¿� ĳ���� ȸ��
        float _yRotation = Input.GetAxis("Mouse X");
        _characterRotationY = //Vector3.Lerp(_characterRotationY, new Vector3(0f, _yRotation, 0f) * lookSensitivity, 0f);
            _curSensitivity * Time.fixedDeltaTime * new Vector3(0f, _yRotation, 0f);
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
        //Debug.Log(myRigid.rotation);
        //Debug.Log(myRigid.rotation.eulerAngles);
    }

    private void CameraRotation()
    {
        // ���� ī�޶� ȸ��
        float _xRotation = Input.GetAxis("Mouse Y");
        float _cameraRotationX = _xRotation * _curSensitivity * Time.fixedDeltaTime;
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
        bool back = Input.GetKey(KeyCode.S);
        bool left = Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.D);

        bool isMove = _velocity.x != 0 || _velocity.z != 0;

        if (isMove)
        {
            if (left)
            {
                animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight < 0f)
                    animationMoveWeight = 0f;
            }
            else if (right)
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
            if (animationMoveWeight > 0.5f)
            {
                animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight < 0.5f)
                    animationMoveWeight = 0.5f;
            }
            else if (animationMoveWeight < 0.5f)
            {
                animationMoveWeight += Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight > 0.5f)
                    animationMoveWeight = 0.5f;
            }

        }

        animator.SetFloat("moveWeight_Tps", animationMoveWeight);

    }
}
