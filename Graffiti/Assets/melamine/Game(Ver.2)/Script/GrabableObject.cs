using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class GrabableObject : MonoBehaviour
{
    private bool isGrab;
    private Rigidbody _rigidBody;
    private Collider _collider;

    public bool IsGrab { get { return isGrab; } }

    private void Awake()
    {
        isGrab = false;
        _rigidBody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void Grab()
    {
        isGrab = true;
        _rigidBody.useGravity = false;
        _rigidBody.isKinematic = true;
        _collider.isTrigger = true;
    }

    public void Put()
    {
        isGrab = false;
        _rigidBody.useGravity = true;
        _rigidBody.isKinematic = false;
        _collider.isTrigger = false;
    }

    public void FollowOnFixedTime(Transform follow, Quaternion rotation)
    {
        Vector3 followPosition = follow.position;

        _rigidBody.MovePosition(followPosition);
        _rigidBody.MoveRotation(rotation);
    }
}
