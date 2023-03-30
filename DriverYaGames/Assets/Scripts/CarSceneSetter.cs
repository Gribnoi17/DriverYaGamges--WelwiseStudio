using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSceneSetter : MonoBehaviour
{
    [SerializeField] private GameObject PoliceCar;
    [SerializeField] private GameObject WhitePoliceCar;
    [SerializeField] private GameObject SportCar;
    [SerializeField] private GameObject SciFiCar;


    private void Start()
    {
        SetAndActivateCar();
    }

    public void SetAndActivateCar()
    {
        if (PlayerPrefs.GetString("Car") == "PoliceCar")
        {
            PoliceCar.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetString("Car") == "WhitePoliceCar")
        {
            WhitePoliceCar.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetString("Car") == "SportCar")
        {
            SportCar.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetString("Car") == "SciFiCar")
        {
            SciFiCar.gameObject.SetActive(true);
        }
    }
}
