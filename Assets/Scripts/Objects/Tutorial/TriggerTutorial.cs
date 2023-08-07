using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerTutorial : TutorialObject {
    private BoxCollider m_trigger = null;


    protected override void OnAwake() {
        m_trigger = GetComponentInChildren<BoxCollider>();
        if(m_trigger == null)
            return;

        m_trigger.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") == false)
            return;

        Activate();
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player") == false)
            return;

        Deactivate();
    }
}

