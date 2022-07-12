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

    // ������ �̸�
    private string _userName;

    private int _hasMoney;

    // �� ����
    private int _currentHoney;
    private int _maxHoney;

    // �� ����
    private int _currentEgg;
    private int _maxEgg;

    private int _maxBeeCount;

    private List<TowerInform> _towerInformList;

    /// <summary>
    /// �ö� �ִ� ��ž�� ���� üũ�Ͽ� ���� �� �� �� �ִ���?
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
    }

    public UserSave() { }

    public UserSave(string userName, int hasMoney, int currentHoney, int maxHoney, int currentEgg, int maxEgg, int maxBee, List<TowerInform> towerInfos)
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
    }
}
