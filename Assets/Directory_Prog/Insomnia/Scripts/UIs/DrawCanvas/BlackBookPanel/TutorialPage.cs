using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Insomnia{
	public class TutorialPage : MonoBehaviour {
		[Header("TutorialPage: Component")]
		[SerializeField] private Image m_display = null;

        [Header("TutorialPage: Settings")]
        [SerializeField] private Color m_defaultColor = new Color(0, 0, 0, 0);
        [SerializeField] private Color m_displayColor = new Color(1, 1, 1, 1);

        private void OnEnable() {
            m_display.sprite = null;
            m_display.color = m_defaultColor;
        }

        public void DisplayTutorial(Sprite tutorialSpr) {
            if(m_display == null)
                return;

            if(tutorialSpr == null)
                return;

            if(m_display.sprite == tutorialSpr) {
                m_display.sprite = null;
                m_display.color = m_defaultColor;
                return;
            }

            m_display.color = m_displayColor;
            m_display.sprite = tutorialSpr;
        }

        private void OnDisable() {
			m_display.sprite = null;
            m_display.color = m_defaultColor;
        }
    }
}
