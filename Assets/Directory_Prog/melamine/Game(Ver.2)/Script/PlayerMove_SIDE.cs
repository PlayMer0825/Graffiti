using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove_SIDE : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float speed = 10f;
    public float speeds=0;
    public float jumpHeight = 3f;
    public float dash = 2f;
    public float rotSpeed = 6f;
    public float animMoveWeightSpeed;

    private Vector3 dir = Vector3.zero;

    private bool ground = false;
    private bool isBorder;
    public static bool isLoad;
    private float animationMoveWeight;
    public LayerMask layer;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        if(MainMenu.loadGame==true)
        {
            this.transform.position = DataManager.Instance.data.playerPosition;
            MainMenu.loadGame = false;
        }
        rigidbody = this.GetComponent<Rigidbody>();
        animator=GetComponentInChildren<Animator>();
        animationMoveWeight= 0f;
        speeds = speed;
        isLoad = true;
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
        dir.Normalize();

        CheckGround();

        /*if (Input.GetButtonDown("Jump") && ground)
        {
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
        }*/
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speeds = speed * dash;
        }
        else
            speeds = speed;
        AnimationUpdate();
    }

    private void FixedUpdate()
    {
        StopToWall();
        if (dir != Vector3.zero)
        {
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x)
                || Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0, 1, 0);
            }
            transform.forward = Vector3.Lerp(transform.forward, dir.normalized, rotSpeed * Time.fixedDeltaTime);
        }
        if (!isBorder&&isLoad==true)
        {
            rigidbody.MovePosition(this.gameObject.transform.position + dir.normalized * speeds * Time.fixedDeltaTime);
        }
    }

    void StopToWall()
    {
        Debug.DrawRay(transform.position, transform.forward * 1, Color.red);
        isBorder = Physics.Raycast(transform.position, transform.forward, 1, LayerMask.GetMask("Wall"));
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
        bool isSprint = Input.GetKey(KeyCode.LeftShift);
        bool isMove = dir.x != 0 || dir.z != 0;

        if (isMove)
        {
            if (isSprint)
            {
                animationMoveWeight += Time.deltaTime * animMoveWeightSpeed;
                if (animationMoveWeight > 1f)
                    animationMoveWeight = 1f;
            }
            else
            {
                if (animationMoveWeight > 0.5f)
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
        }
        else
        {
            animationMoveWeight -= Time.deltaTime * animMoveWeightSpeed;
            if (animationMoveWeight < 0f) 
                animationMoveWeight = 0f;
        }

        animator.SetFloat("moveWeight_Side", animationMoveWeight);
    }

    private void OnTriggerStay(Collider other)
    {
        if(Telepoter_This.telepoter==true)
        {
            if (other.CompareTag("Secret"))
                Position_This.Secret = true;
            if (other.CompareTag("Back_Front"))
                Position_This.Back_Front = true;
            if (other.CompareTag("Back_Back"))
                Position_This.Back_Back = true;
            if (other.CompareTag("City"))
                Position_This.City = true;
            if(other.CompareTag("Riverside"))
                Position_This.Riverside = true;
        }
    }
}
