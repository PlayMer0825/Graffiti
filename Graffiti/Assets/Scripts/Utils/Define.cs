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

    public enum MovementType : byte { 
        Walk,
        Run,
        Crouch,
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