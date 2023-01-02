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
            Debug.Log($"{hit.transform.tag}");
            if(hit.collider.CompareTag("Untagged")) {
                hit.transform.gameObject.SetActive(false);
                return;
            }
                
            if(hit.collider.CompareTag("Paintable")) {
                i_expectedDrawPos = hit.point;
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

    public void IS_Draw_ChangeSpraySize(InputAction.CallbackContext value) {
        float size = value.ReadValue<float>();
        var shape = e_sprayParticle.shape;
        shape.angle += size;
    }

    //TODO: 마우스 위치로 나갈 수 있도록 조절
    //public void ISE_MousePosition(InputAction.CallbackContext value) {
    //    Vector3 mousePos = value.ReadValue<Vector3>();
    //}

    public void IS_Draw_OnLeftClick(InputAction.CallbackContext value) {
        if(value.phase == InputActionPhase.Performed) {
            e_sprayParticle.Play();

        }

        if(value.phase == InputActionPhase.Canceled) {
            e_sprayParticle.Stop();
        }
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
        if(performed) {
            e_sprayParticle.Play();
        }
        else {
            e_sprayParticle.Stop();
        }
    }

    #endregion
}
