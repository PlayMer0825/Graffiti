using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DelegateTutorial : TutorialObject {
    [SerializeField] protected Button m_button = null;
    [SerializeField] protected TextMeshProUGUI m_text = null;
    private bool m_pointerUp = false;
    protected override void OnAwake() { }

    public override void OnClick() {
        base.OnClick();

        if(SpriteIndex < m_tutorialSprite.Length - 2)
            m_text.text = "´ÙÀ½";
        else
            m_text.text = "´Ý±â";
    }

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
