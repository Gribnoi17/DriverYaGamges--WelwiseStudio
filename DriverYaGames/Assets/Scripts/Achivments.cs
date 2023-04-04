using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
//using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class Achivments : MonoBehaviour
{
    //PlayerPrefs, 0 - не выполненно, 1 - выполнено
    [SerializeField] private List<Task> _allAchivments;

    private void Start()
    {
        foreach(Task task in _allAchivments)
        {
            CheckAchiv(task.NumberAchiv);
        }
    }

    void CheckAchiv(int numAchiv)
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

    void AchivmentOne()
    {
        if(PlayerPrefs.GetInt("BuyCar") == 1)
        {
            for(int i = 0; i < _allAchivments.Count; i++)
            {
                if (_allAchivments[i].NumberAchiv == 1)
                {
                    _allAchivments[i].ChangeCompleted();
                    _allAchivments[i].ChangePersent(100);
                    _allAchivments.RemoveAt(i);
                }
            }
        }
    }

    void AchivmentTwo()
    {
        print(2);
    }

    void AchivmentThree()
    {
        print(3);
    }

    void AchivmentFour()
    {
        print(4);
    }

    void AchivmentFive()
    {
        print(5);
    }

    void AchivmentSix()
    {
        print(6);
    }

    void AchivmentSeven()
    {
        print(7);
    }

    void AchivmentEight()
    {
        print(8);
    }

    void AchivmentNine()
    {
        print(9);
    }

    void AchivmentTen()
    {
        print(10);
    }
}
