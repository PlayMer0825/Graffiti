using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PicturePiece : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [Header("퍼즐 ")]
    [SerializeField] int snapOffset = 60;
    [SerializeField] Sprite puzzle_sprite;
    [SerializeField] private Collider2D dropZoneCol;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 startPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 클릭 시 처음 위치 저장
        startPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그가 시작될 때 오브젝트를 맨 앞으로 가져오기
        transform.SetAsLastSibling();

        // 드래그 시작 시 위치 고정
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그 종료 시 위치 고정 해제
        canvasGroup.blocksRaycasts = true;

        // 드롭 영역과 충돌하는 경우
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<Collider2D>() == dropZoneCol)
        {
            // 위치 고정
            transform.position = dropZoneCol.transform.position;
            GetComponent<BoxCollider2D>().enabled = false; // BoxCollider2D로 수정
        }
        else // 드롭 영역과 충돌하지 않는 경우
        {
            // 처음 위치로 돌아감
            transform.position = startPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중에는 오브젝트를 마우스 위치로 이동
        rectTransform.anchoredPosition += eventData.delta;
    }

    public Vector3 GetStartPosition()
    {
        return GetStartPosition();
    }
}

