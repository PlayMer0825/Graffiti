using UnityEngine;

namespace Insomnia{
	public class QuestProvider : MonoBehaviour {
		[Header("QuestProvider: References")]
		[SerializeField] private Quest[] m_questsToRegist = null;

		public void RegisterQuest() {
			if(m_questsToRegist == null)
				return;

			if(m_questsToRegist.Length <= 0)
				return;

			for(int i = 0; i < m_questsToRegist.Length; i++) {
				QuestContainer.Instance.AddQuest(m_questsToRegist[i]);
            }
		}
	}
}
