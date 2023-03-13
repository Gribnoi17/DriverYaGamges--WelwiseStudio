using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Odometer : MonoBehaviour
{
    [SerializeField] private Text odomText;
    [SerializeField] private GameObject getterSpeed;
    [SerializeField] private float distSpeed;
    private Speedometer _speedometr;
    private float dist;

    private void Awake()
    {
        dist = 0f;
         _speedometr = getterSpeed.GetComponent<Speedometer>();
    }
    void Update()
    {
        float kilom = _speedometr.GetCurSpeed() * (Time.deltaTime / distSpeed);
        dist += kilom;
        odomText.text = $"{Mathf.Round(dist)} km";
    }
}
