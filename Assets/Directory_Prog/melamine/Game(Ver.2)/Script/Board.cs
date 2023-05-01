using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Board : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float currentPosition_x;
    public float direction = 3.0f; //이동속도+방향
    public float diRection = 0f;
    public float dash = 3f;
    public float dash_jump = 3f;
    public float animMoveWeightSpeed;

    public float decrease = -1.5f;

    private bool ground = false;
    private bool bench = false;
    public float jumpHeight = 3f;
    public float jumpHeights = 0f;
    private float animationMoveWeight;

    Animator animator;
    private Vector3 dir = Vector3.zero;

    public LayerMask layer;
    void Start()
    {
        currentPosition_x = transform.position.x;
        rigidbody = this.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        jumpHeights = jumpHeight;
        diRection = direction;
        decrease = -1.5f;
    }
    void Update()
    {
        CheckGround();
        AnimationUpdate();
        if (Input.GetKeyDown(KeyCode.W) && ground)
        {
            Debug.Log("jump");
            Vector3 jumpPower = Vector3.up * jumpHeights;
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
            animator.SetTrigger("isJumping");
        }

        if (Input.GetKey(KeyCode.LeftShift))
            diRection = direction * dash;
        else
            if(ground==true)
            {
                diRection = diRection+decrease*Time.deltaTime;
            }
       
        
           

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("TRUE");
            jumpHeights = jumpHeight * dash_jump;
        }
        else
            jumpHeights = jumpHeight;


        currentPosition_x += Time.deltaTime * diRection;
        transform.position = new Vector3(currentPosition_x, this.transform.position.y, this.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name=="Left")
        {
            direction *= -1;
            this.transform.localScale = new Vector3(1.0f, 1f, -1f);
            decrease = -1.5f;
        }
        else if(other.gameObject.name=="Right")
        {
            direction *= -1;
            this.transform.localScale = new Vector3(-1.0f, 1f, -1f);
            decrease = 1.5f;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.name == "Bench")
        {
            Debug.Log("Bench");
            bench = true;   
        }
    }


    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.25f, layer))
        {
            Debug.Log("true");
            animator.SetBool("isGround", true);
            bench = false;
            ground = true;
        }
        else
        {
            animator.SetBool("isGround", false);
            Debug.Log("false");
            ground = false;
        }
    }

    void AnimationUpdate()
    {
        if (bench == true)
        {
            animator.SetBool("isGround", true);
            animationMoveWeight += Time.deltaTime * animMoveWeightSpeed;
            if (animationMoveWeight > 1f)
                animationMoveWeight = 1f;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight < 0f)
                    animationMoveWeight = 0f;
            }
            else if (animationMoveWeight > 0.5f)
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
        animator.SetFloat("moveWeight", animationMoveWeight);
    }
}
