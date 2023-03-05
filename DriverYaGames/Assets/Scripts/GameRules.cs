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

    private string regime;

    public string Regime { get { return regime; }  private set { regime = value; } }

    public Dictionary<int, string> regimeRace = new Dictionary<int, string>()
    {
            { 0, "FreeRace"},
            { 1, "RaceForTime"},
    };

    private void Start()
    {
        //выгружаем из PlayerPrefs 
        // int _regime и меняем нули
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
    }
}
