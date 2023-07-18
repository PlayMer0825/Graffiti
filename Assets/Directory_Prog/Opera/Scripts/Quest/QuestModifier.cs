using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Insomnia{
	public class QuestModifier : MonoBehaviour {
		[Header("QuestModifier: Settings")]
		[SerializeField] private uint m_questIDtoIncrease = 0;

		public void IncreaseQuest() {
			if(m_questIDtoIncrease == 0)
				return;

			QuestContainer.Instance.UpdateQuest(m_questIDtoIncrease);
		}
	}
}
