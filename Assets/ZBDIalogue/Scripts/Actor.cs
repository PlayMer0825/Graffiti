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
                m_ani_Acting.SetTrigger(actingTriggerID);
            if (m_ani_Face != null)
                m_ani_Face.SetTrigger(faceTriggerID);
        }
    }
}