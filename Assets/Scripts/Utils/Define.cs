using System;
using UnityEngine.InputSystem;

public class Define
{
    [Serializable]
    public struct MovementStat {
        public MovementData[] moveType;
        public float turnSmoothVelocity;
        public float turnSmoothTime;
    }

    [Serializable]
    public struct MovementData {
        public MovementType type;
        public float speed;
        public float acceleration;
        public float damping;
    }

    public enum InputPriority : ushort {
        PlayerCharacter = 0,
        Paint,
        UI,
    }

    public enum InputType : byte {
        Player_Wander = 0,
        Player_Draw = 1,
        Player_PaintUI = 2,

    }

    public enum EventFuncInvokePoint {
        OnReady,
        OnUnReady,
        OnStart,
        OnFinish
    }

    public enum Status : byte { Walk, Run, Crouch, }
    
    public enum InteractionType : ushort {
        //TODO: 값을 레이어와 일치시
        None = 0,
        Paintable = 1,
        Dialog,
    }
}

public static class Extensions {
    public static byte Not(this byte value) {
        return (byte)(1 ^ value);
    }
}

//public InputAction fire;

//[SerializeField] private InputActionAsset controls;

//private InputActionMap _inputActionMap;

//private void Start() {
//    _inputActionMap = controls.FindActionMap("Gameplay");

//    fire = _inputActionMap.FindAction("Fire");

//    fire.performed += OnFireAction;
//}

//private void OnFireAction(InputAction.CallbackContext obj) {
//    // do stuff
//}