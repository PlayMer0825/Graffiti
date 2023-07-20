using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//테스트
[RequireComponent(typeof(AudioSource))]
//여까지
public class Telepoter_This : MonoBehaviour
{
    //테스트
    [SerializeField] private AudioSource m_source = null;
    //여까지
    [SerializeField] private GameObject m_doorIcon = null;
    public Image image;
    public static bool telepoter = false;

    //테스트
    private void Awake()
    {
        m_source = GetComponent<AudioSource>();
    }
    //여까지

    IEnumerator FadeCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
        
        SceneManager.LoadScene(target_SceneName);
    }


    private bool m_isPlayerTriggered = false;
    [SerializeField]
    private string target_SceneName = string.Empty;

    private void OnTriggerEnter(Collider other)
    {
        if (m_isPlayerTriggered)
            return;

        if (other.CompareTag("Player"))
        {
            m_isPlayerTriggered = true;
            //TODO: UI 켜주기
            if (m_doorIcon == null)
                return;

            m_doorIcon.SetActive(true);

            //테스트
            if (m_source == null || m_source.clip == null)
                return;

            m_source.PlayOneShot(m_source.clip);
            //여까지
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0) && m_isPlayerTriggered)
        {
            PlayerMove_SIDE.isLoad = false;
            telepoter = true;
            StartCoroutine(FadeCoroutine());
            if (target_SceneName == string.Empty)
                return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_isPlayerTriggered == false)
            return;

        if (other.CompareTag("Player"))
        {
            m_isPlayerTriggered = false;

            if (m_doorIcon == null)
                return;

            m_doorIcon.SetActive(false);
        }
    }
}
