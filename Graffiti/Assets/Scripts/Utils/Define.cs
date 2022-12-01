using System;

public class Define
{
    [Serializable]
    public struct MovementStat {
        public float _speed;
        public float _turnSmoothVelocity;
        public float _turnSmoothTime;
    }

    public enum InputPriority : ushort {
        PlayerCharacter = 0,
        Paint,
        UI,
    }
}
