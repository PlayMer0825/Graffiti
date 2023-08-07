using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public static bool isBench=true;
    public static bool isEnd = false;
    private Rigidbody rigidbody;
    public float currentPosition_x;
    public float direction = 3.0f; //이동속도+방향
    public float diRection = 0f;
    public float dash = 4f;
    public float dash_jump = 3f; 
    public float animMoveWeightSpeed;
    private bool isJump = false;
    private bool isDash = false;

    public float decrease = -1.5f;

    private bool ground = false;
    private bool bench = false;
    public float jumpHeight = 5f;
    private float animationMoveWeight;

    public Slider slider_Dash;
    public float slider_speed = 4;
    public float minPos;
    public float maxPos;
    public RectTransform pass;
    private bool dash_Slider = false;
    public GameObject slider1;

    public GameObject Bench;
    public Slider slider_Balance;
    public float slider_Balance_Auto=150f;
    public float slider_Balance_Speed = 300f;
    private bool balance_Slider = false;
    public GameObject slider2;

    public float dashTime;
    private Coroutine dashCoroutine;

    public static bool bench_State = false;


    Animator animator;
    private Vector3 dir = Vector3.zero;

    public LayerMask layer;
    public LayerMask layer2;

    public AudioSource Rolling;
    public AudioSource Jump;
    public AudioSource Slide;
    public AudioSource Landing;
    public AudioSource Drag;
    public AudioSource Perfect;

    void Start()
    {
        isEnd = false;
        currentPosition_x = transform.position.x;
        rigidbody = this.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        diRection = direction;
        decrease = -1.5f;
        slider1.SetActive(false);
        slider2.SetActive(false);
    }
    void Update()
    {
        if(!isEnd)
        {
            diRection = diRection;
        }
        else if(isEnd)
        {
            diRection = 0;
        }
        CheckGround();
        //AnimationUpdate();
        if (Input.GetKeyDown(KeyCode.W) && ground && bench_State == false&&!isDash)
        {
            Jump.Play();
            animator.SetTrigger("isJumping");
            Debug.Log("jump");
            Vector3 jumpPower = Vector3.up * jumpHeight;
            rigidbody.AddForce(jumpPower, ForceMode.Impulse);
            Invoke("OnInvoke_Jump", 0.5f);
        }

        if (Input.GetMouseButtonDown(0) && dash_Slider == false&&ground&&!isDash)
        {
            
            slider1.SetActive(true);
            //animator.SetFloat("slide_Speed", 1.0f);
            //diRection = direction * dash;
            dash_Slider = true;
            SetDash();
            isDash = true;
        }
        if(balance_Slider == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                //Debug.Log("a");
                slider_Balance.value -= slider_Balance_Speed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                //Debug.Log("d");
                slider_Balance.value += slider_Balance_Speed * Time.deltaTime;
            }
        }
        else
        {
            if (ground == true && diRection >= 3)
            {
                diRection = diRection + decrease * Time.deltaTime;
            }
            else if (ground == false && diRection < 3)
            {
                diRection = 3;
            }
        }

        if (bench == true&&balance_Slider==false&&bench_State==false)
        {
            Slide.Play();
            balance_Slider = true;
            slider2.SetActive(true);
            SetBalance();
        }

        currentPosition_x += Time.deltaTime * diRection;
        transform.position = new Vector3(currentPosition_x, this.transform.position.y, this.transform.position.z);
    }

    public void SetDash()
    {
        if(dash_Slider==true)
        {
            slider_Dash.value = 0;
            //minPos = pass.anchoredPosition.x;
            //maxPos = pass.sizeDelta.x + minPos;
            StartCoroutine(Dash());
        }
    }
    public void SetBalance()
    {
        if(balance_Slider==true)
        {
            slider_Balance.value = 150;
            StartCoroutine(Balance());
        }
    }
    
    IEnumerator Dash()
    {
        yield return null;
        while (!(Input.GetMouseButtonUp(0) || slider_Dash.value == slider_Dash.maxValue))
        { 
            slider_Dash.value+=Time.deltaTime*slider_speed;
            yield return null;
        }
        if (slider_Dash.value >= minPos && slider_Dash.value <= maxPos)
        {
            effect.isEffect = true;
            animator.SetFloat("Slide_Speed", 1.5f);
            Debug.Log("ok");
            animator.SetTrigger("isSliding");
            Invoke("OnInvoke_Dash_Success", 0.45f);
            dash_Slider = false;
            slider1.SetActive(false);
            Invoke("OnInvoke_Dash", 1.0f);
            Perfect.Play();
        }
        else
        {
            animator.SetFloat("Slide_Speed", 1.0f);
            animator.SetTrigger("isSliding");
            Invoke("OnInvoke_Dash_Fail", 0.45f);
            dash_Slider = false;
            slider1.SetActive(false);
            Invoke("OnInvoke_Dash", 1.0f);
        }
    }

    IEnumerator Balance()
    {
        Debug.Log("Drag Playing");
        Drag.Play();
        yield return null;
        while (!(bench == false || slider_Balance.value == 0 || slider_Balance.value == 300))
        {
            yield return null;
            if (slider_Balance.value <= 150)
            {
                slider_Balance.value -= slider_Balance_Auto * Time.deltaTime;
            }
            else
                slider_Balance.value += slider_Balance_Auto * Time.deltaTime;
        }
        Drag.Stop();
        Debug.Log("Drag Stopped");
        if (slider_Balance.value >= 0 && slider_Balance.value <= 300)
        {
            yield return null;
            isBench = false;
            bench = false;
            isJump = false;
            balance_Slider = false;
            slider2.SetActive(false);
        }
        if (ground == true)
        {
            bench = false;
            isJump = true;
            balance_Slider = false;
            slider2.SetActive(false);
        }
    }




    public void FixedUpdate()
    {
        if(!ground)
        {
            rigidbody.AddForce(Vector3.down *10);
        }
        
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Bench")
        {
            bench = true;
            animator.SetBool("isBench", true);
        }
        else
            bench = false;

    }

    void CheckBalance()
    {
        if(balance_Slider==true)
        {
           
        }
    }
    void CheckGround()
    {
        
        RaycastHit hit;
        Debug.DrawRay(transform.position + (Vector3.up * 0.2f), Vector3.up*5f, Color.red);
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 0.25f, layer))
        {
            isBench = true;
            bench = false;
            animator.SetBool("isBench", false);
            ground = true;
            animator.SetBool("isGround", true);

            if(!Rolling.isPlaying)
            {
                Rolling.Play();
            }
            if(isJump==true)
            {
                Landing.Play();
                isJump = false;
            }
        }
        else
        {
            animator.SetBool("isGround", false);
            ground = false;
            Rolling.Stop();
        }

        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.up, out hit, 3f, layer2))
        {
            bench_State = true;
            Debug.Log("bench");
        }
        else
            bench_State = false;
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
            if (Input.GetMouseButton(0))
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

    void OnInvoke_Jump()
    {
        isJump = true;
    }

    void OnInvoke_Dash()
    {
        isDash = false;
        effect.isEffect = false;
    }

    void OnInvoke_Dash_Success()
    {
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
        dashCoroutine = StartCoroutine(Dash(true));
    }

    void OnInvoke_Dash_Fail()
    {
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
        }
        dashCoroutine = StartCoroutine(Dash(false));
    }

    IEnumerator Dash(bool isSuccess)
    {
        float time = 0f;
        var acc = isSuccess ? dash : -dash;
        while (time < dashTime)
        {
            diRection += acc * Time.deltaTime;
            if (diRection < 3)
                diRection = 3;
            else if (diRection > 24)
                diRection = 24;
            yield return null;
            time += Time.deltaTime;
        }
        dashCoroutine = null;
    }
}
