using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public const string MoneyNameConst = "money";

    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private int numberOfCoinsToBeAdded;
    private int _currentAmount = 0;
    private void Start()
    {
        EventManager.DroveKmForMoney += AddMoney;
        _moneyText.text = "0$";
        //_moneyText.text = PlayerPrefs.GetInt(nameof(MoneyNameConst)).ToString() + "$";
    }

    private void OnDestroy()
    {
        EventManager.DroveKmForMoney -= AddMoney;
    }

    public int GetCurrentAmount()
    {
        return _currentAmount;
    }

    public void AddMoney()
    {
        //int money = PlayerPrefs.GetInt(nameof(MoneyNameConst));
        //PlayerPrefs.SetInt(nameof(MoneyNameConst), money + numberOfCoinsToBeAdded);
        _currentAmount += numberOfCoinsToBeAdded;
        _moneyText.text = _currentAmount.ToString() + "$";
        //_moneyText.text = PlayerPrefs.GetInt(nameof(MoneyNameConst)).ToString() + "$";
    }


}
