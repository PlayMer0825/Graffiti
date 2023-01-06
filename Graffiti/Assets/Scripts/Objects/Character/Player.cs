using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerBrain))]
public class Player : MonoBehaviour {
    private static PlayerBrain e_brain = null;
    public  static PlayerBrain Brain { get => e_brain; }

    private static PlayerMovement e_movement = null;
    public  static PlayerMovement Movement { get => e_movement; }
}
