using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speedometrText;

    private List<ObjectMoving> _movingObjects= new List<ObjectMoving>();
    private float _currentSpeed;
    public float CurrentSpeed { get { return _currentSpeed; } set { _currentSpeed += value; } }
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

    public float GetCurSpeed()
    {
        return _currentSpeed;
    }

    public IEnumerator SpeedBoosterController()
    {
        _currentSpeed += 15;
        if (PlayerPrefs.GetInt("MaxSpeed") < 260 && _currentSpeed >= 260)
            PlayerPrefs.SetInt("MaxSpeed", 260);
        yield return new WaitForSeconds(2);
        _currentSpeed -= 15;
        StopCoroutine(SpeedBoosterController());
    }

    public IEnumerator SpeedUnBusterOrShielPickUp()
    {
        _currentSpeed -= 10;
        yield return new WaitForSeconds(3);
        _currentSpeed += 10;
        StopCoroutine(SpeedUnBusterOrShielPickUp());
    }
}
