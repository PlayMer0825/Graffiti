using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Insomnia{
	public class QuestContainer : MonoBehaviour {
        #region Singleton
        private static QuestContainer m_instance = null;
		public static QuestContainer Instance { get {
				if(m_instance == null) {
					QuestContainer go = new GameObject().AddComponent<QuestContainer>();
					m_instance = go;
					m_instance.gameObject.name = "@QuestContainer";
					DontDestroyOnLoad(go.gameObject);
				}

				return m_instance;
			} 
		}

		#endregion

		[Header("QuestContainer: Components")]

		[Header("QuestContainer: References")]
		[SerializeField] private GameObject m_questUI = null;
		[SerializeField] private Transform m_questParent = null;
		[SerializeField] private GameObject m_elementPrefab = null;
		[SerializeField] private List<QuestUIElement> m_elements = new List<QuestUIElement>();
		[SerializeField] private Image m_fadeImage = null;

		[Header("QuestContainer: Instances")]
		[SerializeField] private Dictionary<uint, Quest> m_quests = new Dictionary<uint, Quest>();

		[Header("QuestContainer: Status")]
		[SerializeField] private bool m_isOpened = false;
		[SerializeField] private bool m_isEnd = false;

		[Header("QuestContainer: Settings")]
		[SerializeField] private string m_endingSceneName = "";

		public bool IsOpened { get => m_isOpened; }


        #region Unity Event Functions
        private void Awake() {
            if(m_instance != null) {
				Destroy(gameObject);
				return;
			}

			m_instance = this;
			DontDestroyOnLoad(gameObject);
        }

        private void Update() {
			if(DrawManager.Instance == null) {
                if(IsOpened)
                    return;

                m_questUI.SetActive(false);
                m_isOpened = false;
				return;
            }

			if(DrawManager.Instance.IsDrawing) {
				if(IsOpened == false)
					return;

				m_questUI.SetActive(false);
				m_isOpened = false;
			}
			else {
				if(IsOpened)
					return;

				m_questUI.SetActive(true);
				m_isOpened = true;
			}
        }

        #endregion


        #region QuestContainer Functions

        public void AddQuest(Quest quest) {
			if(m_quests.ContainsKey(quest.QuestID))
				return;
			quest.Initialize();

			m_quests.Add(quest.QuestID, quest);

			int diff = Mathf.Abs(m_elements.Count - m_quests.Count);
			if(diff > 0) {
				if(m_elementPrefab == null) 
					Debug.LogError("QuestContainer: m_elementPrefab is null");
				else {
					QuestUIElement element = Instantiate(m_elementPrefab, m_questParent).GetComponent<QuestUIElement>();
					if(element == null)
						return;

					element.Initialize(quest);
					m_elements.Add(element);
				}
			}

			//TODO: UI 업데이트
			UpdateUI();
		}

		public void UpdateQuest(uint questID, int inc = 1) {
			if(m_quests.ContainsKey(questID) == false)
				return;

			m_quests[questID].Increase(inc);
            UpdateUI();

			if(CheckAllQuestClear())
				ChangeSceneToEnding();
        }

		public void UpdateUI() {
			if(m_quests.Count <= 0)
				return;

			for(int i = 0; i < m_elements.Count; i++) {
				m_elements[i].UpdateUI();
			}
		}

		private bool CheckAllQuestClear() {
			if(m_isEnd)
				return false;

			if(m_quests == null)
				return false;

			if(m_quests.Count <= 0)
				return false;

			bool success = true;

			foreach(uint key in m_quests.Keys) {
				success &= m_quests[key].IsCompleted;
			}

			return success;
		}

		private void ChangeSceneToEnding() {
            m_isEnd = true;

			for(int i = 0; i < m_elements.Count; i++) {
				m_elements[i].gameObject.SetActive(false);
			}

            PlayerMove_SIDE.isLoad = false;
			StartCoroutine(FadeCoroutine());

            //TODO: SceneChanger로 마지막 씬으로 넘기기
        }
        #endregion

        #region Copied Functions

        IEnumerator FadeCoroutine() {
			if(m_fadeImage == null)
				yield break;

            float fadeCount = 0;
            while(fadeCount < 1.0f) {
                fadeCount += 0.01f;
                yield return new WaitForSeconds(0.01f);
                m_fadeImage.color = new Color(0, 0, 0, fadeCount);
            }

            SceneManager.LoadScene(m_endingSceneName);
			SceneManager.sceneLoaded += (scene, mode) => {
				StartCoroutine(FadeoutCoroutine());
			};
        }

		IEnumerator FadeoutCoroutine() {
            if(m_fadeImage == null)
                yield break;

            float fadeCount = 1;
            while(fadeCount >= 0.1f) {
                fadeCount -= 0.01f;
                yield return new WaitForSeconds(0.01f);
                m_fadeImage.color = new Color(0, 0, 0, fadeCount);
            }

			m_fadeImage.color = new Color(0, 0, 0, 0);
			yield break;
        }
        #endregion
    }
}
