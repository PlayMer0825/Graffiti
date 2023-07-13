using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTransformSetting : MonoBehaviour
{
    public Transform targetTrasnform;
    public Vector3 initialPosition;

    public void OnEnable()
    {
        TransObjectPosition();
    }

    public void TransObjectPosition()
    {
        targetTrasnform.position = initialPosition;
    }
}
