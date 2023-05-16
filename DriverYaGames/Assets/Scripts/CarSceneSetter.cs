using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSceneSetter : MonoBehaviour
{
    [SerializeField] private GameObject policeCar;
    [SerializeField] private GameObject whitePoliceCar;
    [SerializeField] private GameObject sportCar;
    [SerializeField] private GameObject sciFiCar;

    private void Start()
    {
        SetAndActivateCar();
    }

    public void SetAndActivateCar()
    {
        string selectedCar = PlayerPrefs.GetString("Car");

        if (selectedCar == "PoliceCar")
        {
            ActivateCar(policeCar);
            DestroyCars(sciFiCar, sportCar, whitePoliceCar);
        }

        if (selectedCar == "WhitePoliceCar")
        {
            ActivateCar(whitePoliceCar);
            DestroyCars(sciFiCar, sportCar, policeCar);
        }

        if (selectedCar == "SportCar")
        {
            ActivateCar(sportCar);
            DestroyCars(sciFiCar, policeCar, whitePoliceCar);
        }

        if (selectedCar == "SciFiCar")
        {
            ActivateCar(sciFiCar);
            DestroyCars(sportCar, policeCar, whitePoliceCar);
        }
    }

    private void ActivateCar(GameObject car)
    {
        car.SetActive(true);
    }

    private void DestroyCars(params GameObject[] cars)
    {
        foreach (GameObject car in cars)
        {
            if (car != null)
            {
                Destroy(car);
            }
        }
    }
}
