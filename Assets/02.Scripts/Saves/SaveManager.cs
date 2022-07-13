using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    public UserSave _userSave;

    private bool _isLoad = false;

    private void Start()
    {
        _isLoad = false;
        

    }

    public void Update()
    {
        if (_isLoad == false)
        {
            StartCoroutine(LoadFromJson());
            _isLoad = true;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            ResetSaveFile();
        }
    }

    public void ResetSaveFile()
    {
        _userSave.ResetData();

        LoadFromJson();
    }

    private IEnumerator LoadFromJson()
    {
        yield return new WaitForEndOfFrame();

        string name = PlayerPrefs.GetString("UserName", "");
        int hasMoney = PlayerPrefs.GetInt("HasMoney", 0);
        int currentHoney = PlayerPrefs.GetInt("CurrentHoney", 0);
        int maxHoney = PlayerPrefs.GetInt("MaxHoney", 0);
        int currentEgg = PlayerPrefs.GetInt("CurrentEgg", 0);
        int maxEgg = PlayerPrefs.GetInt("MaxEgg", 0);
        int maxBeeCount = PlayerPrefs.GetInt("MaxBeeCount", 0);

        // PlayerPrefs.DeleteKey("TowerInfoJsonStr");
        string towerInfoJsonStr = PlayerPrefs.GetString("TowerInfoJsonStr", "");
        var towerInfos = JsonConvert.DeserializeObject<List<TowerData>>(towerInfoJsonStr);

        // PlayerPrefs.DeleteKey("BeeInfoJsonStr");
        string beeInfoJsonStr = PlayerPrefs.GetString("BeeInfoJsonStr", "");
        var beeInfos = JsonConvert.DeserializeObject<List<int>>(beeInfoJsonStr);

        // PlayerPrefs.DeleteKey("ShopItemJsonStr");
        string shopItemInfoJsonStr = PlayerPrefs.GetString("ShopItemJsonStr", "");
        var shopItemInfos = JsonConvert.DeserializeObject<List<int>>(shopItemInfoJsonStr);

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
    public void SaveTowerInfos(List<TowerData> value)
    {
        var jsonStr = JsonConvert.SerializeObject(value);
        PlayerPrefs.SetString("TowerInfoJsonStr", jsonStr);
    }

    public void SaveBeeInfos(List<int> value)
    {
        var jsonStr = JsonConvert.SerializeObject(value);
        PlayerPrefs.SetString("BeeInfoJsonStr", jsonStr);
    }
    public void SaveShopItemInfos(List<int> value)
    {
        var jsonStr = JsonConvert.SerializeObject(value);
        PlayerPrefs.SetString("ShopItemJsonStr", jsonStr);
    }
}
