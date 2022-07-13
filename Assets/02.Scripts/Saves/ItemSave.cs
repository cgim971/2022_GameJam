using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSave", menuName = "Assets/ScriptableObject/Items")]
public class ItemSave : ScriptableObject
{
    public string _itemName;
    public ItemType _itemType;
    public int _itemGrade;
    public Sprite _itemSprite;

    public BeeInfo _beeInfo;
}

[System.Serializable]
public class BeeInfo
{
    public int _damage;
    public float _attackSpeed;
    public float _critical;
    public int _honeyAmount;
    public float _range;
}

public enum ItemType
{
    NONE,
    EGG,
    HONEY,
    BEE
}
