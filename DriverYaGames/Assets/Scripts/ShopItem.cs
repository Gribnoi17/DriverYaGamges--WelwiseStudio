using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] _typeOfItem _ItemType = _typeOfItem.Car;
    [SerializeField] private string _itemName;
    public int _itemCost = 50;
    public bool _isLocked = true;
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Achivments _ach;
    [SerializeField] private SliderVolumeController _svc;

    private PanelAnimation _panelAnimation;
    private enum _typeOfItem { Car, Location }
    private GarageSetUp garageSetUp;


    private void Start()
    {
        _panelAnimation = FindObjectOfType<PanelAnimation>();
        garageSetUp = FindObjectOfType<GarageSetUp>();


        if (!PlayerPrefs.HasKey(_itemName))
        {
            if(_isLocked)
                PlayerPrefs.SetString(_itemName, "Locked");
            else
                UnlockItem();
        }
        else if(PlayerPrefs.GetString(_itemName) == "Unlocked")
        {
            UnlockItem();
        }

        if (_isLocked)
            _lockPanel.SetActive(true);
    }

    public void UnlockItem()
    {
        if(_svc != null)
            _svc.BuyButtonSound();
        _lockPanel.SetActive(false);
        PlayerPrefs.SetString(_itemName, "Unlocked");
        garageSetUp.SetLocationByPlayerPrefs();
        _isLocked = false;
        StartCoroutine(_ach.ReCheck());
    }

    public void BuyItem()
    {
        if(PlayerPrefs.GetInt("MoneyNameConst") >= _itemCost && _isLocked)
        {
            PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") - _itemCost);
            UnlockItem();
            if (_ItemType == _typeOfItem.Car)
            {
                PlayerPrefs.SetInt("CarsCount", PlayerPrefs.GetInt("CarsCount") + 1);
                PlayerPrefs.SetString("Car", _itemName);
            }
            else
            {
                PlayerPrefs.SetString("ChousenLocation", _itemName);
                PlayerPrefs.SetInt("LocationsCount", PlayerPrefs.GetInt("LocationsCount") + 1);
            }
            _buyButton.gameObject.SetActive(false);
            garageSetUp.Congrats();
        }
    }


    public void ActivateBuyButton()
    {
        if (_panelAnimation.GetIsTransitioningBtwCars() == true)
            return;
        if(_isLocked == true)
        {
            _buyButton.gameObject.SetActive(true);
            _buyButton.onClick.RemoveAllListeners();
            _buyButton.onClick.AddListener(BuyItem);
            _buyButton.onClick.AddListener(_panelAnimation.UpdateMoneyText);
        }
        else
        {
            _buyButton.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _buyButton.onClick.RemoveAllListeners();
    }

}
