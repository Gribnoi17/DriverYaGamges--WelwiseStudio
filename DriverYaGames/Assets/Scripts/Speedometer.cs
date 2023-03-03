using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Speedometer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _speedometrText;

    private List<ObjectMoving> _movingObjects= new List<ObjectMoving>();
    private int _currentSpeed;
    private GameRules gameRules;

    private void Start()
    {
        gameRules = FindObjectOfType<GameRules>();
        _currentSpeed = gameRules.StartSpeed;
        ObjectMoving[] objects = FindObjectsOfType<ObjectMoving>();
        _movingObjects.AddRange(objects);
        InvokeRepeating(nameof(IncreaseSpeed), 1f, 1f);
    }


    private void Update()
    {
        ChangeSpeed();
    }

    private void IncreaseSpeed()
    {
        if(_currentSpeed < gameRules.MaxSpeed)
            _currentSpeed += gameRules.RateOfSpeedGrowth;
    }


    private void ChangeSpeed()
    {
        foreach(ObjectMoving obj in _movingObjects)
        {
            obj.CurrentSpeed = obj.StartSpeed * (_currentSpeed / gameRules.StartSpeed);
        }
        _speedometrText.text = _currentSpeed.ToString();
    }

    public int GetCurSpeed()
    {
        return _currentSpeed;
    }
}
