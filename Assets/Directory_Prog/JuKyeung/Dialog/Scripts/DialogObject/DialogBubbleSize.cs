using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBubbleSize : MonoBehaviour
{
    public TextMeshProUGUI text;
    public RectTransform bubbleRect;

    private void Start()
    {
        text.text = "";
    }

    public void SetText(string _text)
    {
        text.text = _text;

        Vector2 size = text.GetPreferredValues(_text);
        bubbleRect.sizeDelta = new Vector2(size.x + 5, size.y + 5);
    }
}
