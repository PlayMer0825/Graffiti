using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
//using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float currentPosition_x;
    public float direction = 3.0f; //이동속도+방향
    void Start()
    {
        currentPosition_x = transform.position.x;
        rigidbody = this.GetComponent<Rigidbody>();
    }
    void Update()
    {
        currentPosition_x += Time.deltaTime * direction;
        transform.position = new Vector3(currentPosition_x, this.transform.position.y, this.transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car"))
            currentPosition_x = currentPosition_x - 235.0f;
        if (other.CompareTag("Car1"))
            currentPosition_x = currentPosition_x + 230.0f;
    }
}