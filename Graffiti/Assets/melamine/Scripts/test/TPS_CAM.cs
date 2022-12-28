using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TPS_CAM : CinemachineVirtualCameraBase
{
    private Transform follow;
    private Transform lookAt;

    public override Transform Follow
    {
        get { return follow; }
        set { follow = value; }
    }

    public override Transform LookAt
    {
        get { return lookAt; }
        set { lookAt = value; } 
    }



    public override CameraState State { get { return default(CameraState); } }
    public override void InternalUpdateCameraState(Vector3 worldUp, float deltaTime)
    {
    }

}
