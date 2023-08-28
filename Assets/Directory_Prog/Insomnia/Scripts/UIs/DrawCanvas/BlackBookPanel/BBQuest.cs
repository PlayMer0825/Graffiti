using TMPro;
using UnityEngine;

namespace Insomnia{
	public class BBQuest : MonoBehaviour {
		[SerializeField] private TextMeshProUGUI m_questText = null;

        private void Start() {
            if(QuestContainer.Instance == null) {
                gameObject.SetActive(false);
                return;
            }


            QuestContainer.Instance.OnQuestModified += OnQuestUpdate;
            Quest firstQuest = QuestContainer.Instance.GetRecentQuest();
            if(firstQuest == null)
                return;

            m_questText.text = firstQuest.Description;
        }

        private void OnDestroy() {
            if(QuestContainer.Instance == null)
                return;

            QuestContainer.Instance.OnQuestModified -= OnQuestUpdate;
        }

        public void OnQuestUpdate(Quest quest) {
            if(quest == null)
                return;

            m_questText.text = quest.Description;
        }

    }
}
