using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZoneObjectsDetector : MonoBehaviour
{
    private int _countOfCars = 0;

    public int CountOfCars { get { return _countOfCars; } set { _countOfCars = value; } }

    private PoliceCar _policeCar;

    private void Start()
    {
        _policeCar = FindObjectOfType<CarSceneSetter>().GetComponentInChildren<PoliceCar>(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            if (_countOfCars < 0)
                _countOfCars = 0;
            _countOfCars++;
            if (_countOfCars == 3)
            {
                StartCoroutine(_policeCar.ShieldController());
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            _countOfCars--;
        }
    }
}
