using PaintIn3D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class SprayController : MonoBehaviour {
    #region GameObjects
    [Header("Spray UI")]
    [SerializeField] private GameObject e_sprayUI = null;
    [SerializeField] private Slider e_sprayLeftOverUI = null;
    [SerializeField] private Image e_sprayLeftOverFillUI = null;

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

    [SerializeField] private float i_sprayLeftover = 1500.0f;
    public float SprayLeftOver { get => i_sprayLeftover; }

    [SerializeField] private float i_sprayDecreaseAmount = 1.3f;
    [SerializeField] private float i_sprayDecreaseInterval = 0.5f;
    private WaitForSeconds i_sprayDecreaseIntervalWait = null;
    private bool i_isSpraying = false;

    #endregion

    #region Properties
    public bool IsFocusing { get => i_isFocusing; }

    #endregion

    #region Unity Event Functions
    private void Awake() {
        i_sprayDecreaseIntervalWait = new WaitForSeconds(i_sprayDecreaseInterval);
    }

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

    public void ChangeColorTo(Color color) { 
        e_nozzle.ChangeColor(color);
        e_sprayLeftOverFillUI.color = color;
    }

    public void ChangeSprayNozzleSize(float wheelDelta) {
        //ShapeModule shape = e_sprayParticle.shape;
        //shape.angle = Mathf.Max(0.1f, Mathf.Min(120f, shape.angle + wheelDelta));
        SizeOverLifetimeModule sizeModule = e_sprayParticle.sizeOverLifetime;
        Debug.Log($"Size: {sizeModule.sizeMultiplier}");

        e_sprayDrawer.Radius = Mathf.Clamp(e_sprayDrawer.Radius + wheelDelta * 0.1f, 0.04f, 0.5f);
        //얘는 굳이 조절 안해줘도 될듯? 얘보단 Opacity조절하는게 더 쓸모있어보임
        //e_sprayDrawer.Scale = new Vector3(shape.angle, shape.angle, shape.angle);
    }

    public void ChangeSprayNozzleSizeWithSlider(float value) {
        e_sprayDrawer.Radius = value;
    }

    public void OnFocus(bool performed) {
        if(performed) { 
            i_isFocusing = true;
            e_sprayUI.SetActive(true);
        }
        else { 
            i_isFocusing = false;
            e_sprayUI.SetActive(false);
        }
    }

    public void OnLeftClick(bool performed) {
        if(i_isFocusing == false)
            return;

        if(i_isDrawable == false)
            return;

        if(i_sprayLeftover <= 0f)
            return;

        if(performed) {
            StartCoroutine(CoUseSprayLiquid());
            
        }
        else {
            i_isSpraying = false;
        }
    }

    private IEnumerator CoUseSprayLiquid() {
        i_isSpraying = true;
        e_nozzle.Play();

        while(i_isSpraying && i_sprayLeftover > 0f) {
            i_sprayLeftover -= i_sprayDecreaseAmount;
            e_sprayLeftOverUI.value = i_sprayLeftover / 100f;
            yield return i_sprayDecreaseIntervalWait;
        }

        e_nozzle.Stop();
        yield break;
    }

    #endregion
}
