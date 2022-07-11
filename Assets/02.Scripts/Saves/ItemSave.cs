using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSave", menuName = "Assets/ScriptableObject/Items")]
public class ItemSave : ScriptableObject
{
    public string _itemName;
    public ItemType _itemType;
    public int _itemGrade;
}

public enum ItemType
{
    NONE,
    EGG,
    HONEY
}