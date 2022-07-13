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

    // 꿀 정보
    [SerializeField] private int _currentHoney;
    [SerializeField] private int _maxHoney;

    // 알 정보
    [SerializeField] private int _currentEgg;
    [SerializeField] private int _maxEgg;

    [SerializeField] private int _maxBeeCount;


    [SerializeField] private List<TowerData> _towerInformList = new List<TowerData>();
    public void AddTowerInfo(TowerInform inform)
    {
        TowerData towerData = inform.towerData;
        _towerInformList.Add(towerData);
        Debug.Log(towerData);
        Debug.Log(inform.transform.parent.GetComponent<MapInform>()._mapNumber);
        CreateTower(towerData, inform.transform.parent.GetComponent<MapInform>()._mapNumber);

        GameManager.Instance._saveManager.SaveTowerInfos(_towerInformList);
    }

    public void RemoveTowerInfo(TowerInform inform)
    {
        TowerData towerData = inform.towerData;
        _towerInformList.Remove(towerData);

        GameManager.Instance._saveManager.SaveTowerInfos(_towerInformList);

        // 타워 삭제 
        RemoveTower(towerData);
    }
    public Dictionary<int, GameObject> _towerDictionary = new Dictionary<int, GameObject>();
    public void CreateTower(TowerData inform, int index)
    {
        inform._slotNumber = index;
        int towerIndex = inform._slotNumber;
        GameObject obj = GameManager.Instance._towerManager.CreateTower(towerIndex);
        _towerDictionary.Add(towerIndex, obj);
    }
    public void CreateTower(TowerData inform)
    {
        int towerIndex = inform._slotNumber;
        GameObject obj = GameManager.Instance._towerManager.CreateTower(towerIndex);
        _towerDictionary.Add(towerIndex, obj);
    }
    public void RemoveTower(TowerData inform)
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


    [SerializeField] private List<int> _beeLvList = new List<int>();
    public void ChangeBeeInfo(int index, int value)
    {
        _beeLvList[index] = value;
        GameManager.Instance._saveManager.SaveBeeInfos(_beeLvList);
    }

    [SerializeField] private List<int> _shopItemLvList = new List<int>();

    public void ChangeShopItemInfo(int index, int value)
    {
        _shopItemLvList[index] = value;
        GameManager.Instance._saveManager.SaveShopItemInfos(_shopItemLvList);
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

        _towerInformList.Clear();

        for (int i = 0; i < _beeLvList.Count; i++)
        {
            _beeLvList[i] = 0;
        }

        for (int i = 0; i < _shopItemLvList.Count; i++)
        {
            _shopItemLvList[i] = 0;
        }
    }

    public UserSave() { }

    public UserSave(string userName, int hasMoney, int currentHoney, int maxHoney, int currentEgg, int maxEgg, int maxBee, List<TowerData> towerInfos, List<int> beeInfos, List<int> shopItemInfos)
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
            _towerInformList = new List<TowerData>();
        }
        else
        {
            // 타워 초기 생성
            for (int i = 0; i < _towerInformList.Count; i++)
                CreateTower(_towerInformList[i]);
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
