using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveManager : MonoBehaviour
{
    public UserSave _userSave;

    private void Awake()
    {
        LoadFronJson();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            ResetSaveFile();
        }
    }

    public void ResetSaveFile()
    {
        _userSave.ResetData();

        LoadFronJson();
    }

    private void LoadFronJson()
    {
        var name = PlayerPrefs.GetString("UserName", "");
        var hasMoney = PlayerPrefs.GetInt("HasMoney", 0);
        var currentHoney = PlayerPrefs.GetInt("CurrentHoney", 0);
        var maxHoney = PlayerPrefs.GetInt("MaxHoney", 0);
        var currentEgg = PlayerPrefs.GetInt("CurrentEgg", 0);
        var maxEgg = PlayerPrefs.GetInt("MaxEgg", 0);
        var maxBeeCount = PlayerPrefs.GetInt("MaxBeeCount", 0);

        var towerInfoJsonStr = PlayerPrefs.GetString("TowerInfoJsonStr", "");
        var towerInfos = JsonUtility.FromJson<List<TowerInform>>(towerInfoJsonStr);

        var beeInfoJsonStr = PlayerPrefs.GetString("BeeInfoJsonStr", "");
        var beeInfos = JsonUtility.FromJson<List<int>>(beeInfoJsonStr);

        var shopItemInfoJsonStr = PlayerPrefs.GetString("ShopItemJsonStr", "");
        var shopItemInfos = JsonUtility.FromJson<List<int>>(shopItemInfoJsonStr);

        _userSave = new UserSave(name, hasMoney, currentHoney, maxHoney, currentEgg, maxEgg, maxBeeCount, towerInfos, beeInfos, shopItemInfos);
    }

    public void SaveUserName(string value)
    {
        PlayerPrefs.SetString("UserName", value);
    }
    public void SaveHasMoney(int value)
    {
        PlayerPrefs.SetInt("HasMoney", value);
    }
    public void SaveCurrentHoney(int value)
    {
        PlayerPrefs.SetInt("CurrentHoney", value);
    }
    public void SaveMaxHoney(int value)
    {
        PlayerPrefs.SetInt("MaxHoney", value);
    }
    public void SaveCurrentEgg(int value)
    {
        PlayerPrefs.SetInt("CurrentEgg", value);
    }
    public void SaveMaxEgg(int value)
    {
        PlayerPrefs.SetInt("MaxEgg", value);
    }
    public void SaveMaxBeeCount(int value)
    {
        PlayerPrefs.SetInt("MaxBeeCount", value);
    }
    public void SaveTowerInfos(List<TowerInform> value)
    {
        var jsonStr = JsonUtility.ToJson(value, false);
        PlayerPrefs.SetString("TowerInfoJsonStr", jsonStr);
    }
    public void SaveBeeInfos(List<int> value)
    {
        var jsonStr = JsonUtility.ToJson(value, false);
        PlayerPrefs.SetString("BeeInfoJsonStr", jsonStr);
    }
    public void SaveShopItemInfos(List<int> value)
    {
        var jsonStr = JsonUtility.ToJson(value, false);
        PlayerPrefs.SetString("ShopItemJsonStr", jsonStr);
    }
}
