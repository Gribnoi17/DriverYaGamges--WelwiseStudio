using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;

public class StartRace : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string RateGame();

    [SerializeField] private GameRules _gameRuler;
    [SerializeField] private GameObject _endConfetti;
    [SerializeField] private Generator _gen;

    [Header("UI")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _finalTimeText;

    private Speedometer _spd;
    private LosePanelActivation _losePanelScript;
    void Start()
    {
        _spd = FindObjectOfType<Speedometer>();
        _losePanelScript = FindObjectOfType<LosePanelActivation>();
        _gameRuler = GetComponent<GameRules>();
        if (_gameRuler.Regime == _gameRuler.regimeRace[0])
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
        _gen.needSpawnShield = true;
        PlayerPrefs.SetInt("CountPlayInFreeRide", PlayerPrefs.GetInt("CountPlayInFreeRide") + 1);
        _timer.gameObject.SetActive(false);
    }

    void StartRaceForTime()
    {
        _gen.needSpawnShield = false;
        _timer.gameObject.SetActive(true);
        _timer.transform.parent.gameObject.SetActive(true);

        if (_gameRuler.Difficult == _gameRuler.difficulty[0])
        {
            _timer.text = "60";
        }
        else if (_gameRuler.Difficult == _gameRuler.difficulty[1])
        {
            _timer.text = "180";

        }
        else if (_gameRuler.Difficult == _gameRuler.difficulty[2])
        {
            _timer.text = "360";
        }
        _finalTimeText.text = _timer.text;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        int _nt = Convert.ToInt32(_timer.text);
        while (true)
        {
            int nextTime = _nt - 1;
            _nt = nextTime;
            if (nextTime / 60d > 1)
                if (nextTime - (Math.Truncate(nextTime / 60d) * 60) < 10)
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
        StartCoroutine(Win());
    }


    private IEnumerator Win()
    {
        _timer.text = "0";
        while (_spd.CurrentSpeed > 0)
        {
            _spd.CurrentSpeed = -1;
        }
        _endConfetti.SetActive(true);
        _losePanelScript.Pause();
        yield return new WaitForSeconds(2f);
        _winPanel.SetActive(true);
        if(_gameRuler.Difficult == _gameRuler.difficulty[1])
        {
            RateGame();
        }
        print("----Звук победы----");
    }
}
