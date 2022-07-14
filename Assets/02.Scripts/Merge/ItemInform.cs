using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInform : MonoBehaviour
{
    public string _itemName;
    public ItemData _itemData;
    public Sprite _itemSprite;

    public void SetItemInform(ItemInform inform)
    {
        _itemName = inform._itemName;
        _itemData = inform._itemData;
        _itemSprite = inform._itemSprite;
    }
}

[System.Serializable]
public class ItemData
{
    public ItemType _itemType;
    public int _itemGrade;

    public int _slotNumber;
}
