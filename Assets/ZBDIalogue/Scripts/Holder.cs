using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZB.Dialogue.Graffiti
{
    public class Holder : MonoBehaviour
    {
        //키보드 입력으로 다이얼로그 입장하는지, 강제로 입장하는지 여부
        public enum ReadType
        {
            btnInput,   //버튼인풋받아서 대화
            enforce     //강제로 대화
        }

        //같은 다이얼로그 하나만 계속 출력인지, 배열에 들어가있는 다이얼로그 ID 순서대로 읽어오는지
        public enum ExportType
        {
            single,     //단일
            sequence    //순서대로 (리스트 원소들 순서대로 읽음)
        }

        public ReadType m_ReadType { get => m_readType; }

        [Header("말걸어서 대화 / 닿으면 강제대화")]
        [SerializeField] private ReadType m_readType;
        [Header("하나의 대화만 / 여러개 대화 순차적으로")]
        [SerializeField] private ExportType m_exportType;
        [Header("ID 세팅 입력필요")]
        [SerializeField] private IDSets[] m_idSets;
        [SerializeField] private int currentIndex;

        public Transform targetObject; // 바라볼 오브젝트

        private void Start()
        {
            Transform parent = transform.parent;

            if(parent != null )
            {
                targetObject = parent;
            }
        }
        public int GetEventID(out UnityEvent uEvent_OnEscape)
        {
            switch(m_exportType)
            {
                case ExportType.single:
                    uEvent_OnEscape = m_idSets[0].m_uEvent_OnEscape;
                    return m_idSets[0].m_EventID;

                case ExportType.sequence:
                    int result = m_idSets[currentIndex].m_EventID;
                    uEvent_OnEscape = m_idSets[currentIndex].m_uEvent_OnEscape;
                    currentIndex = currentIndex + 1 < m_idSets.Length ? currentIndex + 1 : currentIndex;
                    return result;
            }

            uEvent_OnEscape = null;
            return -1;
        }

        [System.Serializable]
        public class IDSets
        {
            public int m_EventID;
            [Header("해당 대화 끝나고 일어날 이벤트")]
            public UnityEvent m_uEvent_OnEscape;
        }
    }
}