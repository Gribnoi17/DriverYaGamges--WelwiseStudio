using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action PlayerDied;
    public static event Action DroveKmForMoney;
    public static event Action PlayerTookShield;
    public static event Action PlayerTookNitro;
    public static event Action PlayerTookCamera;
    public static void OnPlayerDied()
    {
        PlayerDied?.Invoke();
    }

    public static void OnDroveKmForMoney()
    {
        DroveKmForMoney?.Invoke();
    }

	public static void OnPlayerTookShield()
	{
		PlayerTookShield?.Invoke();
	}

    public static void OnPlayerTookNitro()
    {
        PlayerTookNitro?.Invoke();
    }
    public static void OnPlayerTookCamera()
    {
        PlayerTookCamera?.Invoke();
    }
}
