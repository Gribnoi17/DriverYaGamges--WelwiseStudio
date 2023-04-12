using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Canvas))]
public class LosePanelActivation : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] TextMeshProUGUI _milageResultText;
    [SerializeField] TextMeshProUGUI _moneyResultText;
    [DllImport("__Internal")]
    private static extern void ReturnToGameExtern();

    private Generator _carGenerator;
    private CarSceneSetter _carSceneSetter;
    private Odometer _odometer;
    private Speedometer _speedometer;
    private Animator _anim;
    private Money _money;
    private bool _watchedAdv = false;

    private void Awake()
    {
        gameObject.transform.parent = null;
    }

    private void Start()
    {
        _money = FindObjectOfType<Money>();
        _watchedAdv = false;
        _anim = _losePanel.GetComponent<Animator>();
        _speedometer = FindObjectOfType<Speedometer>();
        _carSceneSetter = FindObjectOfType<CarSceneSetter>();
        _carGenerator = FindObjectOfType<Generator>();
        _odometer = FindObjectOfType<Odometer>();
        EventManager.PlayerDied += ActivateLosePanel;
    }

    private void OnDestroy()
    {
        EventManager.PlayerDied -= ActivateLosePanel;
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _money.GetCurrentAmount());
        _carGenerator.gameObject.SetActive(false);
    }


    private void ActivateLosePanel()
    {
        _milageResultText.text = _odometer.GetCurrentMilage().ToString();
        _moneyResultText.text = _money.GetCurrentAmount().ToString();

        _speedometer.enabled = false;
        _odometer.IsCounting(false);
        _losePanel.SetActive(true);
        if (_watchedAdv == false)
        {
            _anim.Play("LosePanelAnim");
            _watchedAdv = true;
        }
        else
        {
            _anim.Play("LosePanelAnim Only Menu");
        } 
        _carGenerator.RemoveAllChildren();
        _carGenerator.gameObject.SetActive(false);
    }

    public void ShowRewardedAdv()
    {
        ReturnToGameExtern();
    }

    public void ReturnToGame()
    {
        PlayerPrefs.SetInt("RebirthCount", PlayerPrefs.GetInt("RebirthCount") + 1);
        _speedometer.enabled = true;
        _odometer.IsCounting(true);
        _carSceneSetter.SetAndActivateCar();
        _losePanel.SetActive(false);
        _carGenerator.gameObject.SetActive(true);
    }



}
