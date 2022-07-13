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
        if (Input.GetKeyDown(KeyCode.A))
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
        string name = PlayerPrefs.GetString("UserName", "");
        int hasMoney = PlayerPrefs.GetInt("HasMoney", 0);
        int currentHoney = PlayerPrefs.GetInt("CurrentHoney", 0);
        int maxHoney = PlayerPrefs.GetInt("MaxHoney", 0);
        int currentEgg = PlayerPrefs.GetInt("CurrentEgg", 0);
        int maxEgg = PlayerPrefs.GetInt("MaxEgg", 0);
        int maxBeeCount = PlayerPrefs.GetInt("MaxBeeCount", 0);

        string towerInfoJsonStr = PlayerPrefs.GetString("TowerInfoJsonStr", "");
        var towerInfos = JsonUtility.FromJson<List<TowerInform>>(towerInfoJsonStr);

        string beeInfoJsonStr = PlayerPrefs.GetString("BeeInfoJsonStr", "");
        var beeInfos = JsonUtility.FromJson<List<int>>(beeInfoJsonStr);

        string shopItemInfoJsonStr = PlayerPrefs.GetString("ShopItemJsonStr", "");
        var shopItemInfos = JsonUtility.FromJson<List<int>>(shopItemInfoJsonStr);

        string getBeeInfoJsonStr = PlayerPrefs.GetString("GetBeeJsonStr", "");
        var getBeeInfos = JsonUtility.FromJson<List<bool>>(getBeeInfoJsonStr);

        _userSave = new UserSave(name, hasMoney, currentHoney, maxHoney, currentEgg, maxEgg, maxBeeCount, towerInfos, beeInfos, shopItemInfos, getBeeInfos);
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
        // 저장이 제대로 안 됨 {}으로 되고 있음 그래서 포탑의 정보 등이 안 됨
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
    public void SaveGetBeeInfos(List<bool> value)
    {
        var jsonStr = JsonUtility.ToJson(value, false);
        PlayerPrefs.SetString("GetBeeJsonStr", jsonStr);
    }

}
