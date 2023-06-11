using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Distance : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    float fadeCount = 1;
    public Image image;
    public GameObject image1;
    public GameObject image2;
    public Text distanceText;
    float distance;
    public Transform other;
    public Transform other1;
    [SerializeField]
    private string target_SceneName = string.Empty;

    public bool isEnd = false;
    public bool isSame = false;

    void Start()
    {
        image1.SetActive(false);
        image2.SetActive(false);
        //image.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        distance = other.position.x - other1.transform.position.x;
        if(distance>0)
        {
            distanceText.text = distance.ToString("F1");
        }
        

        if(other.position.x<=other1.position.x)
        {
            cam.Follow = null;
            isSame = true;
            //Board.isEnd = true;
            //image.color = new Color(0, 0, 0, 1);
            //image1.SetActive(true);
            //image2.SetActive(true);
        }

        if(isSame&&!isEnd)
        {
            StartFadeOut();
            isEnd = true;
        }


    }
    public void StartFadeOut()
    {
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        float fadeCount = 0;
        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color = new Color(0, 0, 0, fadeCount);
        }
    }

    /*IEnumerator FadeCoroutine()
    {
        image.color = new Color(0, 0, 0, 1);
        if (target_SceneName == string.Empty)
            yield return null;
        image1.SetActive(true);

        Invoke("OnInvoke", 1.5f);

    }
    void OnInvoke()
    {
        SceneManager.LoadScene(target_SceneName);
    }*/
}
