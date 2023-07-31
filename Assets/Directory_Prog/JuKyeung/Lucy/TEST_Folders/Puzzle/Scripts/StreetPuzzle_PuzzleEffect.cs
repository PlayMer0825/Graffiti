using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StreetPuzzle_PuzzleEffect : MonoBehaviour
{

    public void ActiveCrowd(Image _img)
    {
        StartCoroutine(ActiveCrowdCorutine(_img));
    }
    private IEnumerator ActiveCrowdCorutine(Image _img)
    {
        float fadeDuration = 2.0f;
        float timer = 0f;

        Color crowdImgColor = _img.color;

        _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 0f);

        _img.gameObject.SetActive(true);

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            crowdImgColor.a += 0.3f * Time.deltaTime;

            _img.color = crowdImgColor;
            yield return null;
        }

        crowdImgColor.a = 1.0f;
        _img.color = crowdImgColor;
    }
}
