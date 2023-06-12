using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZB.Dialogue.Graffiti
{
    public class DialogueMachine : MonoBehaviour
    {
        [Header("다이얼로그 입장할 때 이벤트")]
        [SerializeField] private UnityEvent m_fixedEvent_OnEnter;
        [Header("다이얼로그 퇴장할 때 이벤트")]
        [SerializeField] private UnityEvent m_fixedEvent_OnEscape;

        private UnityEvent m_uEvent_OnEscape;
        [SerializeField] private DialogueContentsPool m_pool;
        [SerializeField] private ActorConnector m_actorConnector;
        [SerializeField] private SpeechBubble m_speechBubble;

        public bool m_Interacting { get => m_interacting; }

        Interact m_nowInteract;
        int m_index;
        bool m_interacting;

        // 매개변수로 이벤트 ID 넣으면 다이얼로그 바로 불러올 수 있다.. -> TimeLine 에서 사용하기 
        public void NewExport(int EventID)
        {
            m_fixedEvent_OnEnter.Invoke();

            m_nowInteract = m_pool.GetInteract(EventID);
            m_index = -1;
            m_interacting = true;
            TryNext();
        }

        public void TryNext()
        {
            //아직 등장 연출 중이다. 스킵
            if (m_speechBubble.m_TextAppearing)
            {
                m_speechBubble.SkipCurrent();
            }

            //등장 연출이 끝났다. 다음대화
            else
            {
                m_index++;
                //진행할 대화가 있다
                if (m_index < m_nowInteract.m_Acts.Count)
                {
                    Act nowAct = m_nowInteract.m_Acts[m_index];

                    //말풍선을 제외한 행동
                    m_actorConnector.Actor_Do(nowAct);

                    //말풍선
                    m_speechBubble.AppearNew(m_actorConnector.GetActorTransform(nowAct), nowAct.m_Context);
                }

                //진행할 대화가 없다
                else
                {
                    m_speechBubble.Disappear();
                    EscapeDialogue();
                }
            }
        }

        public void EscapeDialogue()
        {
            m_interacting = false;
            m_uEvent_OnEscape.Invoke();
            m_fixedEvent_OnEscape.Invoke();
            m_uEvent_OnEscape.RemoveAllListeners();
        }

        public void AddEscapeEvent(UnityAction action)
        {
            m_uEvent_OnEscape.AddListener(action);
        }

        private void Awake()
        {
            m_uEvent_OnEscape = new UnityEvent();
        }
    }
}