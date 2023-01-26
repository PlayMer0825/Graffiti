using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom_In_Out : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera virtualCamera;
    float CameraDistance;
    CinemachineComponentBase componentBase;
    [SerializeField]
    float sensitivity = 14f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (componentBase == null)
        {
            componentBase = virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Body);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            CameraDistance = Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            if (componentBase is CinemachineFramingTransposer)
            {
                (componentBase as CinemachineFramingTransposer).m_CameraDistance -= CameraDistance;
            }
        }
    }
}
