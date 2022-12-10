using System;

public class Define
{
    [Serializable]
    public struct MovementStat {
        public float speed;
        public float acceleration;
        public float turnSmoothVelocity;
        public float turnSmoothTime;
    }

    public enum InputPriority : ushort {
        PlayerCharacter = 0,
        Paint,
        UI,
    }
}