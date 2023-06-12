using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB.Dialogue.Graffiti
{
    public class Actor : MonoBehaviour
    {
        public int m_ActorID { get => m_actorID; }

        [Header("몸체, 얼굴 애니메이터 삽입 필요")]
        [SerializeField] Animator m_ani_Acting;
        [SerializeField] Animator m_ani_Face;
        [Space]
        [Header("ID 기입필요")]
        [SerializeField] int m_actorID;

        [Space]
        [Header("최근에 입력받은 명령")]
        [SerializeField] Act m_currentAct;

        public void Do(Act act)
        {
            m_currentAct = act;

            string actingTriggerID = "";
            string faceTriggerID = "";
            string[] splitStr = act.m_AniInput.Split('/');

            if (splitStr.Length == 2)
            {
                actingTriggerID = splitStr[0];
                faceTriggerID = splitStr[1];
            }
            else if (splitStr.Length == 1)
            {
                actingTriggerID = splitStr[0];
            }

            if (m_ani_Acting != null)
            {
                if(!act.IsActingEmpty())
                {
                    m_ani_Acting.SetTrigger(actingTriggerID);
                    Debug.Log("액팅 연출할 것이 있음");
                }
                else
                {
                    m_ani_Acting.ResetTrigger(actingTriggerID);
                    Debug.Log("액팅 연출할 것이 없음");
                }
            }
            if (m_ani_Face != null)
                m_ani_Face.SetTrigger(faceTriggerID);
            
            // 내가 추가.. 
            //else if(m_ani_Acting == null)
            //{
            //    Debug.Log("액팅 null");
            //    m_ani_Acting.ResetTrigger(actingTriggerID);
            //    //m_ani_Face.ResetTrigger(faceTriggerID);
            //}
            // 그게 되어야 겠네.. 액팅의 내용을 판별해가지고 내용이 비어있으면 트리거를 해제하고 비어있지 않으면 해당 내용을 하는 식으로
        }
    }
}