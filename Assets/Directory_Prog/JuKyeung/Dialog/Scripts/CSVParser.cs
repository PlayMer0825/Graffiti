using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;
using System;

public class CSVParser : MonoBehaviour
{
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };

    public static List<Dictionary<string, object>> Read(string file) // CSV 파일을 읽어들임 file 매개변수를 받아서  List~~ 형식으로 반환... 
    {
        var list = new List<Dictionary<string, object>>(); // list = 리스트 초기화
        TextAsset data = Resources.Load(file) as TextAsset; // TextAsset data =  변수 안에 CSV 파일을 읽어옴 

        var lines = Regex.Split(data.text, LINE_SPLIT_RE); // lines = 할당된 CSV 내용들을 줄단위로 분할

        string csvText = file.Substring(0, file.Length - 1); // file 매개변수에서 마지막 문자를 삭제한 문자열을 저장 // 삭제해도 될듯 

        if (lines.Length <= 1) return list; // lines 에 분할된 줄의 수가 1 이하라면 빈 리스트를 반환

        var header = Regex.Split(lines[0], SPLIT_RE); // header = lines의 0번째 줄을 분할 EventName| ActorID|  Context|
        for (var i = 1; i < lines.Length; i++) // CSV 파일 내용을 각각 분할하여 딕셔너리 형태로 저장 
        {
            var values = Regex.Split(lines[i], SPLIT_RE); // values 에 1번째 줄부터 하나씩 해당 줄의 내용들을 다시 분할한다. -> 얘네또한  |  |  | 
            //if (values.Length == 0 || values[0] == "") continue; // 예외처리. values의 내용이 없거나 0번째면은 다시 로드 ?

            if (values.Length == 0) continue; // *** 수정 부분  해당 줄이 비어있을 경우 건너뒨다. 

            var entry = new Dictionary<string, object>(); // entty = 각 한줄한줄의 값을 저장할 딕셔너리 생성
            for (var j = 0; j < header.Length && j < values.Length; j++) // 분할된 CSV 파일을 한줄씩 처리, 각 줄의 내용들을 다시 분할,각 열을 딕셔너리 형태로 저장...
            {
                string value = values[j]; // CSV 파일에서 분할된 각 줄의 내용 values 을 한 열 씩 처리하는 부분
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value; // 해당 처리가 완료된 후의 문자열을 object 형식에 저장
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f)) // value 문자열의 값을 int 정수형식으로 변환시켜 f 에 저장 
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue; // 분할된 각 줄의 내용을 딕셔너리 형태로 저장 [header[j]] 은 각 열의 이름(헤더) 
            }
            list.Add(entry);
        }
        return list;
    }
}
