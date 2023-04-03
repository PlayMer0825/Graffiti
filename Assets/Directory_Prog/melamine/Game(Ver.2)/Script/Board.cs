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

    private bool ground = false;
    public float jumpHeight = 3f;
    public float jumpHeights = 0f;

    private Vector3 dir = Vector3.zero;

    public LayerMask layer;
    void Start()
    {
        currentPosition_x = transform.position.x;
        rigidbody = this.GetComponent<Rigidbody>();
        jumpHeights = jumpHeight;
        diRection = direction;
    }
    void Update()
    {
        CheckGround();
        if (Input.GetKeyDown(KeyCode.W) && ground)
        {
            Debug.Log("jump");
            Vector3 jumpPower = Vector3.up * jumpHeights;
            rigidbody.AddForce(jumpPower, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.LeftShift))
            diRection = direction * dash;
        else
            diRection = direction;

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
        }
        else if(other.gameObject.name=="Right")
        {
            direction *= -1;
        }
    }

    void CheckGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + (Vector3.up * 0.2f), Vector3.down, out hit, 3f, layer))
        {
            Debug.Log("true");
            ground = true;
        }
        else
        {
            ground = false;
        }
    }
}
