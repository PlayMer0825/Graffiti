using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CameraController{
    #region Components
    [SerializeField] private Camera           _cam       = null;
    [SerializeField] private CinemachineBrain _cineBrain = null;

    #endregion

    #region User Defined Functions
    public void Init() {
        _cam = Camera.main;

        if(_cam == null) {
            Debug.LogError($"Managers.MainCameraController's Camera is not Validated!");
            return;
        }

        _cineBrain = _cam.GetComponent<CinemachineBrain>();
        if(_cineBrain == null) {
            Debug.LogError($"Managers.MainCameraController's CinemachineBrain is not Validated!");
            return;
        }
    }

    public void DisableLayerMask(params LayerMask[] layers) {
        int disableMasks = 0;

        for(int i = 0; i < layers.Length; i++) {
             disableMasks +=  1 << layers[i];
        }

        _cam.cullingMask = _cam.cullingMask & ~disableMasks;
    }

    public void EnableLayerMask(params LayerMask[] layers) {
        int enableMasks = 0;

        for(int i = 0; i < layers.Length; i++) {
            enableMasks += 1 << layers[i];
        }

        _cam.cullingMask = _cam.cullingMask | enableMasks;
    }

    public void ResetLayerMask() {
        _cam.cullingMask = ~( 0 & _cam.cullingMask );
    }

    #endregion
}
