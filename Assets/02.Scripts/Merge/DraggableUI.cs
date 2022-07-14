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

        // 자신 부모가 뭐냐에 따라 리스트 값 변경
        if (_previousParent.GetComponent<MapInform>() != null)
        {
            GameManager.Instance._saveManager._userSave.RemoveTowerInfo(transform.GetComponent<TowerInform>());
        }
        else
        {
            GameManager.Instance._saveManager._userSave.RemoveItemInfo(transform.GetComponent<ItemInform>());
        }

        // 부모가 캔버스면
        if (transform.parent == _canvas)
        {
            transform.SetParent(_previousParent);
            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;

            // 원래 위치로
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
        // 캔버스가 아니면
        else
        {
            MapInform map = eventData.pointerDrag.transform.GetComponentInParent<MapInform>();
            SlotInform slot = eventData.pointerDrag.transform.GetComponentInParent<SlotInform>();

            // 어디서 드래그를 했는지
            // 맵
            if (_previousParent.GetComponent<MapInform>() != null)
            {
                // 드래그 된 곳이 무엇인지
                //옮겨진 곳이 맵인지
                Transform parent = eventData.pointerDrag.transform.parent;
                int count = parent.childCount;

                // 자식이 여러개임?
                if (count > 1)
                {
                    Transform item_1 = parent.GetChild(0);
                    Transform item_2 = parent.GetChild(1);

                    if (map != null)
                    {
                        // 포탑 위치를 바꿔라

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
                        // 포탑인가용?
                        if (item_1.GetComponent<ItemInform>()._itemData._itemType == ItemType.BEE)
                        {
                            GameManager.Instance._saveManager._userSave.RemoveItemInfo(item_1.GetComponent<ItemInform>());

                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(item_2.GetComponent<ItemInform>());
                            GameManager.Instance._saveManager._userSave.AddTowerInfo(item_2.GetComponent<TowerInform>());

                            item_1.GetComponent<RectTransform>().position = _previousParent.GetComponentInParent<RectTransform>().position;
                            item_1.SetParent(_previousParent);
                            return;
                        }
                        // 아닌데용
                        else
                        {
                            transform.SetParent(_previousParent);
                            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;
                            return;
                        }
                    }
                }
                // 아닌데용
                else
                {
                    if (map != null)
                    {
                        transform.GetComponent<TowerInform>().towerData._slotNumber = map._mapNumber;

                        GameManager.Instance._saveManager._userSave.AddTowerInfo(transform.GetComponent<TowerInform>());
                        return;
                    }
                    // 슬롯인지
                    else
                    {
                        // 아이템 리스트에 추가
                        transform.GetComponent<ItemInform>()._itemData._slotNumber = slot._slotNumber;

                        GameManager.Instance._saveManager._userSave.RefreshItemInfo(transform.GetComponent<ItemInform>());
                        return;
                    }
                }
            }
            // 슬롯
            else
            {
                Transform parent = eventData.pointerDrag.transform.parent;
                int count = parent.childCount;

                // 자식이 여러개임?
                if (count > 1)
                {
                    Transform item_1 = parent.GetChild(0);
                    Transform item_2 = parent.GetChild(1);

                    // 맵임 
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
                // 아닌데용
                else
                {
                    if (map != null)
                    {
                        // 타워인지
                        if (transform.GetComponent<ItemInform>()._itemData._itemType == ItemType.BEE)
                        {
                            transform.GetComponent<TowerInform>().towerData._slotNumber = map._mapNumber;
                            GameManager.Instance._saveManager._userSave.AddTowerInfo(transform.GetComponent<TowerInform>());
                            return;
                        }
                        // 아님
                        else
                        {
                            GameManager.Instance._saveManager._userSave.RefreshItemInfo(transform.GetComponent<ItemInform>());

                            transform.SetParent(_previousParent);
                            _rect.position = _previousParent.GetComponentInParent<RectTransform>().position;
                            return;
                        }
                    }
                    // 슬롯인지
                    else
                    {
                        // 아이템 리스트에 추가
                        transform.GetComponent<ItemInform>()._itemData._slotNumber = slot._slotNumber;

                        GameManager.Instance._saveManager._userSave.RefreshItemInfo(transform.GetComponent<ItemInform>());
                        return;
                    }
                }
            }

        }

    }

}
