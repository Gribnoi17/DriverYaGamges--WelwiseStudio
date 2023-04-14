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
        _lockPanel.SetActive(false);
        PlayerPrefs.SetString(_itemName, "Unlocked");
        _isLocked = false;
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
                print(PlayerPrefs.GetInt("CarsCount"));
            }
            else
            {
                PlayerPrefs.SetInt("LocationsCount", PlayerPrefs.GetInt("LocationsCount") + 1);
                print(PlayerPrefs.GetInt("LocationsCount"));
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
