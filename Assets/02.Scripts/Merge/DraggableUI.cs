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
        GameManager.Instance._saveManager._userSave.RemoveItemInfo(this.GetComponent<ItemInform>());

        _previousParent = transform.parent;

        transform.SetParent(_canvas);
        transform.SetAsLastSibling();

        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = eventData.position;
        pos.z = 0;

        _rect.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.blocksRaycasts = true;

        if (transform.parent == _canvas)
        {
            transform.SetParent(_previousParent);
            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;

            GameManager.Instance._saveManager._userSave.RelocateItem(_previousParent.GetComponent<SlotInform>()._slotNumber, this.gameObject);
        }
        else
        {
            

            //GameManager.Instance._saveManager._userSave.RemoveItem(GetComponent<ItemInform>()._itemData);

            if (_previousParent.GetComponent<MapInform>())
            {
                GameManager.Instance._saveManager._userSave.RemoveTowerInfo(this.GetComponent<TowerInform>());
            }

            Transform slot = eventData.pointerDrag.transform.parent;

            // ���ΰ���?
            MapInform mapInform = slot.GetComponent<MapInform>();

            // �ű� ���� ����
            if (mapInform != null)
            {
                if (mapInform._isWay || !GameManager.Instance._saveManager._userSave.IsCanBuildBee())
                {
                    transform.SetParent(_previousParent);
                    _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;

                    GameManager.Instance._saveManager._userSave.RelocateItem(_previousParent.GetComponent<SlotInform>()._slotNumber, this.gameObject);
                    return;
                }

                // �ڽ��� �� ���ΰ�?
                if (slot.childCount > 1)
                {
                    // �������� ����
                    ItemInform item_1 = slot.GetChild(0).GetComponent<ItemInform>();
                    ItemInform item_2 = slot.GetChild(1).GetComponent<ItemInform>();

                    // ���̸� �ڸ� �ٲ�
                    if (item_2._itemData._itemType == ItemType.BEE)
                    {
                        // ��ž�� ��ġ��
                        GameManager.Instance._saveManager._userSave.AddTowerInfo(item_2.GetComponent<TowerInform>());

                        slot.GetChild(0).GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                        slot.GetChild(0).SetParent(_previousParent);
                    }
                    // ���� �ƴϸ� �ٽ� ���ư�
                    else
                    {
                        transform.SetParent(_previousParent);
                        _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;

                        GameManager.Instance._saveManager._userSave.RelocateItem(_previousParent.GetComponent<SlotInform>()._slotNumber, this.gameObject);
                        return;
                    }
                }
                // �ڽ��� �ϳ���
                else
                {
                    ItemInform item = slot.GetChild(0).GetComponent<ItemInform>();

                    // ���� ������ ���̸� 
                    if (item._itemData._itemType == ItemType.BEE)
                    {
                        // ��ž�� ��ġ��
                        GameManager.Instance._saveManager._userSave.AddTowerInfo(item.GetComponent<TowerInform>());
                        return;
                    }
                    else
                    {
                        transform.SetParent(_previousParent);
                        _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;

                        GameManager.Instance._saveManager._userSave.RelocateItem(_previousParent.GetComponent<SlotInform>()._slotNumber, this.gameObject);
                        return;
                    }

                }
            }
            else
            {
                if (slot.childCount > 1)
                {
                    ItemInform item_1 = slot.GetChild(0).GetComponent<ItemInform>();
                    ItemInform item_2 = slot.GetChild(1).GetComponent<ItemInform>();

                    // �ű�� �� ������ ���̶��
                    if (_previousParent.GetComponent<MapInform>() != null)
                    {
                        if (item_1._itemData._itemType == ItemType.BEE)
                        {
                            // ��ž ����
                            GameManager.Instance._saveManager._userSave.RemoveTowerInfo(item_1.GetComponent<TowerInform>());
                            GameManager.Instance._saveManager._userSave.AddTowerInfo(item_2.GetComponent<TowerInform>());

                            slot.GetChild(0).GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                            slot.GetChild(0).SetParent(_previousParent);
                            return;
                        }
                        else
                        {
                            transform.SetParent(_previousParent);
                            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;
                            return;
                        }
                    }

                    if (item_1._itemData._itemType == ItemType.EGG || item_2._itemData._itemType == ItemType.EGG)
                    {
                        if (item_1._itemData._itemType == ItemType.BEE || item_2._itemData._itemType == ItemType.BEE)
                        {
                            GameManager.Instance._saveManager._userSave.RemoveItem(item_1._itemData);

                            int i_1 = _previousParent.GetComponent<SlotInform>()._slotNumber;
                            int i_2 = eventData.pointerDrag.transform.parent.GetComponent<SlotInform>()._slotNumber;
                            item_1._itemData._slotNumber = i_1;
                            item_2._itemData._slotNumber = i_2;

                            GameManager.Instance._saveManager._userSave.RelocateItem(i_1, item_1.gameObject);
                            GameManager.Instance._saveManager._userSave.RelocateItem(i_2, item_2.gameObject);

                            slot.GetChild(0).GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                            slot.GetChild(0).SetParent(_previousParent);
                            return;
                        }

                        if (item_1._itemData._itemType == ItemType.EGG && item_2._itemData._itemType == ItemType.EGG)
                        {
                            GameManager.Instance._saveManager._userSave.RemoveItem(item_1._itemData);

                            int i_1 = _previousParent.GetComponent<SlotInform>()._slotNumber;
                            int i_2 = eventData.pointerDrag.transform.parent.GetComponent<SlotInform>()._slotNumber;
                            item_1._itemData._slotNumber = i_1;
                            item_2._itemData._slotNumber = i_2;

                            GameManager.Instance._saveManager._userSave.RelocateItem(i_1, item_1.gameObject);
                            GameManager.Instance._saveManager._userSave.RelocateItem(i_2, item_2.gameObject);

                            slot.GetChild(0).GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                            slot.GetChild(0).SetParent(_previousParent);
                            return;
                        }

                        if (item_1._itemData._itemType == ItemType.EGG)
                        {
                            item_1._itemData._itemType = ItemType.BEE;
                            item_1._itemData._itemGrade = item_2._itemData._itemGrade;
                            Destroy(item_2.gameObject);
                        }
                        else
                        {
                            item_2._itemData._itemType = ItemType.BEE;
                            item_2._itemData._itemGrade = item_1._itemData._itemGrade;
                            GameManager.Instance._saveManager._userSave.RemoveItem(item_1._itemData);
                            GameManager.Instance._saveManager._userSave.RelocateItem(eventData.pointerDrag.transform.parent.GetComponent<SlotInform>()._slotNumber, item_2.gameObject);

                            Destroy(item_1.gameObject);
                        }

                        return;
                    }

                    if (item_1._itemData._itemType != ItemType.BEE || item_2._itemData._itemType != ItemType.BEE)
                    {
                        // �������� Ÿ���� ������
                        if (item_1._itemData._itemType == item_2._itemData._itemType)
                        {
                            // �������� ��޵� ���ٸ�
                            if (item_1._itemData._itemGrade == item_2._itemData._itemGrade)
                            {
                                // ������ �� �� ���ְ� �ϳ��� ���� ������
                                item_1._itemData._itemGrade++;
                                Destroy(item_2.gameObject);
                                return;
                            }
                        }
                    }

                    item_1._itemData._slotNumber = _previousParent.GetComponent<SlotInform>()._slotNumber;
                    slot.GetChild(0).GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                    slot.GetChild(0).SetParent(_previousParent);
                    return;
                }

                this.GetComponent<ItemInform>()._itemData._slotNumber = eventData.pointerDrag.transform.parent.GetComponent<SlotInform>()._slotNumber;
                GameManager.Instance._saveManager._userSave.RelocateItem(eventData.pointerDrag.transform.parent.GetComponent<SlotInform>()._slotNumber, this.gameObject);
            }
        }
    }

}
