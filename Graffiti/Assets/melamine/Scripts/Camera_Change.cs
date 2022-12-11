using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_Change : MonoBehaviour
{
    Transform target = null;
    public bool Click_Check = false;
    public GameObject Player;
    public CinemachineVirtualCamera Player_Cam;
    public CinemachineVirtualCamera Draw_Cam;
    // Start is called before the first frame update
    void Start()
    {
        PlayerCamera();
    }
    void Update()
    {
        if(Player_Cam.enabled==false&&Draw_Cam.enabled==true)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerCamera();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(Click_Check==true)
            {
                DrawCamera();
            }
            
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
        Player.SetActive(true);
        Player_Cam.enabled = true;
        Draw_Cam.enabled = false;
    }
    public void DrawCamera()
    {
        Player.SetActive(false);
        Player_Cam.enabled = false;
        Draw_Cam.enabled = true;
    }
}
