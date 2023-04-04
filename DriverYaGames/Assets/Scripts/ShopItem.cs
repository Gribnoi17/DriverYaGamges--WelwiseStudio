using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private string _itemName;
    public int _itemCost = 50;
    public bool _isLocked = true;
    [SerializeField] private GameObject _lockPanel;
    [SerializeField] private Button _buyButton;

    private PanelAnimation _panelAnimation;

    private void Start()
    {

        _panelAnimation = FindObjectOfType<PanelAnimation>();

        if (!PlayerPrefs.HasKey(_itemName))
        {
            if(_isLocked)
                PlayerPrefs.SetString(_itemName, "Locked");
            else
                UnlockLocation();
        }
        else if(PlayerPrefs.GetString(_itemName) == "Unlocked")
        {
            UnlockLocation();
        }

        if (_isLocked)
            _lockPanel.SetActive(true);
    }

    public void UnlockLocation()
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
            UnlockLocation();
        }
    }


    public void ActivateBuyButton()
    {
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
