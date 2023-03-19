using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartRace : MonoBehaviour
{
    [SerializeField] private GameObject _gameRuler;
    [SerializeField] private TextMeshProUGUI _timer;
    void Start()
    {
        if(_gameRuler.GetComponent<GameRules>().Regime == _gameRuler.GetComponent<GameRules>().regimeRace[0])
        {
            StartFreeRace();
        }
        else
        {
            StartRaceForTime();
        }
    }

    void StartFreeRace()
    {
        _timer.gameObject.SetActive(false);
        StartRaceText();
    }

    void StartRaceForTime()
    {
        StartRaceText();

        _timer.gameObject.SetActive(true);

        if (_gameRuler.GetComponent<GameRules>().Difficult == _gameRuler.GetComponent<GameRules>().difficulty[0])
        {
            _timer.text = 30.ToString();
        }
        else if (_gameRuler.GetComponent<GameRules>().Difficult == _gameRuler.GetComponent<GameRules>().difficulty[1])
        {
            _timer.text = 60.ToString();
        }
        else if (_gameRuler.GetComponent<GameRules>().Difficult == _gameRuler.GetComponent<GameRules>().difficulty[2])
        {
            _timer.text = 120.ToString();
        }

        StartCoroutine(Timer());

        IEnumerator Timer()
        {
            while(true)
            {
                yield return new WaitForSeconds(1f);
                _timer.text = Convert.ToString(Convert.ToInt32(_timer.text) - 1);
                if (Convert.ToInt32(_timer.text) == 0)
                    break;

            }

            //вызываем финиш заезда 
            StopCoroutine(Timer());
        }
    }

    IEnumerator StartRaceText()
    {
        //READY
         yield return new WaitForSeconds(1f);
        //TO
         yield return new WaitForSeconds(1f);
        //GO!
    }
}
