using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour{
    #region Singleton
    private static Managers _instance = null;
    public static Managers Instance { get => _instance; }
    #endregion

    #region Primitive Managers
    private static InteractionManager _interact = new InteractionManager();
    public static InteractionManager Interact { get=> _interact; }
    #endregion

    #region Unity Event Functions
    private void Awake() {
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


    }

    #endregion
}
