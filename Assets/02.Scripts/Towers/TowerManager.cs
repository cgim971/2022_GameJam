using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TowerManager : MonoBehaviour
{
    public GameObject _tower;
    public Transform _parent;
    public Camera _mainCam;

    public List<MapInform> _mapList;

    private void Awake()
    {
        _mainCam = Camera.main;

        _mapList = GameManager.Instance._mapList;
    }

    public GameObject CreateTower(int index)
    {
        GameObject newTower = Instantiate(_tower, _parent);
        Vector3 towerPos = _mainCam.ScreenToWorldPoint(_mapList[index].transform.position);
        towerPos.z = 0;
        newTower.transform.position = towerPos;
        newTower.GetComponent<TowerInform>().towerData._slotNumber = index;
        return newTower;
    }

    public void RefreshTowerPos(GameObject tower, int index)
    {
        Vector3 towerPos = _mainCam.ScreenToWorldPoint(_mapList[index].transform.position);
        towerPos.z = 0;

        tower.transform.position = towerPos;
    }

    public void RemoveTower(GameObject tower)
    {
        Destroy(tower);
    }
}
