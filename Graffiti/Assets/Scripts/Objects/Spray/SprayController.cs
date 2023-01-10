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

    [Header("Spray Nozzle GameObject")]
    [SerializeField] Nozzle e_nozzle = null;

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
    private Vector3 i_expectedDrawPos = Vector3.zero;

    private WaitForSeconds i_rayCastInterval = null;
    #endregion

    #region Properties
    public bool IsFocusing { get => i_isFocusing; }

    #endregion

    #region Unity Event Functions
    private void Awake() { i_rayCastInterval = new WaitForSeconds(i_rayCastIntervalValue); }

    private void Update() {
        if(!i_isFocusing)
            return;

        RaycastHit hit;
        Debug.DrawRay(e_standardDir.position, e_standardDir.forward * i_sprayDistance, Color.green);
        if(Physics.Raycast(e_standardDir.position, e_standardDir.forward, out hit, i_sprayDistance, 1 << LayerMask.NameToLayer("Paintable"))) {
            i_expectedDrawPos = hit.point;
            i_isDrawable = true;
            e_sprayDrawer.enabled = true;
        }
        else {
            i_isDrawable = false;
            e_sprayDrawer.enabled = false;
            e_nozzle.Stop();
        }

        e_nozzle.LookAt(i_expectedDrawPos);
    }

    #endregion

    #region User Defined Functions

    public void ChangeColorTo(Color color) { e_nozzle.ChangeColor(color); }

    public void ChangeSprayNozzleSize(float wheelDelta) {
        ShapeModule shape = e_sprayParticle.shape;
        shape.angle = Mathf.Max(0.1f, Mathf.Min(120f, shape.angle + wheelDelta));
        SizeOverLifetimeModule sizeModule = e_sprayParticle.sizeOverLifetime;
        Debug.Log($"Size: {sizeModule.sizeMultiplier}");

        e_sprayDrawer.Radius += wheelDelta * 0.1f;
        //얘는 굳이 조절 안해줘도 될듯? 얘보단 Opacity조절하는게 더 쓸모있어보임
        //e_sprayDrawer.Scale = new Vector3(shape.angle, shape.angle, shape.angle);
    }

    public void OnFocus(bool performed) {
        if(performed) { i_isFocusing = true;  }
        else          { i_isFocusing = false; }
    }

    public void OnLeftClick(bool performed) {
        if(i_isFocusing == false)
            return;

        if(i_isDrawable == false)
            return;

        if(performed) { e_nozzle.Play(); }
        else          { e_nozzle.Stop(); }
    }

    #endregion
}
