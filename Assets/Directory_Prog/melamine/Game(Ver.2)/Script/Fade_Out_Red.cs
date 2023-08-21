using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fade_Out_Red : MonoBehaviour
{
    [SerializeField]
    private string target_SceneName = string.Empty;
    public Image image;
    static public bool isRed = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRed)
        {
            isRed = false;
            StartCoroutine(FadeCoroutine());
        }
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
        SceneManager.LoadScene(target_SceneName);
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (isRed==true&&other.CompareTag("Player"))
        {
            StartCoroutine(FadeCoroutine());
        }
    }*/

    public void Check()
    {

        Debug.Log("check");
        if (SceneManager.GetActiveScene().name == "Chapter_02(Riverside)")
        {
            Debug.Log("red");
            isRed = true;
        }
    }
}
