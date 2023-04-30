using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class LosePanelActivation : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _audioSources;
    [SerializeField] TextMeshProUGUI _milageResultText;
    [SerializeField] TextMeshProUGUI[] _moneyResultText;
    [DllImport("__Internal")]
    private static extern void ReturnToGameExtern();

    private Generator _carGenerator;
    private CarSceneSetter _carSceneSetter;
    private Odometer _odometer;
    private Speedometer _speedometer;
    private Animator _anim;
    private Money _money;
    private PauseController _pauseController;
    private bool _watchedAdv = false;

    private void Awake()
    {
        gameObject.transform.SetParent(null, false);
    }

    private void Start()
    {
        Invoke(nameof(Initialization), 1.5f);
        //Initialization();
        //EventManager.PlayerDied += ShowPanelThroughtTime;
    }

    private void Initialization()
    {
        print("Сука, где спидометр????");
        _audioSources = GameObject.Find("AuduoSources");
        _pauseController = FindObjectOfType<PauseController>();
        _money = FindObjectOfType<Money>();
        _watchedAdv = false;
        _anim = _losePanel.GetComponent<Animator>();
        _speedometer = FindObjectOfType<Speedometer>();
        _carSceneSetter = FindObjectOfType<CarSceneSetter>();
        _carGenerator = FindObjectOfType<Generator>();
        _odometer = FindObjectOfType<Odometer>();
    }

    private void OnDestroy()
    {
        //EventManager.PlayerDied -= ShowPanelThroughtTime;
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _money.GetCurrentAmount());
        Destroy(_carGenerator);
    }

    public void ShowPanelThroughtTime()
    {
        Pause();
        Invoke("ActivateLosePanel", 0.7f);
    }


    private void ActivateLosePanel()
    {
        _losePanel.SetActive(true);
        _milageResultText.text = _odometer.GetCurrentMilage().ToString();
        if (_watchedAdv == false)
        {
            _anim.Play("LosePanelAnim");
            _watchedAdv = true;
        }
        else
        {
            _anim.Play("LosePanelAnim Only Menu");
            DOTween.KillAll();
        }
    }


    public void Pause()
    {
        foreach (TextMeshProUGUI txt in _moneyResultText)
        {
            txt.text = _money.GetCurrentAmount().ToString();
        }

        _speedometer.enabled = false;
        _odometer.IsCounting(false);
        if(_audioSources != null)
            _audioSources.gameObject.SetActive(false);
        _carGenerator.RemoveAllChildren();
        _carGenerator.gameObject.SetActive(false);
    }


    public void ShowRewardedAdv()
    {
        ReturnToGameExtern();
    }

    public void ReturnToGame()
    {
        if (_audioSources != null)
        {
            _audioSources.gameObject.SetActive(true);
        }
        _pauseController.PauseStart();
        PlayerPrefs.SetInt("RebirthCount", PlayerPrefs.GetInt("RebirthCount") + 1);
        _speedometer.enabled = true;
        _odometer.IsCounting(true);
        _carGenerator.gameObject.SetActive(true);
        _carSceneSetter.SetAndActivateCar();
        _losePanel.SetActive(false);
    }
}
