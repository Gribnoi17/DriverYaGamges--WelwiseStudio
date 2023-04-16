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

        if (PlayerPrefs.GetString("ControllerType") == "Swipe")
        {
            StartCoroutine(WaitForSwipeDet());
        }
        if (PlayerPrefs.GetString("ControllerType") == "Slant")
        {
            StartCoroutine(WaitForAcs());
        }
        if ((PlayerPrefs.GetString("ControllerType") == "Keyboard"))
        {
            StartCoroutine(WaitForKeyboard());
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
        _swipeDetection.SetActive(false);
        yield return new WaitForSeconds(4f);
        _swipeDetection.SetActive(true);
    }


    IEnumerator WaitForKeyboard()
    {
        foreach (PoliceCar car in _cars)
        {
            car.CanMove = false;
        }
        yield return new WaitForSeconds(4f);

        foreach (PoliceCar car in _cars)
        {
            car.CanMove = true;
        }

    }
}
