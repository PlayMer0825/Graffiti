using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZB.Dialogue.Graffiti
{
    [System.Serializable]
    public class Interact
    {
        public int m_EventID;
        public List<Act> m_Acts;

        public Interact()
        {
            m_Acts = new List<Act>();
        }
    }

    [System.Serializable]
    public class Act
    {
        public int m_ActorID;
        public string m_Context;
        public string m_AniInput;

        public Act(string ActorID, string Context, string AniInput)
        {
            m_ActorID = int.Parse(ActorID);
            m_Context = Context;
            m_AniInput = AniInput;
        }

        //NUll 판별 여부
        public bool IsActingEmpty()
        {
            return string.IsNullOrEmpty(m_AniInput);
        }
    }

    public class DialogueContentsPool : MonoBehaviour
    {
        [SerializeField] ZB.CSV.ZBCsvRead m_csvReader;
        [SerializeField] List<Interact> m_Interacts;

        public void MakeInteracts()
        {
            m_csvReader.LoadData();

            string focusing_EventID = "";
            List<Interact> parsedData = new List<Interact>();
            Interact newInteract = null;
            for (int i = 0; i < m_csvReader.m_datas.Count; i++)
            {
                for (int j = 0; j < m_csvReader.m_datas[i].Count; j++)
                {
                    //검사하던 EventID와 다르다. 새 Interact추가, Focusing 최신화
                    if (m_csvReader.m_datas[i][j]["EventID"] != focusing_EventID)
                    {
                        newInteract = new Interact();
                        parsedData.Add(newInteract);
                        focusing_EventID = m_csvReader.m_datas[i][j]["EventID"];
                        newInteract.m_EventID = int.Parse(focusing_EventID);

                        newInteract.m_Acts.Add(MakeAct(m_csvReader.m_datas[i][j]));
                    }
                    else
                    {
                        newInteract.m_Acts.Add(MakeAct(m_csvReader.m_datas[i][j]));
                    }
                }
            }

            m_Interacts = parsedData;
        }
        public Interact GetInteract(int EventID)
        {
            for (int i = 0; i < m_Interacts.Count; i++)
            {
                if (m_Interacts[i].m_EventID == EventID)
                {
                    return m_Interacts[i];
                }
            }

            Debug.LogError("DialougeContentsPool / GetInteract() / EventID 입력오류 / 현재입력 : " + EventID);
            return null;
        }

        private void Awake()
        {
            MakeInteracts();
        }
        private Act MakeAct(Dictionary<string,string> dic_str_str)
        {
            Act result = new Act(
                            dic_str_str["ActorID"],
                            dic_str_str["Context"],
                            dic_str_str["Acting/Face"]
                );

            return result;
        }
    }
}