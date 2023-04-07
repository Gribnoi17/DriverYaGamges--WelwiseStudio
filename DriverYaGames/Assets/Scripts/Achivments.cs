using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.PackageManager.Requests;
using Unity.VisualScripting;

public class Achivments : MonoBehaviour
{
    //PlayerPrefs, 0 - не выполненно, 1 - выполнено
    //ByuCar, колличество купленных авто
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

    [Header("To view")]
    [SerializeField] private int countNotReceived = 0;

    private void Start()
    {
        ReCheck();
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
        if(PlayerPrefs.GetInt("BuyCar") >= 1)
        {
            for(int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 1)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
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
                //дать денег
            }
        }
    }

    private void AchivmentTwo()
    {
        //реализовано
        if(PlayerPrefs.GetInt("MaxSpeed") == 260)
        {
            for(int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 2)
                {
                    _allAchivments[i].ChangeCompleted();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
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
                //дать денег
            }
        }
    }

    private void AchivmentThree()
    {
        if(PlayerPrefs.GetInt("SecondLocation") == 1)
        {
            for (int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 3)
                {
                    _allAchivments[i].ChangeCompleted();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
                    break;
                }
            }
        }
    }

    private void AchivmentFour()
    {
        //реализовано
        int deathCount = PlayerPrefs.GetInt("DeatCount");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 4)
            {
                if (deathCount >= 10)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
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

    private void AchivmentFive()
    {
        //реализовано
        int rebirthCount = PlayerPrefs.GetInt("RebirthCount");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 5)
            {
                if (rebirthCount >= 7)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(rebirthCount * 100f % 7f));
                    break;
                }
            }
        }
    }

    private void AchivmentSix()
    {
        //реализовано
        int n = PlayerPrefs.GetInt("BestMilage");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 6)
            {
                if (n >= 666)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(n * 100f % 666f));
                    break;
                }
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
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(n * 100f % 25f));
                    break;
                }
            }    
        }
    }

    private void AchivmentEight()
    {
        int n = PlayerPrefs.GetInt("MileageOnFirstLocation");
        int nn = PlayerPrefs.GetInt("MileageOnSecondLocation");

        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 8)
            {
                if (n >= 1000 && nn >= 1000)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32((n + nn) * 100f % 2000f));
                    break;
                }
            }
        }
    }

    private void AchivmentNine()
    {
        int n = PlayerPrefs.GetInt("PurchasedCars");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 9)
            {
                if (n == 4)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(n * 100f % 4));
                    break;
                }
            }
        }
    }

    private void AchivmentTen()
    {
        //реализовано
        int n = PlayerPrefs.GetInt("CompletedTests");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 10)
            {
                if (n == 12)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
                    break;
                }
                else
                {
                    _allAchivments[i].ChangePersent(Convert.ToInt32(n * 100f % 9));
                    break;
                }
            }
        }
    }

    private void OnAchivPanel()
    {
        _tPC.StartVisible();
    }
    private void OffAchivPanel()
    {
        _tPC.StartUnVisible();   
    }

    public void ReCheck()
    {
        foreach (Task task in _allAchivments)
        {
            CheckAchiv(task.NumberAchiv);
        }
    }
}
