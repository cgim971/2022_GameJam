using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StageInform
{
    public int _stageNumber;

    public Vector2 _startPos;
    public Vector2 _endPos;

    public List<Vector2> _nextPos;
}

public class StageManager : MonoBehaviour
{
    public List<StageInform> _stageInform;

    public void SetStage(int stageNumber)
    {
        foreach (StageInform stage in _stageInform)
        {
            if (stage._stageNumber == stageNumber)
            {
                // 스테이지 설정 
                break;
            }
        }
    }
    public GameObject monster;
    private void Start()
    {
        SetStage(1);
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = Instantiate(monster, null);
            obj.GetComponent<MonsterMove>()._stageInform = _stageInform[0];
        }
    }
}
