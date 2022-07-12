using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSave
{
    // 유저의 이름
    public string _userName;

    public long _hasMoney;

    // 꿀 정보
    public int _currentHoney;
    public int _maxHoney;

    // 알 정보
    public int _currentEgg;
    public int _maxEgg;

    public int _currentBee;
    public int _maxBee;

    public List<TowerInform> _towerInformList;

    public bool GetBeeCount()
    {
        if(_currentBee < _maxBee)
        {
            return true;
        }

        return false;
    }

    public UserSave() { }

    public UserSave(string userName, long hasMoney, int currentHoney, int maxHoney, int currentEgg, int maxEgg, int currentBee, int maxBee)
    {
        _userName = userName;
        _hasMoney = hasMoney;
        _currentHoney = currentHoney;
        _maxHoney = maxHoney;
        _currentEgg = currentEgg;
        _maxEgg = maxEgg;
        _currentBee = currentBee;
        _maxBee = maxBee;   
    }
}
