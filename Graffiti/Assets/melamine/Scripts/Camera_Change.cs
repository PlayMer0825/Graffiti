using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Change : MonoBehaviour
{
    Transform target = null;
    public bool Click_Check = false;
    public bool Draw_Check = false;
    public GameObject Player;
    public CinemachineVirtualCamera Player_Cam;
    public CinemachineVirtualCamera Draw_Cam;
    public CinemachineFreeLook TPS_Cam;
    public bool player_cam = true;
    public bool draw_cam = false;
    public bool tps_cam = false;
    // Start is called before the first frame update
    void Start()
    {
        Player_Cam.enabled = true;
        Draw_Cam.enabled = false;
        TPS_Cam.enabled = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerCamera();
        }
       
        if (Input.GetMouseButtonDown(0))
        {
            if(Click_Check==true)
            {
                DrawCamera();
            }
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            TPS_CAM();
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if(col.tag=="Player")
        {
            Click_Check = true;
            target = col.gameObject.transform;
            this.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        Click_Check = false;
        target = null;
        this.gameObject.GetComponent<Renderer>().material.color=Color.white;
    }
    public void PlayerCamera()
    {
        if(draw_cam==true)
        {
            //Player.SetActive(true);
            Player_Cam.enabled = true;
            player_cam = true;
            Draw_Cam.enabled = false;
            draw_cam = false;
        }
    }
    public void DrawCamera()
    {
        if(player_cam==true||tps_cam==true)
        {
            if (player_cam == true)
            {
                player_cam = false;
                Player_Cam.enabled = false;
            }
            if (tps_cam == true)
            {
                tps_cam = false;
                TPS_Cam.enabled = false;
            }
            //Player.SetActive(false);
            Draw_Cam.enabled = true;
            draw_cam = true;
        }
    }

    public void TPS_CAM()
    {
        if (player_cam == true && tps_cam == false && draw_cam == false)
        {
            Debug.Log("d");
            player_cam = false;
            Player_Cam.enabled = false;
            tps_cam = true;
            TPS_Cam.enabled = true;
        }
        else if (draw_cam == true && tps_cam == false)
        {
            Draw_Check = true;
            draw_cam = false;
            Draw_Cam.enabled = false;
            tps_cam = true;
            TPS_Cam.enabled = true;
        }
        else if (draw_cam == false && tps_cam == true&&Draw_Check==true)
        {
            draw_cam = true;
            Draw_Cam.enabled = true;
            tps_cam = false;
            TPS_Cam.enabled = false;
            Draw_Check = false;
        }
        else if (player_cam == false && tps_cam == true)
        {
            player_cam = true;
            Player_Cam.enabled = true;
            tps_cam = false;
            TPS_Cam.enabled = false;
        }
    }
}
