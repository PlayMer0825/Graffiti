using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Changer : MonoBehaviour
{
    [SerializeField]
    public CinemachineVirtualCamera Fixed_Point;

    [SerializeField]
    public CinemachineVirtualCamera TPS;

    [SerializeField]
    public CinemachineVirtualCamera SIDE;

    public bool Area_Check = false;


    // Start is called before the first frame update
    void Start()
    {
        TPS.enabled = false;
        Fixed_Point.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().area == true)
            {
                GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state = false;
                GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = false;
                GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = true;
                Area_Check = true;
                TPS.enabled = true;
                Fixed_Point.enabled = false;
                SIDE.enabled = false;
            }
            else if (GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().area == false)
            {
                GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state = true;
                GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = true;
                GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = false;
                TPS.enabled = false;
                Fixed_Point.enabled = false;
                SIDE.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().area == true)
            {
                if (TPS.enabled == true)
                {
                    GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = true;
                    GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = false;
                    TPS.enabled = false;
                    Fixed_Point.enabled = true;
                }
                else if (Fixed_Point.enabled == true)
                {
                    GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = false;
                    GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = true;
                    TPS.enabled = false;
                    Fixed_Point.enabled = false;
                }
            }
        }

        if (GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().area == false && Area_Check == true)
        {
            GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state = true;
            GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = true;
            GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = false;
            Area_Check = false;
            TPS.enabled = false;
            Fixed_Point.enabled = false;
            SIDE.enabled = true;
        }

    }
}