using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour {
    [SerializeField] private Interactable _curInteractingObj = null;
    public Interactable CurInteractObj { get => _curInteractingObj; }

    public void InteractWithCurIntObj() {
        return;
    }

    public void ChangeCurIntObj(Interactable newIntObj) {
        if(_curInteractingObj != null)
            return;

        if(_curInteractingObj.Equals(newIntObj))
            return;

        _curInteractingObj = newIntObj;
    }

    /// <summary>
    /// 씬 전환처럼 모든 상호작용을 종료할 때 사용합니다.
    /// </summary>
    public void ResetCurIntObj() { _curInteractingObj = null; }

    public void ExtractFromCurIntObj(Interactable disabled) {
        if(_curInteractingObj == null)
            return;

        if(_curInteractingObj.Equals(disabled)) {
            _curInteractingObj = null;
        }

        return;
    }
}
