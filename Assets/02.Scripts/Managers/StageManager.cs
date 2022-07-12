using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageInform
{
    public int _stageNumber;

    public List<int> _stageNumbers;
}

public class StageManager : MonoBehaviour
{
    public List<StageInform> _stageInform;
    public Transform _monsters;
    public GameObject monster;
    public void SetStage(int stageNumber)
    {
        foreach (MapInform map in GameManager.Instance._mapList)
        {
            map._isWay = false;
        }

        foreach (StageInform stage in _stageInform)
        {
            if (stage._stageNumber == stageNumber)
            {
                // 스테이지 설정 
                for (int i = 0; i < _stageInform[stageNumber - 1]._stageNumbers.Count; i++)
                {
                    foreach (MapInform map in GameManager.Instance._mapList)
                    {
                        if (map._mapNumber == _stageInform[stageNumber - 1]._stageNumbers[i])
                        {
                            map._isWay = true;
                        }
                    }
                }

                break;
            }
        }
    }
    private void Start()
    {
        SetStage(1);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = Instantiate(monster, _monsters);
            obj.transform.position = Vector3.zero;
            obj.GetComponent<MonsterMove>()._stageInform = _stageInform[0];
        }
    }
}
