using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public List<SlotInform> _slotList;
    public GameObject _item;

    private void Awake()
    {
        _slotList = GameManager.Instance._slotList;
    }

    public GameObject CreateItem(int index)
    {
        GameObject newItem = Instantiate(_item, _slotList[index].transform);
        newItem.GetComponent<ItemInform>()._itemData._slotNumber = index;
        return newItem;
    }
    public void RemoveItem(GameObject item)
    {
        Destroy(item);
    }
}
