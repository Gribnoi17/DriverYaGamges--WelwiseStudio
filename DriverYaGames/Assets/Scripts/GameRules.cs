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

    private string regime;
    public string Regime { get { return regime; } private set { regime = value; } }

    public Dictionary<int, string> regimeRace = new Dictionary<int, string>()
    {
        {0, "FreeRace" },
        {1, "RaceForTime"}
    };

    private Generator carGenerator;

    private void Start()
    {
        //0 и 1 заменятся на параметр из Player Prefs
        if(0 == 0)
        {
            regime = regimeRace[0];
        } else if(1 == 1)
        {
            regime = regimeRace[1];
        }
    }

    private void Awake()
    {
        StartSpeed = startSpeed;
        MaxSpeed = maxSpeed;
        RateOfSpeedGrowth = rateOfSpeedGrowth;
        carGenerator = FindObjectOfType<Generator>();
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
