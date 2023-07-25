using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : UIBehaviour
{
	[SerializeField]
	Text uiText;

	[SerializeField]
	Image uiBackground;
	[SerializeField]
	Image uiIcon;

	private readonly Color[] colors = new Color[] {
		new Color(1, 1, 1, 1),
		new Color(0.9f, 0.9f, 1, 1),
	};

	public void UpdateItem(int count)
	{
		uiText.text = (count + 1).ToString("00"); // 01부터 자동으로 숫자 카운트 (올라감 ) 
		uiBackground.color = colors[Mathf.Abs(count) % colors.Length];
		uiIcon.sprite = Resources.Load<Sprite>((Mathf.Abs(count) % 30 + 1).ToString("Dial_icon000")); // 월 별 이미지(필요 시 ) 네이밍 Dial_icon 000
	}
}