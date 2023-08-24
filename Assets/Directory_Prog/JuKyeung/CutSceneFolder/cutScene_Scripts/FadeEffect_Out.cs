using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FadeEffect_Out : MonoBehaviour
{
    public Image image;
    public UnityEvent fadeEndEvent;

    private void OnEnable()
    {
        StartCoroutine(FadeOutCorutine());
    }
    IEnumerator FadeOutCorutine()
    {
        Color imageColor = image.color;
        float fadeDuration = 2.0f; // 페이드 아웃에 걸리는 시간
        float fadeStartTime = Time.time;

        while (imageColor.a < 1.0f)
        {
            float elapsed = Time.time - fadeStartTime;
            float normalizedTime = Mathf.Clamp01(elapsed / fadeDuration);
            imageColor.a = normalizedTime;
            image.color = imageColor;

            yield return null;
        }

        fadeEndEvent.Invoke();
    }
}
