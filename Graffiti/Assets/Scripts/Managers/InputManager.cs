using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;
using static Define;

public class InputManager : MonoBehaviour {
    [Tooltip("PlayerInput Scriptable Object")]
    [SerializeField] InputActionAsset _actionAsset = null;

    [Tooltip("PlayerInput Component")]
    [SerializeField] private PlayerInput _playerInput = null;
    private List<InputActionMap> _actionMaps = null;

    private void Awake() {
        if(_playerInput == null) {
            Debug.LogError($"InputManager: PlayerInput is not Validated!");
            return;
        }

        GetActionMaps();
    }

    public void RegisterPlayerInput(PlayerInput inputSystem) {
        if(_playerInput.Equals(inputSystem))
            return;

        _playerInput = inputSystem;
        GetActionMaps();
    }

    private void GetActionMaps() {
        _actionMaps = _playerInput.actions.actionMaps.ToList();
    }

    //TODO: 나중에 다양한 입력을 받는 오브젝트가 생성되면 추가해야함
    //public void ChangePlayerInputStateTo(PlayerInput inputSystem) { }

    public void ChangeInputState(InputType type) {
        for(int i = 0; i < _actionMaps.Count; i++) {
            _actionMaps[i].Disable();
        }

        _actionMaps[(int)type].Enable();
    }
}