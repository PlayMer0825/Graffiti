using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region Singleton
    private SceneController _instance = null;
    public SceneController Instance { get { return _instance; } }

    #endregion

    #region Local Variables
    private WaitForSeconds m_sceneLoadingWait = null;
    [SerializeField] private float m_loadWaitInterval = 0.1f;
    [SerializeField] private float m_FakeLoadingThreshold = 4.0f;
                     private float m_SceneLoadindgProgress = 0.0f;

    #endregion

    #region Properties
    /// <summary>
    /// 외부 오브젝트가 씬 로드 매니저로부터 로드 진행도를 받기 위한 Property
    /// </summary>
    public float SceneLoadingProgress { 
        get => m_SceneLoadindgProgress; 
    }

    #endregion

    private void Awake() {
        if(_instance != null)
            Destroy(gameObject);

        _instance = this;
        DontDestroyOnLoad(gameObject);

        m_sceneLoadingWait = new WaitForSeconds(m_loadWaitInterval);
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Space)) {
            ChangeSceneTo(1);
        }
    }

    /// <summary>
    /// 다음 씬 로드 요청을 받는 함수
    /// </summary>
    /// <param name="sceneIndex">Next Scene's Build Index to load</param>
    public void ChangeSceneTo(int sceneIndex) {
        //TODO: 씬을 페이드인/아웃 할거면 여기서 Completed에 코루틴 넣어주고 페이드 인/아웃 호출
        StartCoroutine(CoStartLoading(sceneIndex));
    }

    /// <summary>
    /// 다음 씬을 비동기적으로 로드하기 위한 Coroutine함수
    /// </summary>
    /// <param name="sceneIndex">Next Scene's Build Index to load</param>
    private IEnumerator CoStartLoading(int sceneIndex) {
        float fakeLoading = 0.0f;
        bool isDone = false;

        AsyncOperation loadTask = SceneManager.LoadSceneAsync("TestScene2");
        loadTask.allowSceneActivation = false;

        while(true) {
            fakeLoading += m_loadWaitInterval;
            m_SceneLoadindgProgress = loadTask.progress;

            isDone = ( fakeLoading >= m_FakeLoadingThreshold ) &&
                     ( loadTask.progress >= 0.9f );

            if(isDone) break;
            yield return m_sceneLoadingWait;
        }

        loadTask.allowSceneActivation = true;
        m_SceneLoadindgProgress = 0.0f;
        //TODO: 씬을 페이드인/아웃 할거면 여기서 페이드 아웃 진행

        yield break;
    }
}
