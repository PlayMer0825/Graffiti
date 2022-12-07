using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCam : MonoBehaviour {
    #region Components
    private CinemachineVirtualCamera m_virtualCam = null;
    public CinemachineVirtualCamera VirtualCam { get => m_virtualCam; }
    #endregion

    #region External Objects
    private static CinemachineBrain s_mainCam = null;

    #endregion

    #region Unity Event Functions
    private void OnValidate() {
        s_mainCam = Camera.main.GetComponent<CinemachineBrain>();
        m_virtualCam = GetComponent<CinemachineVirtualCamera>();

        if(s_mainCam == null)
            Debug.LogError($"InteractionCam: {gameObject.name}'s CinemachineBrainCam is not Validated");

        if(m_virtualCam == null)
            Debug.LogError($"InteractionCam: {gameObject.name}'s CinemachineVirtualCam is not Validated");
    }

    private void Awake() {
        
    }

    #endregion

    #region User Defined Functions


    #endregion
}
