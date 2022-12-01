using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public abstract class InputableObject : MonoBehaviour {
    [SerializeField] private InputPriority _priority;
    public InputPriority Priority { get => _priority; }
    public abstract void HandleInput();
}