using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MainCameraController : MonoBehaviour {
    #region Singleton
    private static MainCameraController _instance;
    public static MainCameraController Instance { get => _instance; }

    #endregion

    #region Components
    [SerializeField] private Camera           _cam       = null;
    [SerializeField] private CinemachineBrain _cineBrain = null;

    #endregion

    #region Properties
    public CinemachineBrain CineCam { get => _cineBrain; }

    #endregion

    #region Unity Event Functions
    private void Awake() {
        if(_instance != null) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _cam = GetComponent<Camera>();
        _cineBrain = GetComponent<CinemachineBrain>();

        if(_cam == null) {
            Debug.LogError($"MainCamera: {gameObject.name}'s Camera is not Validated!");
            gameObject.SetActive(false);
            return;
        }

        if(_cineBrain == null) {
            Debug.LogError($"MainCamera: {gameObject.name}'s CinemachineBrain is not Validated!");
            gameObject.SetActive(false);
            return;
        }
    }

    private void Update() {
        
    }

    #endregion

    #region User Defined Functions
    public void DisableLayerMask(params LayerMask[] layers) {
        int disableMasks = 0;

        for(int i = 0; i < layers.Length; i++) {
             disableMasks +=  1 << layers[i];
        }

        _cam.cullingMask = _cam.cullingMask ^ disableMasks;
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
