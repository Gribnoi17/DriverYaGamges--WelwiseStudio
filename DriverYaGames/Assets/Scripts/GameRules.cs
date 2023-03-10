using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    [SerializeField] private int startSpeed = 50;
    public int StartSpeed { get { return startSpeed; } private set { startSpeed = value; } }

    [SerializeField] private int maxSpeed = 240;
    public int MaxSpeed { get { return maxSpeed; } private set { maxSpeed = value; } }

    [SerializeField] private int rateOfSpeedGrowth = 1;
    public int RateOfSpeedGrowth { get { return rateOfSpeedGrowth; } private set { rateOfSpeedGrowth = value; } }

    [SerializeField] private int _startCarSpawnPeriod = 2;
    public int StartCarSpawnPeriod { get { return _startCarSpawnPeriod; } private set { _startCarSpawnPeriod = value; } }

    [SerializeField] private int _minCarSpawnPeriod = 1;
    public int MinCarSpawnPeriod { get { return _minCarSpawnPeriod; } private set { _minCarSpawnPeriod = value; } }


    private CarGenerator carGenerator;

    private void Awake()
    {
        StartSpeed = startSpeed;
        MaxSpeed = maxSpeed;
        RateOfSpeedGrowth = rateOfSpeedGrowth;
        carGenerator = FindObjectOfType<CarGenerator>();
    }


    private void Inizializition()
    {
        carGenerator.SpawnPeriod = _startCarSpawnPeriod;
    }

    public void SetCarSpawnPeriod(float val)
    {
        if (carGenerator.SpawnPeriod > _minCarSpawnPeriod)
            carGenerator.SpawnPeriod -= val;
        else
            carGenerator.SpawnPeriod = _minCarSpawnPeriod;
    }

}
