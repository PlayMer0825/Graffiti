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

    [SerializeField]
    public CinemachineVirtualCamera HideOut;

    public bool Area_Check = false;
    public bool Hide_Check = false;

    [SerializeField]
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        TPS.enabled = false;
        Fixed_Point.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().area == true &&   Area_Check == true)
            {
                GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state = false;
                if (TPS.enabled == true)
                {
                    animator?.SetBool("isTps", false);

                    GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = true;
                    GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = false;
                    TPS.enabled = false;
                    Fixed_Point.enabled = true;
                }
                else if (Fixed_Point.enabled == true)
                {
                    animator?.SetBool("isTps", true);
                    GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = false;
                    GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = true;
                    TPS.enabled = true;
                    Fixed_Point.enabled = false;
                }
            }

            if(GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().hideOut_Area == true)
            {
                GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state = false;
                if (TPS.enabled == true)
                {
                    animator?.SetBool("isTps", false);
                    GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled=true;
                    GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled=false;
                    TPS.enabled = false;
                    HideOut.enabled = true;
                    TranslateCamera(HideOut);
                    Debug.Log("fuck");
                }
                else if (HideOut.enabled == true)
                {
                    animator?.SetBool("isTps", true);
                    GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = false;
                    GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = true;
                    TPS.enabled = true;
                    HideOut.enabled = false;
                    Debug.Log("you");
                }
            }
        }

        if ((GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().area == false && Area_Check == true)
            ||GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().hideOut_Area == false&&Hide_Check==true)
        {
            GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state = true;
            animator?.SetBool("isTps", false);
            GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state = true;
            GameObject.Find("Player").GetComponent<PlayerMove_SIDE>().enabled = true;
            GameObject.Find("Player").GetComponent<PlayerMove_TPS>().enabled = false;
            Area_Check = false;
            Hide_Check= false;
            TPS.enabled = false;
            Fixed_Point.enabled = false;
            SIDE.enabled = true;
        }
    }

    private void TranslateCamera(string camName)
    {
        TranslateCamera(GameObject.Find(camName).GetComponent<CinemachineVirtualCamera>());
    }

    private void TranslateCamera(CinemachineVirtualCamera cam)
    {
        cam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);
    }

    public void MouseClicked()
    {
        animator?.SetBool("isTps", true);
        Area_Check = true;
        TPS.enabled = true;
        Fixed_Point.enabled = false;
        SIDE.enabled = false;
    }

    public void Exit()
    {
        animator?.SetBool("isTps", false);
        Area_Check = false;
        TPS.enabled = false;
        Fixed_Point.enabled = false;
        SIDE.enabled = true;
    }
}