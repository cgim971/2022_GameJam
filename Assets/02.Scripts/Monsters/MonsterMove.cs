using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public StageInform _stageInform;

    public List<MapInform> _mapList;
    int _index = 0;
    Camera _mainCam;
    public float _speed;

    Vector2 _targetPosition;

    Vector2 _distance = Vector2.zero;

    public void Start()
    {
        _mapList = GameManager.Instance._mapList;

        _mainCam = Camera.main;
        Vector2 pos = _mainCam.ScreenToWorldPoint(_mapList[_stageInform._stageNumbers[_index++]].transform.position);

        transform.position = pos;

        _targetPosition = _mainCam.ScreenToWorldPoint(_mapList[_stageInform._stageNumbers[_index]].transform.position);
        _distance = _targetPosition - (Vector2)transform.position;
    }

    private void Update()
    {
        _distance = _targetPosition - (Vector2)transform.position;

        if (_distance.magnitude < 0.1f)
        {
            if (++_index >= _stageInform._stageNumbers.Count)
            {
                // 종료 조건
                Destroy(this.gameObject);
            }
            else
            {
                _targetPosition = _mainCam.ScreenToWorldPoint(_mapList[_stageInform._stageNumbers[_index]].transform.position);
            }
        }
        else
        {
            Vector2 normalized = _distance.normalized;
            transform.Translate(normalized * Time.deltaTime * _speed);
        }
    }
}
