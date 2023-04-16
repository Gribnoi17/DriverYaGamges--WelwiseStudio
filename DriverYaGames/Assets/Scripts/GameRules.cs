using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour
{
    [SerializeField] private int _startSpeed = 50;
    public int StartSpeed { get { return _startSpeed; } private set { _startSpeed = value; } }

    public int MaxSpeed { get { return _maxSpeed; } private set { _maxSpeed = value; } }

    [SerializeField] private int rateOfSpeedGrowth = 1;
    public int RateOfSpeedGrowth { get { return rateOfSpeedGrowth; } private set { rateOfSpeedGrowth = value; } }

    [SerializeField] private float _startCarSpawnPeriod = 2;
    public float StartCarSpawnPeriod { get { return _startCarSpawnPeriod; } private set { _startCarSpawnPeriod = value; } }

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

    [Header("CarsCharacteristics")]
    [SerializeField] private int _maxSpeed_PoliceCar;
    [SerializeField] private int _maxSpeed_WhitePoliceCar;
    [SerializeField] private int _maxSpeed_SportCar;
    [SerializeField] private int _maxSpeed_SciFiCar;

    private int _maxSpeed = 240;
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
        carGenerator = FindObjectOfType<Generator>();
        StartSpeed = _startSpeed;
        Inizializition();
        RateOfSpeedGrowth = rateOfSpeedGrowth;
    }


    private void Inizializition()
    {
        carGenerator.SpawnPeriod = _startCarSpawnPeriod;

        if (PlayerPrefs.GetString("Car") == "WhitePoliceCar" && PlayerPrefs.GetString("WhitePoliceCar") == "Unlocked")
        {
            _maxSpeed = _maxSpeed_WhitePoliceCar;
        }
        else if (PlayerPrefs.GetString("Car") == "SportCar" && PlayerPrefs.GetString("SportCar") == "Unlocked")
        {
            _maxSpeed = _maxSpeed_SportCar;
        }
        else if (PlayerPrefs.GetString("Car") == "SciFiCar" && PlayerPrefs.GetString("SciFiCar") == "Unlocked")
        {
            _maxSpeed = _maxSpeed_SciFiCar;
        }
        else
        {
            _maxSpeed = _maxSpeed_PoliceCar;
        }

        MaxSpeed = _maxSpeed;
    }

    public void SetCarSpawnPeriod(float val)
    {
        if (carGenerator.SpawnPeriod > _minCarSpawnPeriod)
            carGenerator.SpawnPeriod -= val;
        else
            carGenerator.SpawnPeriod = _minCarSpawnPeriod;
    }

}
