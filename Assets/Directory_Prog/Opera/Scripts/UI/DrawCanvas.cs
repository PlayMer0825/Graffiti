using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCanvas : MonoBehaviour {


    [Header("Black Book UI")]
    [SerializeField] BlackBook _blackBookCanvas = null;

    private void Awake() {
        if(_blackBookCanvas == null) 
            _blackBookCanvas = GetComponentInChildren<BlackBook>();
        _blackBookCanvas.gameObject.SetActive(false);
        
    }

    public void OpenBlackBook() {
        _blackBookCanvas.gameObject.SetActive(true);
    }

    public void CloseBlackBook() {
        _blackBookCanvas.gameObject.SetActive(true);
    }
}
