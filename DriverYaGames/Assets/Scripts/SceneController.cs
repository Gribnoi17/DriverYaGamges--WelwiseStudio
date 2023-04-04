using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject _acsController;
    [SerializeField] GameObject _swipeDetection;

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
        _swipeDetection.SetActive(false);
        yield return new WaitForSeconds(4f);
        _swipeDetection.SetActive(true);
    }
}
