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
        if (_gameRuler.GetComponent<GameRules>().Regime == _gameRuler.GetComponent<GameRules>().regimeRace[0])
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
        PlayerPrefs.SetInt("CountPlayInFreeRide", PlayerPrefs.GetInt("CountPlayInFreeRide") + 1);
        _timer.gameObject.SetActive(false);
    }

    void StartRaceForTime()
    {
        _timer.gameObject.SetActive(true);
        _timer.transform.parent.gameObject.SetActive(true);

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
            int _nt = Convert.ToInt32(_timer.text);
            while(true)
            {
                int nextTime = _nt - 1;
                _nt = nextTime;
                if (nextTime / 60d > 1)
                    if(nextTime - (Math.Truncate(nextTime / 60d) * 60) < 10)
                        _timer.text = $"{Math.Truncate(nextTime / 60d)}:0{nextTime - (Math.Truncate(nextTime / 60d) * 60)}";
                    else
                        _timer.text = $"{Math.Truncate(nextTime / 60d)}:{nextTime - (Math.Truncate(nextTime / 60d) * 60)}";
                else
                    _timer.text = Convert.ToString(Convert.ToInt32(_nt) - 1).ToString();

                if (_nt == 0 || _nt < 0)
                    break;
                else
                    yield return new WaitForSeconds(1f);
            }
            StopCoroutine(Timer());
        }
    }
}
