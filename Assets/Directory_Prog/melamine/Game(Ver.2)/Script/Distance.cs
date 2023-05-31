using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Distance : MonoBehaviour
{
    
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

    void Start()
    {
        image1.SetActive(false);
        image2.SetActive(false);
        image.color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        distance = other.position.x - other1.transform.position.x;
        distanceText.text = distance.ToString();

        if(other.position.x<=other1.position.x)
        {
            Board.isEnd = true;
            image.color = new Color(0, 0, 0, 1);
            image1.SetActive(true);
            image2.SetActive(true);
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
