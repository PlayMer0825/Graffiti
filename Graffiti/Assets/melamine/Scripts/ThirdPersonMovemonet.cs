using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovemonet : MonoBehaviour
{
    public Transform cam;
    public CharacterController controller;

    //Player status speed
    public float speed = 5f;
    public float JumpPow;
    public float Player_Speed = 5f;
    public float Player_Run = 10f;
    public float Player_Sit = 2.5f;
    public float gravityMultiply = 1.0f;
    public float sitHeight;

    private Vector3 direction;
    private bool wasGround;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    private Vector3 otherVelocity;

    //status check
    private bool run = false;
    private bool sit = false;

    private float originHeight;
    private Vector3 originCenter;

    void Start()
    {
        JumpPow = 5.0f;
        Player_Speed = speed;
        controller = GetComponent<CharacterController>();

        wasGround = true;
        otherVelocity = Vector3.zero;
        originHeight = controller.height;
        originCenter = controller.center;
    }

    // Update is called once per frame
    void Update()
    {
        if(controller==null) return;
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction=new Vector3(horizontal, 0f, vertical).normalized;

        otherVelocity += Time.deltaTime * gravityMultiply * Physics.gravity;

        Jump();
       
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle=Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg+cam.eulerAngles.y;
            Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * speed;
            Vector3 velocity = otherVelocity + moveDir;
            controller.Move(velocity * Time.deltaTime);
        }
        else
        {
            controller.Move(otherVelocity * Time.deltaTime);
        }

        Run();
        Sit();
    }

    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (run == false && wasGround == true)
            {
                sit = false;
                speed = Player_Run;
                run = true;
                controller.height = originHeight;
                controller.center = originCenter;
            }
            else if (run == true)
            {
                speed = Player_Speed;
                run = false;
            }
        }
    }

    private void Sit()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (sit == false&&wasGround==true)
            {
                run = false;
                speed = Player_Sit;
                sit = true;

                controller.height = sitHeight;
                controller.center -= new Vector3(0.0f, sitHeight * 0.5f, 0.0f);
            }
            else if (sit == true)
            {
                speed = Player_Speed;
                sit = false;

                controller.height = originHeight;
                controller.center = originCenter;
            }
        }
    }

    private void Jump()
    {
        if (controller.isGrounded)
        {
            wasGround = true;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(sit == false)
                {
                    otherVelocity = JumpPow * Vector3.up;
                }
            }
        }
        else
        {
            if (wasGround)
            {
                sit = false;
                run = false;
                speed = Player_Speed;
                wasGround = false;
                if (Vector3.Dot(otherVelocity, Vector3.up) < 0.0f)
                {
                    otherVelocity = Time.deltaTime * gravityMultiply * Physics.gravity;
                }
            }
        }
    }

}
