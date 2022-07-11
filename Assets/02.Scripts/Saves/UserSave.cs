using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserSave
{
    // ������ �̸�
    public string _userName;

    public long _hasMoney;

    // �� ����
    public int _currentHoney;
    public int _maxHoney;

    // �� ����
    public int _currentEgg;
    public int _maxEgg;

    public UserSave() { }

    public UserSave(string userName, long hasMoney, int currentHoney, int maxHoney, int currentEgg, int maxEgg)
    {
        _userName = userName;
        _hasMoney = hasMoney;
        _currentHoney = currentHoney;
        _maxHoney = maxHoney;
        _currentEgg = currentEgg;
        _maxEgg = maxEgg;
    }
}
