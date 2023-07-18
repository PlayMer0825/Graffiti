using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


[Serializable]
public class Data
{
    public Vector3 playerPosition;
    public int sceneIndex;
    public bool board;

    // 딕셔너리 형태로 활성화 비활성화 된 오브젝트들 이름이랑 bool 을 저장
    public Dictionary<string, bool> activeObjectState;

    public Data()
    {
        activeObjectState = new Dictionary<string, bool>();
    }
}
