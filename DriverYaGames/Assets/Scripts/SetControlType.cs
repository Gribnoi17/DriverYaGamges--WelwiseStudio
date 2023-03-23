using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetControlType : MonoBehaviour
{

    private AcsController _acsController;
    private SwipeDetection _swipeDetection;

    private void Awake()
    {
        _acsController = FindObjectOfType<AcsController>();
        _swipeDetection = FindObjectOfType<SwipeDetection>();
    }

    private void Start()
    {
        if(PlayerPrefs.GetString("ControllerType") == "Slant")
        {
            _acsController.gameObject.SetActive(true);
            _swipeDetection.gameObject.SetActive(false);

        }
        else
        {
            _acsController.gameObject.SetActive(false);
            _swipeDetection.gameObject.SetActive(true);
        }
    }
}
