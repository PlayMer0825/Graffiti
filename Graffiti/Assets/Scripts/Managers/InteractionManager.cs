using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager{
    [SerializeField] private Interactable _curInteractingObj = null;
    public Interactable CurInteractObj { get => _curInteractingObj; }
    private PlayerTest Player {
        get {
            if(_player == null)
                _player = Managers.Player;
            return _player;
        }
    }
    private PlayerTest _player = null;

    public bool EnterInteractWithCurObj(Interactable interactedObject) {
        if(_curInteractingObj == null)
            return false;

        if(_curInteractingObj.Equals(interactedObject) == false)
            return false;

        //TODO: 그림그리기 외 다른 상호작용이 생기면 Interactable의 타입에 따라서 컬링할 레이어를 선택하도록 수정 필요
        //Managers.Cam.DisableLayerMask(LayerMask.NameToLayer("PlayerBody"));
        Player.ChangeInputTypeTo(Define.InputType.Player_Draw);
        return true;
    }

    public bool ExitInteractWithCurObj() {
        if(_curInteractingObj == null)
            return false;

        _curInteractingObj.OffInteract();
        //TODO: 그림그리기 외 다른 상호작용이 생기면 Interactable의 타입에 따라서 컬링할 레이어를 선택하도록 수정 필요
        //Managers.Cam.EnableLayerMask(LayerMask.NameToLayer("PlayerBody"));
        Player.ChangeInputTypeTo(Define.InputType.Player_Wander);
        return true;
    }

    public void ChangeCurIntObj(Interactable newIntObj) {
        if(_curInteractingObj == null) {
            _curInteractingObj = newIntObj;
            return;
        }

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
