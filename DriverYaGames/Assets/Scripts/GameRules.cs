using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRules : MonoBehaviour
{
    [Header("Speed")]
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

    [Header("Cars Characteristics")]
    [SerializeField] private int _maxSpeed_PoliceCar;
    [SerializeField] private int _maxSpeed_WhitePoliceCar;
    [SerializeField] private int _maxSpeed_SportCar;
    [SerializeField] private int _maxSpeed_SciFiCar;

    [Header("Economics and Gameplay")]
    [Tooltip("Обратный коэфф. скорости набора километража, чем больше, тем медленнее")] [SerializeField] private float _distSpeed = 30;
    public float DistSpeed { get { return _distSpeed; } }

    [Tooltip("Каждый раз, когда игрок проходит n-ное число километров, он получает награду")] [SerializeField] private float _rewardedKilometers = 15;
    public float RewardedKilometers { get { return _rewardedKilometers; } }

    [Tooltip("Вознаграждение за n-ное кол-во километров")] [SerializeField] private int _moneyForDistance = 20;
    public int MoneyForDistance { get { return _moneyForDistance; } }

   [SerializeField] private float _shieldActionTime = 5;

    private int _maxSpeed = 240;
    private Generator _carGenerator;
    private Odometer _odometr;
    private Money _money;
    private Shield _shield;
    private PoliceCar _policeCar;

    private void Start()
    {

        if(PlayerPrefs.GetInt("RegimeRace") == 0)
        {
            Regime = regimeRace[0];
        } 
        else if(PlayerPrefs.GetInt("RegimeRace") == 1)
        {
            Regime = regimeRace[1];
            if (PlayerPrefs.HasKey("NRDif") || PlayerPrefs.HasKey("GSDif") || PlayerPrefs.HasKey("TZDif"))
            {
                Difficult = difficulty[GetDiff()];
            }
            else
            {
                PlayerPrefs.SetInt("NRDif", 0);
                PlayerPrefs.SetInt("GSDif", 0);
                PlayerPrefs.SetInt("TZDif", 0);
                Difficult = difficulty[GetDiff()];
            }
        }
        Inizializition();
    }

    private int GetDiff()
    {
        string nameScene = SceneManager.GetActiveScene().name;
        if (nameScene == "NightRoad")
        {
            return PlayerPrefs.GetInt("NRDif");
        }

        else if(nameScene == "GreenCity")
        {
            return PlayerPrefs.GetInt("GSDif");
        }

        else if(nameScene == "ToxicZone")
        {
            return PlayerPrefs.GetInt("TZDif");
        }
        return 0;
    }

    private void Awake()
    {
        _money = FindObjectOfType<Money>();
        _odometr = FindObjectOfType<Odometer>();
        _carGenerator = FindObjectOfType<Generator>();
        _shield = FindObjectOfType<Shield>();
        StartSpeed = _startSpeed;      
        RateOfSpeedGrowth = rateOfSpeedGrowth;
    }


    private void Inizializition()
    {
        _carGenerator.SpawnPeriod = _startCarSpawnPeriod;

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
        _odometr.SetDistSpeed(_distSpeed);
        _odometr.SetKm4Money(_rewardedKilometers);
        _money.SetKmRewardVar(_moneyForDistance);
        _shield._activationTime = _shieldActionTime;
        StartCoroutine(LateInizialization());
    }

    public void SetCarSpawnPeriod(float val)
    {
        if (_carGenerator.SpawnPeriod > _minCarSpawnPeriod)
            _carGenerator.SpawnPeriod -= val;
        else
            _carGenerator.SpawnPeriod = _minCarSpawnPeriod;
    }

    private IEnumerator LateInizialization()
    {
        yield return new WaitForSeconds(2f);
        _policeCar = FindObjectOfType<PoliceCar>();
        _policeCar.SetShieldActionTime(_shieldActionTime);
    }

}
