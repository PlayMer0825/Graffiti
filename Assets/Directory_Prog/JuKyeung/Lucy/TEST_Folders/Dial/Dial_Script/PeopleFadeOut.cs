using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 휠 수에 따라서 배치해 놓은 이미지 비율에 따라 점점 사람 사라지게 만들거임 
/// </summary>
public class PeopleFadeOut : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 1f; // 이미지가 서서히 사라지는 데 걸리는 시간

    public Image[] image;
    private float currentFadeTime;
    private bool fadingOut;

    private int currentIndex;

    public void EventCheck()
    {
        Debug.Log("이벤트 체크" );
        Color color = image[currentIndex].color;

        for(int i =0; i <image.Length; i++)
        {

            color.a = 0f;
        }
    }
}
