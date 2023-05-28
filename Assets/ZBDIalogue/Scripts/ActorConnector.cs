using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB.Dialogue.Graffiti
{
    public class ActorConnector : MonoBehaviour
    {
        [SerializeField] List<Actor> m_finded_actors;

        public void Actor_Do(Act act)
        {
            FindActor(act.m_ActorID).Do(act);
        }
        public Transform GetActorTransform(Act act)
        {
            return FindActor(act.m_ActorID).transform;
        }
        public Vector3 GetActorPos(Act act)
        {
            return FindActor(act.m_ActorID).transform.position;
        }

        Actor FindActor(int actorID)
        {
            //리스트 안에 있는지 찾는다.
            for (int i = 0; i < m_finded_actors.Count; i++)
            {
                if (m_finded_actors[i].m_ActorID == actorID)
                {
                    return m_finded_actors[i];
                }
            }

            //리스트안에 없다면, 리스트 업데이트, 리스트안에서 다시 찾기
            bool checkNext = false;
            Actor[] newArray = FindObjectsOfType<Actor>();
            for (int i = 0; i < newArray.Length; i++)
            {
                for (int j = 0; j < m_finded_actors.Count; j++)
                {
                    if (newArray[i] == m_finded_actors[j])
                    {
                        checkNext = true;
                        break;
                    }
                }

                if (checkNext)
                {
                    checkNext = false;
                    continue;
                }
                m_finded_actors.Add(newArray[i]);
            }
            for (int i = 0; i < m_finded_actors.Count; i++)
            {
                if (m_finded_actors[i].m_ActorID == actorID)
                {
                    return m_finded_actors[i];
                }
            }

            //실패했다면 LogError
            Debug.LogError($"ActorConnector / FindActor() / 찾기 실패 / 받은 매개변수 : {actorID} / 매개변수 입력오류 또는 찾고자하는 Actor오브젝트 비활성화");
            return null;
        }
    }
}