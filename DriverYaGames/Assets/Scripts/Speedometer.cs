using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speedometrText;

    private List<ObjectMoving> _movingObjects= new List<ObjectMoving>();
    private int _currentSpeed;
    private GameRules _gameRules;
    private float _periodGrowthRate;

    private void Start()
    {
        _gameRules = FindObjectOfType<GameRules>();
        _currentSpeed = _gameRules.StartSpeed;
        ObjectMoving[] objects = FindObjectsOfType<ObjectMoving>();
        _movingObjects.AddRange(objects); 
        InvokeRepeating(nameof(IncreaseSpeed), 1f, 1f);
        InvokeRepeating(nameof(IncreaseCarsSpawnRate), 1f, 1f);

        float stagesCount = ((_gameRules.MaxSpeed - _gameRules.StartSpeed) / (_gameRules.RateOfSpeedGrowth));
        _periodGrowthRate = (_gameRules.StartCarSpawnPeriod - _gameRules.MinCarSpawnPeriod) / stagesCount;
    }


    private void Update()
    {
        ChangeSpeed();
    }

    private void IncreaseSpeed()
    {
        if(_currentSpeed < _gameRules.MaxSpeed)
        {
            _currentSpeed += _gameRules.RateOfSpeedGrowth;
        }
    }

    private void IncreaseCarsSpawnRate()
    {
        _gameRules.SetCarSpawnPeriod(_periodGrowthRate);
    }

    private void ChangeSpeed()
    {
        foreach(ObjectMoving obj in _movingObjects)
        {
            obj.CurrentSpeed = obj.StartSpeed * (_currentSpeed / _gameRules.StartSpeed);
        }
        _speedometrText.text = _currentSpeed.ToString();
    }
}
