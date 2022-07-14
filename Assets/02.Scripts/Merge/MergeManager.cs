using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeManager : MonoBehaviour
{
    public Button _eggBtn;
    public ItemSave _eggInform;

    private void Start()
    {
        _eggBtn.GetComponent<Button>().onClick.AddListener(() => CreateEgg());
    }

    public void CreateEgg()
    {
        // 알이 있는지?
        // if (GameManager.Instance._saveManager._userSave.USER_CURRENTEGG <= 0) return;

        int index = -1;
        for (int i = 0; i < GameManager.Instance._slotList.Count; i++)
        {
            if (GameManager.Instance._slotList[i].transform.childCount == 0)
            {
                index = i;
                break;
            }
        }

        if (index == -1) return;

        ItemInform itemInform = new ItemInform
        {
            _itemName = _eggInform._itemName,
            _itemData = new ItemData
            {
                _itemType = _eggInform._itemType,
                _itemGrade = _eggInform._itemGrade,
                _slotNumber = index
            },
            _itemSprite = _eggInform._itemSprite
        };

        GameManager.Instance._saveManager._userSave.AddItemInfo(itemInform);
    }
}
