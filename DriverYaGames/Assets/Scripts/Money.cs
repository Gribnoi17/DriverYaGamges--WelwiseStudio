using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public const string MoneyNameConst = "money";

    [SerializeField] private TextMeshProUGUI _moneyText;
    private int numberOfCoinsToBeAdded;
    private int _currentAmount = 0;
    private void Start()
    {
        EventManager.DroveKmForMoney += AddMoney;
        _moneyText.text = "0$";
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
        _currentAmount += numberOfCoinsToBeAdded;
        _moneyText.text = _currentAmount.ToString() + "$";
    }

    public void SetKmRewardVar(int value)
    {
        numberOfCoinsToBeAdded = value;
    }


}
