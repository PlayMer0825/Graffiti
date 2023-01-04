using PaintIn3D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public class SprayController : MonoBehaviour {
    #region GameObjects
    [Header("Standard Transform")]
    [SerializeField] Transform e_standardDir = null;

    #endregion

    #region Components
    [SerializeField] private ParticleSystem e_sprayParticle = null;
    [SerializeField] private P3dPaintSphere e_sprayDrawer = null;

    #endregion


    #region Variables
    private bool i_isFocusing = false;
    private bool i_isDrawable = false;
    [SerializeField] private float i_rayCastIntervalValue = 0.1f;
    [SerializeField] private float i_sprayDistance = 5.0f;
    [SerializeField] private Vector3 i_expectedDrawPos = Vector3.zero;

    private WaitForSeconds i_rayCastInterval = null;
    #endregion

    #region Unity Event Functions
    private void Awake() {
        i_rayCastInterval = new WaitForSeconds(i_rayCastIntervalValue);
    }

    private void Update() {
        if(!i_isFocusing)
            return;

        RaycastHit hit;
        Debug.DrawRay(e_standardDir.position, e_standardDir.forward * i_sprayDistance, Color.green);
        if(Physics.Raycast(e_standardDir.position, e_standardDir.forward, out hit, i_sprayDistance)) {
            Debug.Log($"Tag: {hit.transform.tag}");
            if(hit.collider.CompareTag("Paintable")) {
                i_expectedDrawPos = hit.point;
                i_isDrawable = true;
            }
            else {
                i_isDrawable = false;
                e_sprayParticle.Stop();
            }
                
        }

        e_sprayParticle.transform.LookAt(i_expectedDrawPos);
    }

    #endregion

    #region User Defined Functions

    public void ChangeColorTo(Color color) {
        e_sprayParticle.startColor = color;
        e_sprayDrawer.Color = color;
    }

    public void OnFocus(bool performed) {
        if(performed) {
            i_isFocusing = true;
        }
        else {
            i_isFocusing = false;
        }
    }

    public void OnLeftClick(bool performed) {
        if(i_isFocusing == false)
            return;
        if(i_isDrawable == false)
            return;

        if(performed) {
            e_sprayParticle.Play();
        }
        else {
            e_sprayParticle.Stop();
        }
    }

    #endregion
}
