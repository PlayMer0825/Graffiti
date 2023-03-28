using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSave : MonoBehaviour
{
    // CSV 파일 이름
    private string fileName;
    [SerializeField] private TextAsset csv_data = null;

    // CSV 파일에서 가져온 데이터를 저장할 리스트
    private List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

    // EventID를 기준으로 ActorID와 Context를 그룹화하여 저장할 딕셔너리
    public Dictionary<string, Dictionary<string, List<string>>> save = new Dictionary<string, Dictionary<string, List<string>>>();
    // Start 메소드에서 CSV 파일을 읽어서 데이터를 저장하고 DialogSave 딕셔너리에 값을 저장합니다.
    void Start()
     {
         fileName = csv_data.name;
         data = CSVParser.Read(fileName);

         foreach (var row in data)
         {

             string eventID = row["EventID"].ToString();
             string actorID = row["ActorID"].ToString();
             string context = row["Context"].ToString();

            if (!save.ContainsKey(eventID))
            {
                save[eventID] = new Dictionary<string, List<string>>();
            }

            // ActorID가 딕셔너리에 없으면 새로운 List를 추가합니다.
            if (!save[eventID].ContainsKey(actorID))
            {
                save[eventID][actorID] = new List<string>();
            }

            save[eventID][actorID].Add(context);
        }
     }

    public void SaveDialog(string eventID, string actorID, string context)
    {
        if (!save.ContainsKey(eventID))
        {
            save[eventID] = new Dictionary<string, List<string>>();
        }
        if (!save[eventID].ContainsKey(actorID))
        {
            save[eventID][actorID] = new List<string>();
        }

        save[eventID][actorID].Add(context);
    }
}