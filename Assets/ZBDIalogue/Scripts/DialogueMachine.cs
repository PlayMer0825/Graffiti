using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ZB.Dialogue.Graffiti
{
    public class DialogueMachine : MonoBehaviour
    {
        [SerializeField] private DialogueContentsPool m_pool;
        [SerializeField] private ActorConnector m_actorConnector;
        [SerializeField] private SpeechBubble m_speechBubble;
        [SerializeField] private UnityEvent m_uEvent_OnEscape;

        public bool Interacting { get => interacting; }

        Interact nowInteract;
        int index;
        bool interacting;

        public void NewExport(int EventID)
        {
            nowInteract = m_pool.GetInteract(EventID);
            index = -1;
            interacting = true;
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
                index++;
                //진행할 대화가 있다
                if (index < nowInteract.m_Acts.Count)
                {
                    Act nowAct = nowInteract.m_Acts[index];

                    //말풍선을 제외한 행동
                    m_actorConnector.Actor_Do(nowAct);

                    //말풍선
                    m_speechBubble.AppearNew(m_actorConnector.GetActorPos(nowAct), nowAct.m_Context);
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
            interacting = false;
            m_uEvent_OnEscape.Invoke();
            m_uEvent_OnEscape.RemoveAllListeners();
        }

        public void AddEscapeEvent(UnityAction action)
        {
            m_uEvent_OnEscape.AddListener(action);
        }
    }
}