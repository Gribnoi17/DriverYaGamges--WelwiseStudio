using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSceneSetter : MonoBehaviour
{
    [SerializeField] private GameObject _policeCar;
    [SerializeField] private GameObject _whitePoliceCar;
    [SerializeField] private GameObject _sportCar;
    [SerializeField] private GameObject _sciFiCar;
    private List<GameObject> _cars;

    private void Start()
    {
        SetAndActivateCar();
    }

    public void SetAndActivateCar()
    {
        if (PlayerPrefs.GetString("Car") == "PoliceCar")
        {
            _policeCar.gameObject.SetActive(true);
            Destroy(_sciFiCar);
            Destroy(_sportCar);
            Destroy(_whitePoliceCar);
        }

        if (PlayerPrefs.GetString("Car") == "WhitePoliceCar")
        {
            _whitePoliceCar.gameObject.SetActive(true);
            Destroy(_sciFiCar);
            Destroy(_sportCar);
            Destroy(_policeCar);
        }

        if (PlayerPrefs.GetString("Car") == "SportCar")
        {
            _sportCar.gameObject.SetActive(true);
            Destroy(_sciFiCar);
            Destroy(_policeCar);
            Destroy(_whitePoliceCar);
        }

        if (PlayerPrefs.GetString("Car") == "SciFiCar")
        {
            _sciFiCar.gameObject.SetActive(true);
            Destroy(_sportCar);
            Destroy(_policeCar);
            Destroy(_whitePoliceCar);
        }
    }
}
