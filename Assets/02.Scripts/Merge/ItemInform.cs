using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInform : MonoBehaviour
{
    public string _itemName;
    public ItemType _itemType;
    public int _itemGrade;

    public Image _itemImage;



    public void SetItemInform(ItemSave itemSave)
    {
        _itemName = itemSave._itemName;
        _itemType = itemSave._itemType;
        _itemGrade = itemSave._itemGrade;
        _itemImage.sprite = itemSave._itemSprite;
    }
}
