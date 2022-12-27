using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/InteractableObjects", order = 1)]
public class InteractableDB : ScriptableObject
{
    public Dictionary<string, Interactable> interactables;
}
