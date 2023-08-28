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
        [SerializeField] private uint m_questID = 0;
        [SerializeField] private bool m_canShowQuest = false;

        private void Start() {
            if(QuestContainer.Instance == null) {
                gameObject.SetActive(false);
                return;
            }

            QuestContainer.Instance.OnQuestModified += UpdateQuestDisplayUI;

            UpdateQuestDisplayUI(QuestContainer.Instance.GetRecentQuest());
        }
        private void Update() {
            if(0 != m_questID)
                return;

            Quest quest = QuestContainer.Instance.GetRecentQuest();
            if(null == quest)
                return;

            UpdateQuestDisplayUI(quest);
        }

        private void OnDestroy() {
            if(QuestContainer.Instance == null)
                return;

            QuestContainer.Instance.OnQuestModified -= UpdateQuestDisplayUI;
        }

        public void UpdateQuestDisplayUI(Quest quest) {
            if(null == quest) {
                m_questID = 0;
                m_questText.text = "";
                m_canShowQuest = false;

                OnPointerExit(null);
                return;
            }

            if(quest.QuestID == m_questID)
                return;

            m_questID = quest.QuestID;
            m_questText.text = quest.Description;
            m_canShowQuest = true;

            OnPointerEnter(null);
            Invoke("TempOnPointerExit", 2f);
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
            #region Guard
            if(null == DrawManager.Instance || true == DrawManager.Instance.IsAnyPanelOpened())
                return;

            if(false == m_canShowQuest)
                return;
            #endregion

            StopAllCoroutines();

            if(null == QuestContainer.Instance.FindQuestWithID(m_questID)) {
                StartCoroutine(CoStartMoveTo(m_upPos));
                return;
            }

            StartCoroutine(CoStartMoveTo(m_downPos));
        }

        public void OnPointerExit(PointerEventData eventData) {
            StopAllCoroutines();
            StartCoroutine(CoStartMoveTo(m_upPos));
        }
        
        private void TempOnPointerExit() {
            OnPointerExit(null);
        }
    }
}
