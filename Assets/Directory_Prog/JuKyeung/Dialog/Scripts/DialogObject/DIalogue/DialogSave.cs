using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSave : MonoBehaviour
{
    //no...new ..find...... = awake
    // 게임시작시 연산 start 
    // data 클래스로 써 데이터의 로직이 들어갑니다. 

    // CSV 파일 이름
    private string fileName;
    [SerializeField] private TextAsset csv_data = null;

    // CSV 파일에서 가져온 데이터를 저장할 리스트
    private List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

    // EventID를 기준으로 ActorID와 Context를 그룹화하여 저장할 딕셔너리
    public Dictionary<string, Dictionary<string, List<string>>> saveDialog = new Dictionary<string, Dictionary<string, List<string>>>();
    // Start 메소드에서 CSV 파일을 읽어서 데이터를 저장하고 DialogSave 딕셔너리에 값을 저장

    public Dictionary<string, Dictionary<string, List<string>>> saveActor = new Dictionary<string, Dictionary<string, List<string>>>();
    void Awake()
    {
        fileName = csv_data.name;
        data = CSVParser.Read(fileName);

        foreach (var row in data)
        {
            string eventID = row["EventID"].ToString();
            string actorID = row["ActorID"].ToString();
            string context = row["Context"].ToString();

            if (!saveDialog.ContainsKey(eventID))
            {
                saveDialog[eventID] = new Dictionary<string, List<string>>();
            }

            // ActorID가 딕셔너리에 없으면 새로운 List를 추가
            if (!saveDialog[eventID].ContainsKey(actorID))
            {
                saveDialog[eventID][actorID] = new List<string>();
            }

            saveDialog[eventID][actorID].Add(context);
        }
    }


    // 어떤 유닛이 됐던 eventid를 기반으로 파싱한 csv데이터 안에서 해당 대화 리스틀 찾아서 넘겨줌
    public static List<string> Get_Dialog(Dictionary<string, Dictionary<string, List<string>>> _save, string Eventid)
    {
        Dictionary<string, List<string>> _currentDialog;
        List<string> dialogList = new List<string>();
        //List<string> actorList = new List<string>();
        if (_save.TryGetValue(Eventid, out _currentDialog))
        {
            // 해당 이벤트에 속한 모든 대화 정보를 반환합니다.
            foreach (KeyValuePair<string, List<string>> pair in _currentDialog)
            {
                // actorID 를 키로서 사용함 -> 이것을 이용해서 dialog 신호를 보내고 싶음. 
                string _actorID = pair.Key;

                foreach (string _context in pair.Value)
                {
                    // 임시로 ActorID 도 함께 확인하기 위해 같이 가져옴. 
                    dialogList.Add(_actorID);
                    dialogList.Add(/*_actorID + " | " + */_context);
                    //actorList.Add(_actorID);
                    //dialogList.Add(_context);
                }
            }
        }
        return dialogList;
    }

}