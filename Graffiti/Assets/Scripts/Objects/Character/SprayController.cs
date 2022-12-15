using PaintIn3D;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.ParticleSystem;

public class SprayController : MonoBehaviour {
    #region Components
    [SerializeField] private ParticleSystem _sprayParticle = null;
    [SerializeField] private P3dPaintSphere _sprayDrawer = null;

    #endregion


    #region Variables
    private bool _isFocusing = false;

    #endregion

    #region Unity Event Functions
    private void Update() {
        if(!_isFocusing)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction, Color.green, 1f);
        
        if(Physics.Raycast(ray, out hit)) {
            
        }
    }

    #endregion

    #region User Defined Functions

    public void ChangeColorTo(Color color) {
        _sprayParticle.startColor = color;
        _sprayDrawer.Color = color;
    }

    public void ISE_ChangeSpraySize(InputAction.CallbackContext value) {
        float size = value.ReadValue<float>();
        var shape = _sprayParticle.shape;
        shape.angle += size;
    }

    //TODO: 마우스 위치로 나갈 수 있도록 조절
    //public void ISE_MousePosition(InputAction.CallbackContext value) {
    //    Vector3 mousePos = value.ReadValue<Vector3>();
    //}

    public void ISE_OnLeftClick(InputAction.CallbackContext value) {
        if(value.phase == InputActionPhase.Performed) {
            _sprayParticle.Play();

        }

        if(value.phase == InputActionPhase.Canceled) {
            _sprayParticle.Stop();
        }
    }

    #endregion
}
