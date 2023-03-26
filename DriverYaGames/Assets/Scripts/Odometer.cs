using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Odometer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI odomText;
    [SerializeField] private GameObject getterSpeed;
    [SerializeField] private float distSpeed;
    [SerializeField] private float kilomOfMoney = 100;
    private Speedometer _speedometr;
    private float dist;
    private float kilom;
    private float kilomForMoneyTemp;



    private void Awake()
    {
        kilomForMoneyTemp = kilomOfMoney;
        dist = 0f;
         _speedometr = getterSpeed.GetComponent<Speedometer>();
    }
    private void Update()
    {
        kilom = _speedometr.GetCurSpeed() * (Time.deltaTime / distSpeed);
        dist += kilom;
        odomText.text = $"{Mathf.Round(dist)} km";
        if (dist > kilomForMoneyTemp)
        {
            kilomForMoneyTemp += kilomOfMoney;
            EventManager.OnDroveKmForMoney();
        }
    }


}
