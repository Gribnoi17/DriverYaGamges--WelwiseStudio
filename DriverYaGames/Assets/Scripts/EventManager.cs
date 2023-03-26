using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action PlayerDied;
    public static event Action DroveKmForMoney;

    public static void OnPlayerDied()
    {
        PlayerDied?.Invoke();
    }

    public static void OnDroveKmForMoney()
    {
        DroveKmForMoney?.Invoke();
    }
}
