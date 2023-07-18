using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Insomnia{
	public class QuestUIElement : MonoBehaviour {
		[Header("QuestUIElement: Components")]
		[SerializeField] private TextMeshProUGUI m_completedCount = null;
		[SerializeField] private TextMeshProUGUI m_questDescription = null;

		[Header("QuestUIElement: References")]
		[SerializeField] private Quest m_quest = null;

        public void Initialize(Quest quest) {
			m_quest = quest;
			if(m_completedCount == null)
				return;

			if(m_questDescription == null)
				return;

			m_questDescription.text = m_quest.Description;
			UpdateUI();
		}

		public void UpdateUI() {
			if(m_quest == null) {
				gameObject.SetActive(false);
				return;
			}

			string color = m_quest.IsCompleted ? "green" : "red";
			m_completedCount.text = $"<color={color}>{m_quest.Counter}</color>/{m_quest.Max}";
		}
	}
}
