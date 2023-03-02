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


    private void Awake()
    {
        StartSpeed = startSpeed;
        MaxSpeed = maxSpeed;
        RateOfSpeedGrowth = rateOfSpeedGrowth;
    }


}
