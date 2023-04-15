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

    [SerializeField] private float _minCarSpawnPeriod = 0.5f;
    public float MinCarSpawnPeriod { get { return _minCarSpawnPeriod; } private set { _minCarSpawnPeriod = value; } }

    private string regime;
    public string Regime { get { return regime; } private set { regime = value; } }

    private string difficult;
    public string Difficult { get { return difficult; } private set { difficult = value; } }

    public Dictionary<int, string> regimeRace = new Dictionary<int, string>()
    {
        {0, "FreeRace" },
        {1, "RaceForTime"}
    };

    public Dictionary<int, string> difficulty = new Dictionary<int, string>()
    {
        {0, "Eazy" },
        {1, "Normal"},
        {2, "Hard"}
    };

    private Generator carGenerator;

    private void Start()
    {
        difficult = "Normal";

        if(PlayerPrefs.GetInt("RegimeRace") == 0)
        {
            Regime = regimeRace[0];
        } else if(PlayerPrefs.GetInt("RegimeRace") == 1)
        {
            Regime = regimeRace[1];
            Difficult = difficulty[PlayerPrefs.GetInt("Difficult")];
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
