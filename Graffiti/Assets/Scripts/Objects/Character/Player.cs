using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerBrain))]
public class Player : MonoBehaviour
{
    private static PlayerMovement i_movement = null;
    private static PlayerBrain i_brain = null;

    public static PlayerMovement Movement { get => i_movement; }
    public static PlayerBrain Brain { get => i_brain;}

    private void Awake() {
        if(i_movement == null || i_brain == null)
            return;

        //Managers.Input.RegisterPlayerInput
    }
}
