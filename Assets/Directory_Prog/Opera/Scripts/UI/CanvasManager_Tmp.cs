using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager_Tmp : MonoBehaviour
{
    private static CanvasManager_Tmp _instance = null;
    public static CanvasManager_Tmp Instance {
        get => _instance;
    }

    [SerializeField]private Canvas _drawCanvas = null;

    private void Awake() {
        if(_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void Update() {
        if(InteractManager.Instance.IsInteracting == false)
            return;


        if(Input.GetKeyDown(KeyCode.Tab)) {
            _drawCanvas.gameObject.SetActive(!_drawCanvas.gameObject.activeSelf);
        }
    }

    public void SetCanvasActiveFalse() {
        _drawCanvas.gameObject.SetActive(false);
    }
}
