using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GetLanguage : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetLang();
    private string _currentLanguage;

    private void Awake()
    {
        _currentLanguage = GetLang();
        if (_currentLanguage == null)
        {
            _currentLanguage = "en";
            _currentLanguage = GetLang();
            Start();
        }
    }

    private void Start()
    {
        if (_currentLanguage == "ru")
        {
            PlayerPrefs.SetString("Language", "ru");
        }
        else if (_currentLanguage == "tr")
        {
            PlayerPrefs.SetString("Language", "tr");
        }
        else
        {
            PlayerPrefs.SetString("Language", "en");
        }
    }
}
