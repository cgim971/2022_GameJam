using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSave
{
    public string USER_NAME
    {
        get => _userName;
        set
        {
            _userName = value;
            GameManager.Instance._saveManager.SaveUserName(_userName);
        }
    }
    public int USER_HASMONEY
    {
        get => _hasMoney;
        set
        {
            _hasMoney = value;
            GameManager.Instance._saveManager.SaveHasMoney(_hasMoney);
        }
    }
    public int USER_HASROYALJELLY
    {
        get => _hasRoyalJelly;
        set
        {
            _hasRoyalJelly = value;
            GameManager.Instance._saveManager.SaveHasMoney(_hasRoyalJelly);
        }
    }
    public int USER_CURRENTHONEY
    {
        get => _currentHoney;
        set
        {
            _currentHoney = value;
            GameManager.Instance._saveManager.SaveCurrentHoney(_currentHoney);
        }
    }
    public int USER_MAXHONEY
    {
        get => _maxHoney;
        set
        {
            _maxHoney = value;
            GameManager.Instance._saveManager.SaveMaxHoney(_maxHoney);
        }
    }
    public int USER_CURRENTEGG
    {
        get => _currentEgg;
        set
        {
            _currentEgg = value;
            GameManager.Instance._saveManager.SaveCurrentEgg(_currentEgg);
        }
    }
    public int USER_MAXEGG
    {
        get => _maxEgg;
        set
        {
            _maxEgg = value;
            GameManager.Instance._saveManager.SaveMaxEgg(_maxEgg);
        }
    }
    public int USER_MAXBEECOUNT
    {
        get => _maxBeeCount;
        set
        {
            _maxBeeCount = value;
            GameManager.Instance._saveManager.SaveMaxBeeCount(_maxBeeCount);
        }
    }

    // 유저의 이름
    [SerializeField] private string _userName;

    [SerializeField] private int _hasMoney;
    [SerializeField] private int _hasRoyalJelly;

    // 꿀 정보
    [SerializeField] private int _currentHoney;
    [SerializeField] private int _maxHoney;

    // 알 정보
    [SerializeField] private int _currentEgg;
    [SerializeField] private int _maxEgg;

    [SerializeField] private int _maxBeeCount;


    [SerializeField] private List<ItemData> _towerInformList = new List<ItemData>();
    public void AddTowerInfo(TowerInform inform)
    {
        ItemData towerData = inform.towerData;
        _towerInformList.Add(towerData);
        CreateTower(towerData, inform.transform.parent.GetComponent<MapInform>()._mapNumber);

        GameManager.Instance._saveManager.SaveTowerInfos(_towerInformList);
    }
    public void RemoveTowerInfo(TowerInform inform)
    {
        ItemData towerData = inform.towerData;
        _towerInformList.Remove(towerData);

        GameManager.Instance._saveManager.SaveTowerInfos(_towerInformList);

        RemoveTower(towerData);
    }
    public void RefreshTowerInfo(ItemData inform)
    {
        //ItemInform item = new ItemInform();
        //item._itemName = "";
        //item._itemData = inform;

        //ItemInform item = new ItemInform
        //{
        //    _itemName = "",
        //    _itemData = inform
        //};

        GameManager.Instance._towerManager.RefreshTower(inform);
        CreateTower(inform, inform._slotNumber);
    }
    public Dictionary<int, GameObject> _towerDictionary = new Dictionary<int, GameObject>();
    public void CreateTower(ItemData inform, int index)
    {
        inform._slotNumber = index;
        int towerIndex = inform._slotNumber;
        GameObject obj = GameManager.Instance._towerManager.CreateTower(towerIndex);
        _towerDictionary.Add(towerIndex, obj);
    }
    public void RemoveTower(ItemData inform)
    {
        int towerIndex = inform._slotNumber;
        GameObject tower = null;
        _towerDictionary.TryGetValue(towerIndex, out tower);
        if (tower != null)
        {
            GameManager.Instance._towerManager.RemoveTower(tower);
            _towerDictionary.Remove(towerIndex);
        }
    }

    [SerializeField] private List<ItemData> _itemInformList = new List<ItemData>();
    public void AddItemInfo(ItemInform inform)
    {
        ItemData itemData = inform._itemData;
        _itemInformList.Add(itemData);

        CreateItem(inform);
        GameManager.Instance._saveManager.SaveItemInfos(_itemInformList);
    }
    public void RemoveItemInfo(ItemInform inform)
    {
        ItemData itemData = inform._itemData;
        _itemInformList.Remove(itemData);

        GameManager.Instance._saveManager.SaveItemInfos(_itemInformList);
    }
    public void RefreshItemInfo(ItemData inform)
    {
        ItemInform item = new ItemInform
        {
            _itemName = "",
            _itemData = inform,
        };

        CreateItem(item);
    }
    public void RefreshItemInfo(ItemInform inform)
    {
        _itemInformList.Add(inform._itemData);
        GameManager.Instance._saveManager.SaveItemInfos(_itemInformList);
    }
    public void CreateItem(ItemInform inform)
    {
        int itemIndex = inform._itemData._slotNumber;
        GameObject obj = GameManager.Instance._itemManager.CreateItem(itemIndex);
        obj.GetComponent<ItemInform>().SetItemInform(inform);

        GameManager.Instance._saveManager.SaveItemInfos(_itemInformList);
    }

    public void DeleteItem(GameObject obj)
    {
        _itemInformList.Remove(obj.GetComponent<ItemInform>()._itemData);
        MonoBehaviour.Destroy(obj);
        GameManager.Instance._saveManager.SaveItemInfos(_itemInformList);
    }


    [SerializeField] private List<int> _beeLvList = new List<int>();
    public void ChangeBeeInfo(int index, int value)
    {
        _beeLvList[index] = value;
        GameManager.Instance._saveManager.SaveBeeInfos(_beeLvList);
    }
    public List<int> USER_BEELVLIST
    {
        get => _beeLvList;
    }

    [SerializeField] private List<int> _shopItemLvList = new List<int>();

    public void ChangeShopItemInfo(int index, int value)
    {
        _shopItemLvList[index] = value;
        GameManager.Instance._saveManager.SaveShopItemInfos(_shopItemLvList);
    }

    public List<int> USER_SHOPITEMLVLIST
    {
        get => _shopItemLvList;
    }

    /// <summary>
    /// 올라가 있는 포탑의 수를 체크하여 벌을 더 할 수 있는지?
    /// </summary>
    /// <returns></returns>
    public bool IsCanBuildBee()
    {
        if (_towerInformList.Count < _maxBeeCount) return true;

        return false;
    }

    public void ResetData()
    {
        USER_NAME = "";
        USER_HASMONEY = 0;
        USER_CURRENTHONEY = 0;
        USER_MAXHONEY = 10;
        USER_CURRENTEGG = 0;
        USER_MAXEGG = 10;
        USER_MAXBEECOUNT = 5;

        PlayerPrefs.DeleteKey("TowerInfoJsonStr");
        PlayerPrefs.DeleteKey("ItemInfoJsonStr");
        PlayerPrefs.DeleteKey("BeeInfoJsonStr");
        PlayerPrefs.DeleteKey("ShopItemJsonStr");

        _towerInformList.Clear();
        GameManager.Instance._saveManager.SaveTowerInfos(_towerInformList);

        _itemInformList.Clear();
        GameManager.Instance._saveManager.SaveItemInfos(_itemInformList);

        _beeLvList.Clear();
        for (int i = 0; i < 10; i++)
        {
            _beeLvList.Add(0);
        }
        GameManager.Instance._saveManager.SaveBeeInfos(_beeLvList);

        _shopItemLvList.Clear();
        for (int i = 0; i < 14; i++)
        {
            _shopItemLvList.Add(0);
        }
        GameManager.Instance._saveManager.SaveShopItemInfos(_shopItemLvList);
    }

    public UserSave() { }

    public UserSave(string userName, int hasMoney, int currentHoney, int maxHoney, int currentEgg, int maxEgg, int maxBee, List<ItemData> towerInfos, List<ItemData> itemInfos, List<int> beeInfos, List<int> shopItemInfos)
    {
        _userName = userName;
        _hasMoney = hasMoney;
        _currentHoney = currentHoney;
        _maxHoney = maxHoney;
        _currentEgg = currentEgg;
        _maxEgg = maxEgg;
        _maxBeeCount = maxBee;

        _towerInformList = towerInfos;
        if (_towerInformList == null)
        {
            _towerInformList = new List<ItemData>();
        }
        else
        {
            // 타워 초기 생성
            for (int i = 0; i < _towerInformList.Count; i++)
                RefreshTowerInfo(_towerInformList[i]);
        }

        _itemInformList = itemInfos;
        if (_itemInformList == null)
        {
            _itemInformList = new List<ItemData>();
        }
        else
        {
            // 아이템 초기 생성
            for (int i = 0; i < _itemInformList.Count; i++)
                RefreshItemInfo(_itemInformList[i]);
        }


        _beeLvList = beeInfos;
        if (_beeLvList == null)
        {
            _beeLvList = new List<int>();
        }

        _shopItemLvList = shopItemInfos;
        if (_shopItemLvList == null)
        {
            _shopItemLvList = new List<int>();
        }
    }
}
