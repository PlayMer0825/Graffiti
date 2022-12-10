using System;

public class Define
{
    [Serializable]
    public struct MovementStat {
        public float speed;
        public float acceleration;
        public float damping;
        public float turnSmoothVelocity;
        public float turnSmoothTime;
    }

    public enum InputPriority : ushort {
        PlayerCharacter = 0,
        Paint,
        UI,
    }
}

public static class Extensions {
    public static byte Not(this byte value) {
        return (byte)(1 ^ value);
    }
}