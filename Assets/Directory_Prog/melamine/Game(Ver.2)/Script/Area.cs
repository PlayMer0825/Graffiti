using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().area = true;
            Debug.Log("d");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().area = false;
        Debug.Log("x");
    }
}