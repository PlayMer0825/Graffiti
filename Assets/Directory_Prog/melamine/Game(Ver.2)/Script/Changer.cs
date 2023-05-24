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

    private Point_In_Time _pointInTime = null;
    private PlayerMove_TPS _tps = null;
    private PlayerMove_SIDE _side = null;

    // Start is called before the first frame update
    void Start()
    {
        TPS.enabled = false;
        Fixed_Point.enabled = false;

        _pointInTime = GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>();
        _tps = GameObject.Find("Player").GetComponent<PlayerMove_TPS>();
        _side = GameObject.Find("Player").GetComponent<PlayerMove_SIDE>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(_pointInTime.area == true &&   Area_Check == true)
            {
                _pointInTime.state = false;
                if (TPS.enabled == true)
                {
                    animator?.SetBool("isTps", false);

                    _side.enabled = true;
                    _tps.enabled = false;
                    TPS.enabled = false;
                    Fixed_Point.enabled = true;
                }
                else if (Fixed_Point.enabled == true)
                {
                    animator?.SetBool("isTps", true);
                    _side.enabled = false;
                    _tps.enabled = true;
                    TPS.enabled = true;
                    Fixed_Point.enabled = false;
                }
            }

            if(_pointInTime.hideOut_Area == true)
            {
                _pointInTime.state = false;
                if (TPS.enabled == true)
                {
                    animator?.SetBool("isTps", false);
                    _side.enabled=true;
                    _tps.enabled=false;
                    TPS.enabled = false;
                    HideOut.enabled = true;
                    TranslateCamera(HideOut);
                    Debug.Log("fuck");
                }
                else if (HideOut.enabled == true)
                {
                    animator?.SetBool("isTps", true);
                    _side.enabled = false;
                    _tps.enabled = true;
                    TPS.enabled = true;
                    HideOut.enabled = false;
                    Debug.Log("you");
                }
            }
        }

        if (( _pointInTime.area == false && Area_Check == true)
            || _pointInTime.hideOut_Area == false&&Hide_Check==true)
        {
            _pointInTime.state = true;
            animator?.SetBool("isTps", false);
            _side.enabled = true;
            _tps.enabled = false;
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