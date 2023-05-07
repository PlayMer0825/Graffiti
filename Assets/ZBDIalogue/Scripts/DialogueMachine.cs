using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB.Dialogue.Graffiti
{
    public class DialogueMachine : MonoBehaviour
    {
        [SerializeField] DialogueContentsPool m_pool;
        [SerializeField] ActorConnector m_actorConnector;
        [SerializeField] SpeechBubble m_speechBubble;

        Interact nowInteract;
        int index;

        public void NewExport(int EventID)
        {
            nowInteract = m_pool.GetInteract(EventID);
            index = -1;
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

        }
    }
}