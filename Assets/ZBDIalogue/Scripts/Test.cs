using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] ZB.Dialogue.Graffiti.DialogueMachine machine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            machine.NewExport(210407);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            machine.TryNext();
        }
    }
}
