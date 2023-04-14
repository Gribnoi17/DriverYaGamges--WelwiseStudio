using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _acsController;
    [SerializeField] private GameObject _swipeDetection;
    [SerializeField] private PoliceCar[] _cars;

    private void Start()
    {
        if(_acsController.activeSelf)
        {
            StartCoroutine(WaitForAcs());  
        }else if(_swipeDetection.activeSelf)
        {
            StartCoroutine(WaitForSwipeDet());
        }
    }

    IEnumerator WaitForAcs()
    {
        _acsController.SetActive(false);
        yield return new WaitForSeconds(4f);
        _acsController.SetActive(true);
    }

    IEnumerator WaitForSwipeDet()
    {
        foreach (PoliceCar car in _cars)
        {
            car.CanMove = false;
        }
        _swipeDetection.SetActive(false);
        yield return new WaitForSeconds(4f);
        _swipeDetection.SetActive(true);
        foreach (PoliceCar car in _cars)
        {
            car.CanMove = true;
        }
    }
}
