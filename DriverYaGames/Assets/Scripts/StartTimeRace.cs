using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class StartTimeRace : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string ShowAdv();

    [SerializeField] private GameRules _gameRuler;
    [SerializeField] private GameObject _endConfetti;
    [SerializeField] private Generator _gen;
    [SerializeField] private SliderVolumeController _sVC;

    [Header("UI")]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI _finalTimeText;

    private Speedometer _spd;
    private LosePanelActivation _losePanelScript;
    private PoliceCar _policeCarScript;
    private SceneControllerAnimTime _sceneController;
    private Generator _carGenerator;
    private bool _isTimeCounting = true; 
    void Start()
    {
        _losePanelScript = FindObjectOfType<LosePanelActivation>();

        if (PlayerPrefs.GetInt("RegimeRace") == 0)
        {
            StartFreeRace();
        }

        else if(PlayerPrefs.GetInt("RegimeRace") == 1)
        {
            Invoke(nameof(StartRaceForTime),4f);
        }
    }

    public void IsTimeCounting(bool value)
    {
        _isTimeCounting = value;
    }

    void StartFreeRace()
    {
        _gen.needSpawnShield = true;
        PlayerPrefs.SetInt("CountPlayInFreeRide", PlayerPrefs.GetInt("CountPlayInFreeRide") + 1);
        _timer.gameObject.SetActive(false);
    }

    void StartRaceForTime()
    {
        _sceneController = FindObjectOfType<SceneControllerAnimTime>();
        _carGenerator = FindObjectOfType<Generator>();
        _spd = FindObjectOfType<Speedometer>();
        _policeCarScript = FindObjectOfType<PoliceCar>();
        _gen.needSpawnShield = false;
        _timer.transform.parent.gameObject.SetActive(true);
        _timer.gameObject.SetActive(true);

        if (_gameRuler.Difficult == _gameRuler.difficulty[0])
        {
            _timer.text = "91";
        }

        else if (_gameRuler.Difficult == _gameRuler.difficulty[1])
        {
            _timer.text = "181";
        }

        else if (_gameRuler.Difficult == _gameRuler.difficulty[2])
        {
            _timer.text = "251";
        }

        _finalTimeText.text = _timer.text;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        int _nt = Convert.ToInt32(_timer.text.ToString());

        while (_nt > 1)
        {
            if (_isTimeCounting)
            {
                int nextTime = _nt - 1;
                _nt = nextTime;

                if (nextTime / 60d > 1)
                {
                    if (nextTime - (Math.Truncate(nextTime / 60d) * 60) < 10)
                        _timer.text = $"{Math.Truncate(nextTime / 60d)}:0{nextTime - (Math.Truncate(nextTime / 60d) * 60)}";
                    else
                        _timer.text = $"{Math.Truncate(nextTime / 60d)}:{nextTime - (Math.Truncate(nextTime / 60d) * 60)}";
                }
                else
                {
                    _timer.text = Convert.ToString(Convert.ToInt32(_nt) - 1).ToString();
                }
                
                yield return new WaitForSeconds(1f);
            }
            else
            {
                yield return null;
            }
        }
        StartCoroutine(Win());
    }



    private IEnumerator Win()
    {
        string nameScene = SceneManager.GetActiveScene().name;

        if (nameScene == "NightRoad")
        {
            PlayerPrefs.SetInt("NRDif", PlayerPrefs.GetInt("NRDif") + 1);
            if(PlayerPrefs.GetInt("NRDif") < 4)
                PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
        }

        else if (nameScene == "GreenCity")
        {
            PlayerPrefs.SetInt("GSDif", PlayerPrefs.GetInt("GSDif") + 1);
            if (PlayerPrefs.GetInt("GSDif") < 4)
                PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
        }

        else if (nameScene == "ToxicZone")
        {
            PlayerPrefs.SetInt("TZDif", PlayerPrefs.GetInt("TZDif") + 1);
            if (PlayerPrefs.GetInt("TZDif") < 4)
                PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
        }

        _timer.text = "0";
        while (_spd.CurrentSpeed > 0)
        {
            _spd.CurrentSpeed = -1;
        }

        _endConfetti.SetActive(true);
        if(_sVC != null)
            _sVC.WinSoundsStart();
        _policeCarScript.enabled = false;
        _carGenerator.RemoveAllChildren();
        _sceneController.DeatctivateAllControl();
        _policeCarScript.gameObject.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(2f);
        _winPanel.SetActive(true);
        _losePanelScript.Pause();
        if(_gameRuler.Difficult == _gameRuler.difficulty[1] || _gameRuler.Difficult == _gameRuler.difficulty[2])
        {
            yield return new WaitForSeconds(0.5f);
            ShowAdv();
        }
    }
}
