using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DelegateTutorial : TutorialObject {
    private bool m_pointerUp = false;
    protected override void OnAwake() { }

    private void Update() {
        if(Activated == false)
            return;

        if(Disabled)
            return;

        if(m_pointerUp == false)
            m_pointerUp = Input.GetMouseButtonUp(0);

        if(Input.GetMouseButton(0) && m_pointerUp) {
            OnClick();
            m_pointerUp = false;
        }
    }
}
