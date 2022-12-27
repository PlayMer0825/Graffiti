using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Managers : MonoBehaviour {
    #region Singleton
    private static Managers _instance = null;
    public static Managers Instance { get => _instance; }
    #endregion

    #region Primitive Managers
    private InteractionManager _interact = new InteractionManager();
    public static InteractionManager Interact { get => _instance._interact; }

    private CameraController _cam = new CameraController();
    public static CameraController Cam { get => _instance._cam; }
    #endregion

    #region MonoBehaviour Managers
    [SerializeField] private InputManager _input = null;
    public static InputManager Input { get => _instance._input; }

    #endregion

    #region Variables
    //[Header("Player Inputs")]
    //[SerializeField] private InputActionAsset _inputAsset = null;

    #endregion

    #region Maintaining Objects
    [SerializeField] public static PlayerTest Player { get; private set; }

    #endregion

    #region Unity Event Functions

    private void Awake() {
        //TODO: Áö¿ö¾ßµÊ
        Player = FindObjectOfType<PlayerTest>();
        Init();

    }

    #endregion

    #region User Defined Functions
    private void Init() {
        if(_instance != null) {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
        //Application.targetFrameRate = 60;

        _cam.Init();
    }

    #endregion
}
