using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{

    public StageInform _stageInform;
    public float _speed = 1;
    public Vector2 _transformPos;
    public Vector2 _nextPos;
    public int _stagePosNumber;

    public bool _isStageNextPos;

    public void Start()
    {
        transform.position = _stageInform._startPos;

        _stagePosNumber = 0;
    }

    private void Update()
    {
        _transformPos = new Vector2(float.Parse((transform.position.x).ToString("F2")), float.Parse((transform.position.y).ToString("F1")));
        if (_transformPos == _stageInform._endPos)
        {
            // 몬스터가 포탑에 닿음

            Destroy(this.gameObject);
        }

        if (!_isStageNextPos)
        {
            if (_transformPos == _stageInform._nextPos[_stagePosNumber])
            {
                _stagePosNumber++;
                if (_stageInform._nextPos.Count <= _stagePosNumber)
                {
                    _isStageNextPos = true;
                    _nextPos = _stageInform._endPos - _transformPos;
                }
            }
            else
            {
                _nextPos = _stageInform._nextPos[_stagePosNumber] - _transformPos;
            }

            _nextPos.Normalize();
        }

        transform.Translate(_nextPos * Time.deltaTime * _speed, Space.World);
    }


}
