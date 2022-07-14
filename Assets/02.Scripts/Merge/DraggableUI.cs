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
        Vector3 pos = eventData.position;
        pos.z = 0;

        _rect.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        _canvasGroup.alpha = 1.0f;
        _canvasGroup.blocksRaycasts = true;

        // �ڽ� �θ� ���Ŀ� ���� ����Ʈ �� ����
        if (_previousParent.GetComponent<MapInform>() != null)
        {
            GameManager.Instance._saveManager._userSave.RemoveTowerInfo(transform.GetComponent<TowerInform>());
        }
        else
        {
            GameManager.Instance._saveManager._userSave.RemoveItemInfo(transform.GetComponent<ItemInform>());
        }

        // �θ� ĵ������
        if (transform.parent == _canvas)
        {
            transform.SetParent(_previousParent);
            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;

            // ���� ��ġ��
            if (_previousParent.GetComponent<MapInform>() != null)
            {
                GameManager.Instance._saveManager._userSave.AddTowerInfo(transform.GetComponent<TowerInform>());
            }
            else
            {
                GameManager.Instance._saveManager._userSave.RefreshItemInfo(transform.GetComponent<ItemInform>());
            }
            return;
        }
        // ĵ������ �ƴϸ�
        else
        {
            MapInform map = eventData.pointerDrag.transform.GetComponentInParent<MapInform>();
            SlotInform slot = eventData.pointerDrag.transform.GetComponentInParent<SlotInform>();

            // ��� �巡�׸� �ߴ���
            // ��
            if (_previousParent.GetComponent<MapInform>() != null)
            {
                // �巡�� �� ���� ��������
                //�Ű��� ���� ������
                Transform parent = eventData.pointerDrag.transform.parent;
                int count = parent.childCount;

                // �ڽ��� ��������?
                if (count > 1)
                {
                    Transform item_1 = parent.GetChild(0);
                    Transform item_2 = parent.GetChild(1);

                    if (map != null)
                    {
                        // ��ž ��ġ�� �ٲ��

                        int index_1 = map._mapNumber;
                        int index_2 = _previousParent.GetComponent<MapInform>()._mapNumber;

                        item_1.GetComponent<TowerInform>().towerData._slotNumber = index_2;
                        transform.GetComponent<TowerInform>().towerData._slotNumber = index_1;

                        GameManager.Instance._saveManager._userSave.RemoveTowerInfo(item_1.GetComponent<TowerInform>());

                        GameManager.Instance._saveManager._userSave.AddTowerInfo(transform.GetComponent<TowerInform>());
                        GameManager.Instance._saveManager._userSave.AddTowerInfo(transform.GetComponent<TowerInform>());

                        item_1.GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                        item_1.SetParent(_previousParent);
                        return;
                    }
                    else
                    {
                        // ��ž�ΰ���?
                        if (item_1.GetComponent<ItemInform>()._itemData._itemType == ItemType.BEE)
                        {
                            GameManager.Instance._saveManager._userSave.RemoveItemInfo(item_1.GetComponent<ItemInform>());

                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(item_2.GetComponent<ItemInform>());
                            GameManager.Instance._saveManager._userSave.AddTowerInfo(item_2.GetComponent<TowerInform>());

                            item_1.GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                            item_1.SetParent(_previousParent);
                            return;
                        }
                        // �ƴѵ���
                        else
                        {
                            transform.SetParent(_previousParent);
                            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;
                            return;
                        }
                    }
                }
                // �ƴѵ���
                else
                {
                    if (map != null)
                    {
                        transform.GetComponent<TowerInform>().towerData._slotNumber = map._mapNumber;

                        GameManager.Instance._saveManager._userSave.AddTowerInfo(transform.GetComponent<TowerInform>());
                        return;
                    }
                    // ��������
                    else
                    {
                        // ������ ����Ʈ�� �߰�
                        transform.GetComponent<ItemInform>()._itemData._slotNumber = slot._slotNumber;

                        GameManager.Instance._saveManager._userSave.RefreshItemInfo(transform.GetComponent<ItemInform>());
                        return;
                    }
                }
            }
            // ����
            else
            {
                Transform parent = eventData.pointerDrag.transform.parent;
                int count = parent.childCount;

                // �ڽ��� ��������?
                if (count > 1)
                {
                    Transform item_1 = parent.GetChild(0);
                    Transform item_2 = parent.GetChild(1);

                    // ���� 
                    if (map != null)
                    {
                        if (transform.GetComponent<ItemInform>()._itemData._itemType == ItemType.BEE)
                        {
                            transform.GetComponent<TowerInform>().towerData._slotNumber = map._mapNumber;

                            GameManager.Instance._saveManager._userSave.AddTowerInfo(transform.GetComponent<TowerInform>());
                            return;
                        }
                        else
                        {
                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(transform.GetComponent<ItemInform>());

                            transform.SetParent(_previousParent);
                            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;
                            return;
                        }
                    }
                    else
                    {
                        ItemData itemData_1 = item_1.GetComponent<ItemInform>()._itemData;
                        ItemData itemData_2 = item_2.GetComponent<ItemInform>()._itemData;

                        int index_1 = slot._slotNumber;
                        int index_2 = _previousParent.GetComponent<SlotInform>()._slotNumber;

                        if (itemData_1._itemType == ItemType.EGG && itemData_2._itemType == ItemType.HONEY)
                        {
                            item_1.GetComponent<ItemInform>()._itemData._itemType = ItemType.BEE;
                            GameManager.Instance._saveManager._userSave.RemoveItemInfo(item_1.GetComponent<ItemInform>());
                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(item_1.GetComponent<ItemInform>());
                            GameManager.Instance._saveManager._userSave.DeleteItem(item_2.gameObject);
                            return;
                        }
                        else if (itemData_1._itemType == ItemType.HONEY && itemData_2._itemType == ItemType.EGG)
                        {
                            item_1.GetComponent<ItemInform>()._itemData._itemType = ItemType.BEE;
                            GameManager.Instance._saveManager._userSave.RemoveItemInfo(item_1.GetComponent<ItemInform>());
                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(item_1.GetComponent<ItemInform>());
                            GameManager.Instance._saveManager._userSave.DeleteItem(item_2.gameObject);
                            return;
                        }
                        else if (itemData_1._itemType == ItemType.HONEY && itemData_2._itemType == ItemType.HONEY)
                        {
                            item_1.GetComponent<ItemInform>()._itemData._itemGrade += 1;
                            GameManager.Instance._saveManager._userSave.RemoveItemInfo(item_1.GetComponent<ItemInform>());
                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(item_1.GetComponent<ItemInform>());
                            GameManager.Instance._saveManager._userSave.DeleteItem(item_2.gameObject);
                            return;
                        }
                        else
                        {
                            item_1.GetComponent<ItemInform>()._itemData._slotNumber = index_2;
                            transform.GetComponent<ItemInform>()._itemData._slotNumber = index_1;

                            GameManager.Instance._saveManager._userSave.RemoveItemInfo(item_1.GetComponent<ItemInform>());

                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(item_1.GetComponent<ItemInform>());
                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(transform.GetComponent<ItemInform>());

                            item_1.GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                            item_1.SetParent(_previousParent);
                            return;
                        }
                    }
                }
                // �ƴѵ���
                else
                {
                    if (map != null)
                    {
                        // Ÿ������
                        if (transform.GetComponent<ItemInform>()._itemData._itemType == ItemType.BEE)
                        {
                            transform.GetComponent<TowerInform>().towerData._slotNumber = map._mapNumber;
                            GameManager.Instance._saveManager._userSave.AddTowerInfo(transform.GetComponent<TowerInform>());
                            return;
                        }
                        // �ƴ�
                        else
                        {
                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(transform.GetComponent<ItemInform>());

                            transform.SetParent(_previousParent);
                            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;
                            return;
                        }
                    }
                    // ��������
                    else
                    {
                        // ������ ����Ʈ�� �߰�
                        transform.GetComponent<ItemInform>()._itemData._slotNumber = slot._slotNumber;

                        GameManager.Instance._saveManager._userSave.RefreshItemInfo(transform.GetComponent<ItemInform>());
                        return;
                    }
                }
            }

        }

    }

}
