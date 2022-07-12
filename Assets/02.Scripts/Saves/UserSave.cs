using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSave
{
    public string USER_NAME
    {
        get => _userName;
        set => GameManager.Instance._saveManager.SaveUserName(value);
    }
    public int USER_HASMONEY
    {
        get => _hasMoney;
        set => GameManager.Instance._saveManager.SaveHasMoney(value);
    }
    public int USER_CURRENTHONEY
    {
        get => _currentHoney;
        set => GameManager.Instance._saveManager.SaveCurrentHoney(value);
    }
    public int USER_MAXHONEY
    {
        get => _maxHoney;
        set => GameManager.Instance._saveManager.SaveMaxHoney(value);
    }
    public int USER_CURRENTEGG
    {
        get => _currentEgg;
        set => GameManager.Instance._saveManager.SaveCurrentEgg(value);
    }
    public int USER_MAXEGG
    {
        get => _maxEgg;
        set => GameManager.Instance._saveManager.SaveMaxEgg(value);
    }
    public int USER_MAXBEECOUNT
    {
        get => _maxBeeCount;
        set => GameManager.Instance._saveManager.SaveMaxBeeCount(value);
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

    [SerializeField] private List<TowerInform> _towerInformList = new List<TowerInform>();
    public void AddTowerInfo(TowerInform inform)
    {
        _towerInformList.Add(inform);
        GameManager.Instance._saveManager.SaveTowerInfos(_towerInformList);
    }

    public void RemoveTowerInfo(TowerInform inform)
    {
        _towerInformList.Remove(inform);
        GameManager.Instance._saveManager.SaveTowerInfos(_towerInformList);
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
        if (_towerInformList.Count < _maxBeeCount)
        {
            return true;
        }

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

    public UserSave(string userName, int hasMoney, int currentHoney, int maxHoney, int currentEgg, int maxEgg, int maxBee, List<TowerInform> towerInfos, List<int> beeInfos, List<int> shopItemInfos)
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
            _towerInformList = new List<TowerInform>();
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
