using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class Achivments : MonoBehaviour
{
    //PlayerPrefs, 0 - не выполненно, 1 - выполнено
    //CarsCount, колличество купленных авто
    //LocationsCount, колличетсов купленных локаций
    //MaxSpeed, максимальная скорость в заезде за все время
    //DeathCount, счетчик смертей
    //RebirthCount, колличество перерождений за рекламу
    //SecondLocation, 0 - закрыта, 1 - открыта
    //BestMilage, максимальный рекорд за заезд, за все время
    //MileageOn..., общая сумма пройденных киллометров на указанной локации 
    //PurchasedCars, колличество купленных машин
    //CompletedTests, колличество выполненных испытаний на время
    [SerializeField] private List<Task> _allAchivments;
    [SerializeField] private TaskPanelControll _tPC;
    [SerializeField] private ShopItem _thirdLocation;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private ShopItem _car;

    [Header("To view")]
    [SerializeField] private int countNotReceived = 0;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();

        CheckActivAchivs();

        if (!PlayerPrefs.HasKey("LocationsCount")) 
            PlayerPrefs.SetInt("LocationsCount", 1);
        if (!PlayerPrefs.HasKey("CarsCount")) 
            PlayerPrefs.SetInt("CarsCount", 1);

        StartCoroutine(ReCheck());
    }

    private void CheckAchiv(int numAchiv)
    {
        switch(numAchiv)
        {
            case 1:
                AchivmentOne();
                break;
            case 2:
                AchivmentTwo();
                break;
            case 3:
                AchivmentThree();
                break;
            case 4:
                AchivmentFour();
                break;
            case 5:
                AchivmentFive();
                break;
            case 6:
                AchivmentSix();
                break;
            case 7:
                AchivmentSeven();
                break;
            case 8:
                AchivmentEight();
                break;
            case 9:
                AchivmentNine();
                break;
            case 10:
                AchivmentTen();
                break;
        }
    }

    private void AchivmentOne()
    {
        if (PlayerPrefs.GetInt("CarsCount") >= 2)
        {
            for(int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 1)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    break;
                }
            }
        }
    }

    public void AchivmentOneGetReward()
    {
        for (int i = 0; i < _allAchivments.Count;i++)
        {
            if(_allAchivments[i].NumberAchiv == 1)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(1);
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _allAchivments[i].GetCountMoneuForReward());
                UpdateMoneyText();
                OffAchivPanel();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentTwo()
    {
        if(PlayerPrefs.GetInt("MaxSpeed") >= 360)
        {
            for(int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 2)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    break;
                }
            }
        }
    }

    public void AchivmentTwoGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 2)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(2);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _allAchivments[i].GetCountMoneuForReward());
                UpdateMoneyText();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentThree()
    {
        if(PlayerPrefs.GetInt("LocationsCount") >= 2)
        {
            for (int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 3)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    break;
                }
            }
        }
    }

    public void AchivmentThreeGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 3)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(3);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _allAchivments[i].GetCountMoneuForReward());
                UpdateMoneyText();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentFour()
    {
        int deathCount = PlayerPrefs.GetInt("DeatCount");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 4)
            {
                if (deathCount >= 10)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    UpdateMoneyText();
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(deathCount * 10);
                    break;
                }
            }
        }    
    }

    public void AchivmentFourGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 4)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(4);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _allAchivments[i].GetCountMoneuForReward());
                UpdateMoneyText();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentFive()
    {
        int rebirthCount = PlayerPrefs.GetInt("RebirthCount");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 5)
            {
                if (rebirthCount >= 7)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    UpdateMoneyText();
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(rebirthCount * 100f / 7f));
                    break;
                }
            }
        }
    }
    public void AchivmentFiveGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 5)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(5);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _allAchivments[i].GetCountMoneuForReward());
                UpdateMoneyText();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentSix()
    {
        int n = PlayerPrefs.GetInt("BestMilage");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 6)
            {
                if (n >= 666)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(n * 100f / 666f));
                    break;
                }
            }
        }
    }

    public void AchivmentSixGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 6)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(6);
                OffAchivPanel();
                _car.UnlockItem();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentSeven()
    {
        int n = PlayerPrefs.GetInt("CountPlayInFreeRide");
        for(int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 7)
            {
                if (n >= 25)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(n * 100f / 25f));
                    break;
                }
            }    
        }
    }

    public void AchivmentSevenGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 7)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(7);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _allAchivments[i].GetCountMoneuForReward());
                UpdateMoneyText();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentEight()
    {
        int n = PlayerPrefs.GetInt("MileageOnFirstLocation");
        int nn = PlayerPrefs.GetInt("MileageOnSecondLocation");
        if(n > 1000)
            n = 1000;
        if(nn > 1000)
            nn = 1000;

        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 8)
            {
                if (n >= 1000 && nn >= 1000)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32((n + nn) * 100f / 2000f));
                    break;
                }
            }
        }
    }

    public void AchivmentEightGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 8)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(8);
                OffAchivPanel();
                _thirdLocation.UnlockItem();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentNine()
    {
        for ( int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 9)
            {
                if (PlayerPrefs.GetInt("CarsCount") == 4)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(PlayerPrefs.GetInt("CarsCount") * 100f / 4));
                    break;
                }
            }
        }
    }

    public void AchivmentNineGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 9)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(9);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _allAchivments[i].GetCountMoneuForReward());
                UpdateMoneyText();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentTen()
    {
        int n = PlayerPrefs.GetInt("CompletedTests");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 10)
            {
                if (n >= 9)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    OnAchivPanel();
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(n * 100f / 9));
                    break;
                }
            }
        }
    }

    public void AchivmentTenGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 10)
            {
                _allAchivments[i].ChangeCompleted();
                ChangeCompleted(10);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + _allAchivments[i].GetCountMoneuForReward());
                UpdateMoneyText();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void OnAchivPanel()
    {
        if(!_tPC.AlreadyRunning)
            _tPC.StartVisible();
    }
    private void OffAchivPanel()
    {
        bool _needStartUnVisible = true;
        foreach (Task task in _allAchivments)
        {
            if (task.NeedReceive == true)
            {
                _needStartUnVisible = false;
                break;
            }
        }
        if(_needStartUnVisible)
        {
            _tPC.StartUnVisible();
        }
             
    }

    public IEnumerator ReCheck()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (Task task in _allAchivments)
        {
            CheckAchiv(task.NumberAchiv);
        }
        StopCoroutine(ReCheck());
    }

    private void CheckActivAchivs()
    {
        for(int numAchiv = 0; numAchiv < _allAchivments.Count; numAchiv++)
        {
            int n = _allAchivments[numAchiv].NumberAchiv;
            if (PlayerPrefs.HasKey($"A{n}"))
            {
                if (PlayerPrefs.GetInt($"A{n}") == 0)
                    _allAchivments[numAchiv].Completed = false;
                else
                    _allAchivments[numAchiv].Completed = true;
            }
            else
            { _allAchivments[numAchiv].Completed = false; PlayerPrefs.SetInt($"A{n}", 0); }
        }

        var _numForDel = new List<int>();
        foreach (Task t in _allAchivments)
        {
            if(t.Completed)
            {
                for(int i = 0; i < _allAchivments.Count; i++)
                {
                    if (_allAchivments[i].NumberAchiv == t.NumberAchiv)
                    {
                        _allAchivments[i].ChangeCompleted();
                        _numForDel.Add(_allAchivments[i].NumberAchiv);
                    }   
                }
            }
        }
        foreach(int i in _numForDel)
        {
            for(int n = 0; n < _allAchivments.Count; n++)
            {
                if (_allAchivments[n].NumberAchiv == i)
                {
                    _allAchivments.RemoveAt(n);
                }
            }
        }
        ReCheck();
    }

    private void ChangeCompleted(int numAchiv)
    {
        PlayerPrefs.SetInt($"A{numAchiv}", 1);
    }

    private void UpdateMoneyText()
    {
        _moneyText.text = PlayerPrefs.GetInt("MoneyNameConst").ToString();
    }
}
