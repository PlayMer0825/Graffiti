using UnityEngine;

namespace Insomnia{
    [CreateAssetMenu(menuName = "ScriptableObject/Quest/Quest", fileName = "Quest")]
    public class Quest : ScriptableObject {
		[Header("Quest: References")]
		[SerializeField] private SimpleIncrease m_action = null;

		[Header("Quest: Status")]
		[SerializeField] private uint m_questID = 0;
		[SerializeField, Multiline(10)] 
		private string m_description = string.Empty;

		[Header("Quest: Settings")]
		[SerializeField] private int m_counter = 0;
		[SerializeField] private int m_totalCnt = 0;

        #region Properties
        public uint QuestID { get => m_questID; }
		public bool IsCompleted { get { return (m_counter >= m_totalCnt); } }
		public int Counter { get => m_counter; }
		public int Max { get => m_totalCnt; }
		public string Description { get => m_description; }

        #endregion

		public void Initialize() {
			m_counter = 0;
		}

        public void Increase(int inc = 1) {
			if(m_action == null)
				return;

			m_action.Progress(inc, ref m_counter, in m_totalCnt); 
		} 
	}
}
