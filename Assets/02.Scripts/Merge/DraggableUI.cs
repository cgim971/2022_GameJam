using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Transform _canvas;
    private Transform _previousParent;
    private RectTransform _rect;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>().transform;
        _rect = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _previousParent = transform.parent;

        transform.SetParent(_canvas);
        transform.SetAsLastSibling();

        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rect.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == _canvas)
        {
            transform.SetParent(_previousParent);
            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;
        }
        else
        {
            // 만약 여러 개라면
            Transform slot = eventData.pointerDrag.transform.parent;
            Debug.Log(slot.name);
            if (slot.childCount > 1)
            {
                ItemInform item_1 = slot.GetChild(0).GetComponent<ItemInform>();
                ItemInform item_2 = slot.GetChild(1).GetComponent<ItemInform>();

                if (item_1._itemType == ItemType.EGG && item_2._itemType == ItemType.EGG)
                {
                    return;
                }

                // 아이템의 타입이 같은지
                if (item_1._itemType == item_2._itemType)
                {
                    // 아이템이 등급도 같다면
                    if (item_1._itemGrade == item_2._itemGrade)
                    {
                        Debug.Log("가타아아아");
                        // 아이템 한 개 없애고 하나를 다음 레벨로
                        item_1._itemGrade++;
                        Destroy(item_2.gameObject);
                        return;
                    }
                }
                Debug.Log("이거 되냐?");

                slot.GetChild(0).GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                slot.GetChild(0).SetParent(_previousParent);
            }
        }
    }

}
