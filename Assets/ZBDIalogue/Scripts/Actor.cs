using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB.Dialogue.Graffiti
{
    public class Actor : MonoBehaviour
    {
        public int m_ActorID { get => m_actorID; }

        [SerializeField] Animator m_ani;
        [SerializeField] int m_actorID;

        [SerializeField] Act m_currentAct;

        public void Do(Act act)
        {
            m_currentAct = act;
        }
    }
}