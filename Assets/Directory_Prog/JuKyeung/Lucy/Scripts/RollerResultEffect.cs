using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RollerResultEffect : MonoBehaviour
{
    [SerializeField] private Image resultImage;

    public UnityEvent RollerPuzzleEndEvent;

    private bool isAnimating = false; 

    private void Start()
    {
        resultImage.gameObject.SetActive(false);
        isAnimating = false;
}
public void ResultAnim()
    {
        StartCoroutine(ResultAnimCoroutine());
    }

    public IEnumerator ResultAnimCoroutine()
    {
        if (isAnimating) yield break;

        isAnimating = true;
        float duration = 1.5f;
        float timer = 0f;

        Color imageColor = resultImage.color;
        resultImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 0f); // 알파값을 0으로 설정하여 시작

        resultImage.gameObject.SetActive(true); // 이미지 활성화

        while (timer < duration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / duration);
            resultImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);

            timer += Time.deltaTime;
            yield return null;
        }
        resultImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 255);
        RollerPuzzleEndEvent.Invoke();
        Debug.Log("정상적");

    }
}
