using System;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.PackageManager.Requests;
//using Unity.VisualScripting;

public class Achivments : MonoBehaviour
{
    //PlayerPrefs, 0 - �� ����������, 1 - ���������
    //CarsCount, ����������� ��������� ����
    //LocationCount, ����������� ��������� �������
    //MaxSpeed, ������������ �������� � ������ �� ��� �����
    //DeathCount, ������� �������
    //RebirthCount, ����������� ������������ �� �������
    //SecondLocation, 0 - �������, 1 - �������
    //BestMilage, ������������ ������ �� �����, �� ��� �����
    //MileageOn..., ����� ����� ���������� ����������� �� ��������� ������� 
    //PurchasedCars, ����������� ��������� �����
    //CompletedTests, ����������� ����������� ��������� �� �����
    [SerializeField] private List<Task> _allAchivments;
    [SerializeField] private TaskPanelControll _tPC;
    [SerializeField] private ShopItem _thirdLocation;

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
        //�����������
        if (PlayerPrefs.GetInt("CarsCount") >= 2)
        {
            for(int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 1)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    OnAchivPanel();
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
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + 100);
                OffAchivPanel();
                _allAchivments.RemoveAt(i);
            }
        }
    }

    private void AchivmentTwo()
    {
        //�����������
        if(PlayerPrefs.GetInt("MaxSpeed") == 260)
        {
            for(int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 2)
                {
                    _allAchivments[i].ChangeCompleted();
                    _allAchivments[i].ChangePersent(100);
                    OnAchivPanel();
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
                _allAchivments.RemoveAt(i);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + 100);
            }
        }
    }

    private void AchivmentThree()
    {
        //�����������
        if(PlayerPrefs.GetInt("LocationCount") >= 2)
        {
            for (int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 3)
                {
                    _allAchivments[i].ChangeCompleted();
                    _allAchivments[i].ChangePersent(100);
                    OnAchivPanel();
                    PlayerPrefs.SetInt("CompletedTests", PlayerPrefs.GetInt("CompletedTests") + 1);
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
                _allAchivments.RemoveAt(i);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + 100);
            }
        }
    }

    private void AchivmentFour()
    {
        //�����������
        int deathCount = PlayerPrefs.GetInt("DeatCount");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 4)
            {
                if (deathCount >= 10)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    OnAchivPanel();
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

    public void AchivmentFourGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 4)
            {
                _allAchivments[i].ChangeCompleted();
                _allAchivments.RemoveAt(i);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + 100);
            }
        }
    }

    private void AchivmentFive()
    {
        //�����������
        int rebirthCount = PlayerPrefs.GetInt("RebirthCount");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 5)
            {
                if (rebirthCount >= 7)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    OnAchivPanel();
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
    public void AchivmentFiveGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 5)
            {
                _allAchivments[i].ChangeCompleted();
                _allAchivments.RemoveAt(i);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + 100);
            }
        }
    }

    private void AchivmentSix()
    {
        //�����������
        int n = PlayerPrefs.GetInt("BestMilage");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 6)
            {
                if (n >= 666)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    OnAchivPanel();
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

    public void AchivmentSixGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 6)
            {
                _allAchivments[i].ChangeCompleted();
                _allAchivments.RemoveAt(i);
                OffAchivPanel();
                //���� ������
            }
        }
    }

    private void AchivmentSeven()
    {
        //�����������
        int n = PlayerPrefs.GetInt("CountPlayInFreeRide");
        for(int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 7)
            {
                if (n >= 25)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    OnAchivPanel();
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

    public void AchivmentSevenGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 7)
            {
                _allAchivments[i].ChangeCompleted();
                _allAchivments.RemoveAt(i);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + 100);
            }
        }
    }

    private void AchivmentEight()
    {
        //�����������
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
                    OnAchivPanel();
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

    public void AchivmentEightGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 8)
            {
                _allAchivments[i].ChangeCompleted();
                _allAchivments.RemoveAt(i);
                OffAchivPanel();
                _thirdLocation.UnlockItem();
            }
        }
    }

    private void AchivmentNine()
    {
        //�����������
        int n = PlayerPrefs.GetInt("CarsCount");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 9)
            {
                if (n == 4)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    OnAchivPanel();
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

    public void AchivmentNineGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 9)
            {
                _allAchivments[i].ChangeCompleted();
                _allAchivments.RemoveAt(i);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + 100);
            }
        }
    }

    private void AchivmentTen()
    {
        //�����������
        int n = PlayerPrefs.GetInt("CompletedTests");
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 10)
            {
                if (n == 12)
                {
                    _allAchivments[i].ChangeReceivePanel();
                    _allAchivments[i].ChangePersent(100);
                    OnAchivPanel();
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

    public void AchivmentTenGetReward()
    {
        for (int i = 0; i < _allAchivments.Count; i++)
        {
            if (_allAchivments[i].NumberAchiv == 10)
            {
                _allAchivments[i].ChangeCompleted();
                _allAchivments.RemoveAt(i);
                OffAchivPanel();
                PlayerPrefs.SetInt("MoneyNameConst", PlayerPrefs.GetInt("MoneyNameConst") + 100);
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
        bool _needStartUnVisible = false;
        foreach (Task task in _allAchivments)
        {
            if (task.NeedReceive == true)
            {
                _needStartUnVisible = false;
                break;
            }
        }
        if(_needStartUnVisible)
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
