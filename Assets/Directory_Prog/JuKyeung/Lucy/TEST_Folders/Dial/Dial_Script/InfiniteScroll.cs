using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class InfiniteScroll : UIBehaviour
{
	[SerializeField]
	private RectTransform itemPrototype;

	[SerializeField, Range(0, 30)]
	int instantateItemCount = 9;

	[SerializeField]
	private Direction direction;

	public OnItemPositionChange onUpdateItem = new OnItemPositionChange();

	[System.NonSerialized]
	public LinkedList<RectTransform> itemList = new LinkedList<RectTransform>();


	protected float diffPreFramePosition = 0;

	protected int currentItemNo = 0;

	[Header("사라지는 마법옵션 $20")]



	[SerializeField] private int maxWheelCount; // 최대로 돌릴 수 있는 휠 횟수
	[SerializeField] private int fadeOutObjCount; // 몇번에 걸쳐 오브젝트들을 사라지게 만들것인지
	[SerializeField] private float fadeTime; // 사라지는데 걸리는 시간
	[SerializeField] private int oneTimeDeleteObject; // 한번에 사라질 오브젝트 갯수

	[SerializeField] private Image[] peopleImages;


	private int currentWheelCount = 0;
	private int checkWheelCount = 0;

	public UnityEvent scrollCheckEvent; // 스크롤 조건 만족 시 이벤트 
	public enum Direction
	{
		Vertical,
		Horizontal,
	}

	// cache component

	private RectTransform _rectTransform;
	protected RectTransform rectTransform
	{
		get
		{
			if (_rectTransform == null) _rectTransform = GetComponent<RectTransform>();
			return _rectTransform;
		}
	}

	private float anchoredPosition
	{
		get
		{
			return direction == Direction.Vertical ? -rectTransform.anchoredPosition.y : rectTransform.anchoredPosition.x;
		}
	}

	private float _itemScale = -1;
	public float itemScale
	{
		get
		{
			if (itemPrototype != null && _itemScale == -1)
			{
				_itemScale = direction == Direction.Vertical ? itemPrototype.sizeDelta.y : itemPrototype.sizeDelta.x;
			}
			return _itemScale;
		}
	}

	protected override void Start()
	{
		var controllers = GetComponents<MonoBehaviour>()
				.Where(item => item is IInfiniteScrollSetup)
				.Select(item => item as IInfiniteScrollSetup)
				.ToList();

		// create items

		var scrollRect = GetComponentInParent<ScrollRect>();
		scrollRect.horizontal = direction == Direction.Horizontal;
		scrollRect.vertical = direction == Direction.Vertical;
		scrollRect.content = rectTransform;

		itemPrototype.gameObject.SetActive(false);

		for (int i = 0; i < instantateItemCount; i++)
		{
			var item = GameObject.Instantiate(itemPrototype) as RectTransform;
			item.SetParent(transform, false);
			item.name = i.ToString();
			item.anchoredPosition = direction == Direction.Vertical ? new Vector2(0, -itemScale * i) : new Vector2(itemScale * i, 0);
			itemList.AddLast(item);

			item.gameObject.SetActive(true);

			foreach (var controller in controllers)
			{
				controller.OnUpdateItem(i, item.gameObject);
			}
		}

		foreach (var controller in controllers)
		{
			controller.OnPostSetupItems();
		}
	}

	void Update()
	{
		if (itemList.First == null)
		{
			return;
		}

		// while 문 돌 때마다 처음으로.
		while (anchoredPosition - diffPreFramePosition < -itemScale * 2)
		{
			diffPreFramePosition -= itemScale;

			var item = itemList.First.Value;
			itemList.RemoveFirst();
			itemList.AddLast(item);

			var pos = itemScale * instantateItemCount + itemScale * currentItemNo;
			item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);

			onUpdateItem.Invoke(currentItemNo + instantateItemCount, item.gameObject);

			currentItemNo++;

		
			//if(currentItemNo % itemList.Count == 0 && currentItemNo != 0)
   //         {
			//	Debug.Log("현재 휠 수: " + currentWheelCount);
			//	Debug.Log("현재 휠 수: " + checkWheelCount);
			//	currentWheelCount++;
			//	checkWheelCount++;
				
			//}

			int countResult = maxWheelCount / fadeOutObjCount;

			//if (currentWheelCount  == countResult) // currentWheelCheck 의 카운트가 "maxWheelCount  / fadeOutCount; 
			//{
			//	currentWheelCount = 0;

			//	int deleteCount = Mathf.Min(oneTimeDeleteObject, peopleImages.Length);
			//	int startIndex = (currentWheelCount - 1) * deleteCount;
			//	int endIndex = startIndex + deleteCount;
			//	// scrollCheckEvent.Invoke();
			//	for (int i = startIndex; i < endIndex && i < peopleImages.Length; i++)
			//	{
			//		if (i >= 0 && i < peopleImages.Length && peopleImages[i].gameObject.activeSelf)
			//		{
			//			StartCoroutine(FadeOutImage(peopleImages[i]));
			//		}
			//	}

			//}

			int totalCount = maxWheelCount * fadeOutObjCount;

			// currentWheelCount와 totalCount를 비교하여 해당 조건을 만족하는 경우에만 처리합니다.
			if (currentWheelCount < totalCount)
			{
				if (currentItemNo % itemList.Count == 0 && currentItemNo != 0)
				{
					currentWheelCount++;
					checkWheelCount++;

					Debug.Log("currentWheelCount : " + currentWheelCount + " || " + "CheckWheelCount : " + checkWheelCount);

					if (currentWheelCount % fadeOutObjCount == 0)
					{
						int deleteCount = Mathf.Min(oneTimeDeleteObject, peopleImages.Length);
						int startIndex = (currentWheelCount / fadeOutObjCount - 1) * deleteCount;
						int endIndex = startIndex + deleteCount;

						for (int i = startIndex; i < endIndex && i < peopleImages.Length; i++)
						{
							if (i >= 0 && i < peopleImages.Length && peopleImages[i].gameObject.activeSelf)
							{
								StartCoroutine(FadeOutImage(peopleImages[i]));
							}
						}
					}
				}
			}

		}

		while (anchoredPosition - diffPreFramePosition > 0)
		{
			diffPreFramePosition += itemScale;

			var item = itemList.Last.Value;
			itemList.RemoveLast();
			itemList.AddFirst(item);

			currentItemNo--;

			var pos = itemScale * currentItemNo;
			item.anchoredPosition = (direction == Direction.Vertical) ? new Vector2(0, -pos) : new Vector2(pos, 0);
			onUpdateItem.Invoke(currentItemNo, item.gameObject);
        }

		if(checkWheelCount >= maxWheelCount)
        {
			Debug.Log("퍼즐 끝나야 합니다");
			// 여기에서 유니티 이벤트를 넣어서 처리하는것이 좋을 것 같아보이긴 함;; 

        }

    }

	private System.Collections.IEnumerator FadeOutImage(Image image)
	{
		float elapsedTime = 0;
		Color originalColor = image.color;
		Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

		while (elapsedTime < fadeTime)
		{
			elapsedTime += Time.deltaTime;
			float t = Mathf.Clamp01(elapsedTime / fadeTime);
			image.color = Color.Lerp(originalColor, targetColor, t);
			yield return null;
		}

		image.color = targetColor;
		image.gameObject.SetActive(false);
	}

	[System.Serializable]
    public class OnItemPositionChange : UnityEvent<int, GameObject> { }

}