using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _acsController;
    [SerializeField] private GameObject _swipeDetection;
    private PoliceCar _activeCar;

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
        _swipeDetection.SetActive(false);
        yield return new WaitForSeconds(4f);
        _swipeDetection.SetActive(true);
    }


    IEnumerator WaitForKeyboard()
    {
        yield return new WaitForSeconds(0.5f);
        _activeCar = FindObjectOfType<PoliceCar>();
        _activeCar.CanMove = false;
        yield return new WaitForSeconds(3.5f);
        _activeCar.CanMove = true;
    }

    public void DeatctivateAllControl()
    {
        _activeCar.CanMove = false;
        _swipeDetection.SetActive(false);
        _acsController.SetActive(false);
    }
}
