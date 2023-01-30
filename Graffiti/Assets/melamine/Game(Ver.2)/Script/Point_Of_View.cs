using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditorInternal;
using UnityEditor.Experimental.Rendering;

public class Point_Of_View : MonoBehaviour
{
    public bool Side = true;
    public bool Tps = false;

    [SerializeField]
    public CinemachineVirtualCamera SIDE;

    [SerializeField]
    public CinemachineVirtualCamera TPS;
    // Start is called before the first frame update
    void Start()
    {
        Side = true;
        Tps = false;
        SIDE.enabled = true;
        TPS.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Point_In_Time").GetComponent<Point_In_Time>().state == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Side == true)
                    Tps_View();
                else if (Tps == true)
                    Side_View();
            }
        }
    }

    void Side_View()
    {
        Side = true;
        Tps = false;
        SIDE.enabled = true;
        TPS.enabled = false;
        gameObject.GetComponent<PlayerMove_SIDE>().enabled = true;
        gameObject.GetComponent<PlayerMove_TPS>().enabled = false;
    }

    void Tps_View()
    {
        Side = false;
        Tps = true;
        SIDE.enabled = false;
        TPS.enabled = true;
        gameObject.GetComponent<PlayerMove_SIDE>().enabled = false;
        gameObject.GetComponent<PlayerMove_TPS>().enabled = true;
    }
}
