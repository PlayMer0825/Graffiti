using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_In : MonoBehaviour
{
    public Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeCoroutine());
    }

    IEnumerator FadeCoroutine()
    {
        float fadeCount = 1.0f;
        while(fadeCount>0f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            image.color=new Color(0,0,0,fadeCount);
        }
    }

    private void OnDisable()
    {
        image.color = new Color(0, 0, 0, 255);
    }

}
