using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOut_Area : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().hideOut_Area = true;
            Debug.Log("hide");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().hideOut_Area = false;
        Debug.Log("out");
    }
}