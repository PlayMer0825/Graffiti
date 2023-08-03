using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Insomnia{
	public class QuestDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        [SerializeField] private Transform m_downPos = null;
        [SerializeField] private Transform m_upPos = null;
		[SerializeField] private Image m_displayRaycaster = null;
        [SerializeField] private GameObject m_questDisplay = null;
        [SerializeField] private TextMeshProUGUI m_questText = null;

        private void Start() {
            if(QuestContainer.Instance == null) {
                gameObject.SetActive(false);
                return;
            }
                

            QuestContainer.Instance.OnQuestCompleted += OnQuestUpdate;
            Quest firstQuest = QuestContainer.Instance.GetRecentQuest();
            if(firstQuest == null)
                return;

            m_questText.text = firstQuest.Description;
        }

        private void OnDestroy() {
            if(QuestContainer.Instance == null)
                return;

            QuestContainer.Instance.OnQuestCompleted -= OnQuestUpdate;
        }

        public void OnQuestUpdate(Quest quest) {
            if(quest == null)
                return;

            m_questText.text = quest.Description;
        }

        private IEnumerator CoStartMoveTo(Transform nextPos) {
            if(m_questDisplay == null)
                yield break;

            while(true) {
                m_questDisplay.transform.position = Vector3.Lerp(m_questDisplay.transform.position, nextPos.position, 0.3f);

                if((m_questDisplay.transform.position - nextPos.position).magnitude <= 0.1f) {
                    m_questDisplay.transform.position = nextPos.position;
                    yield break;
                }

                yield return null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            StopAllCoroutines();
            StartCoroutine(CoStartMoveTo(m_downPos));
        }

        public void OnPointerExit(PointerEventData eventData) {
            StopAllCoroutines();
            StartCoroutine(CoStartMoveTo(m_upPos));
        }
    }
}
