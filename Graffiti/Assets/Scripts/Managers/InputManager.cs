using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;
public class InputManager : MonoBehaviour {
    #region Singleton
    private static InputManager _instance = null;
    public static InputManager Instance { get => _instance; }

    #endregion

    #region Local Variables
    [SerializeField] private InputableObject _ingameInputHandler = null;
    [SerializeField] private InputableObject _uiInputHandler = null;
    #endregion

    #region Properties


    #endregion

    #region Unity Event Functions
    private void Awake() {
        if(_instance != null) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        _uiInputHandler?.HandleInput();
        _ingameInputHandler?.HandleInput();
    }

    #endregion

    #region User Defined Functions
    /// <summary>
    /// Input값을 적용할 <see cref="InputableObject"/> 인스턴스를 변경합니다.
    /// Start() 이벤트 함수 이후부터 호출 가능합니다.
    /// </summary>
    /// <returns>true if Interruption successed, else false.</returns>
    public bool InterruptHandleInput(InputableObject input) {
        if(input.Equals(_ingameInputHandler))
            return false;
        _ingameInputHandler = input;
        return true;
    }
    #endregion
}
