using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class InputableObject : MonoBehaviour {
    public abstract bool HandleInput();
}