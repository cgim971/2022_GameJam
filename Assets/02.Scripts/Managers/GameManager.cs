using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    public SaveManager _saveManager { get; private set; }
    public StageManager _stageManager { get; private set; }
    public TowerManager _towerManager { get; private set; }
    public ItemManager _itemManager { get; private set; }


    public GameObject _map;
    public Transform _midPanel;

    public List<MapInform> _mapList;
    public List<SlotInform> _slotList;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        _saveManager = FindObjectOfType<SaveManager>();
        _stageManager = FindObjectOfType<StageManager>();
        _towerManager = FindObjectOfType<TowerManager>();
        _itemManager = FindObjectOfType<ItemManager>();

        for (int i = 0; i < 40; i++)
        {
            GameObject obj = Instantiate(_map, _midPanel);
            obj.GetComponent<MapInform>()._mapNumber = i;

            _mapList.Add(obj.GetComponent<MapInform>());
        }

        for(int i = 0; i< _slotList.Count; i++)
        {
            _slotList[i].GetComponent<SlotInform>()._slotNumber = i;
        }

    }

    private void Start()
    {

    }
}
