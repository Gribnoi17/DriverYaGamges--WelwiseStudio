using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GarageSetUp : MonoBehaviour
{
    [SerializeField] private List<GameObject> _choiceMarks;
    [Tooltip("������ ���� ������������ � ����� �� �������, ��� � ������� � ������")]

    private Location _chosenLocation = Location.NightRoad;

    public enum Location
    {
        NightRoad,
        GreenCity,
        ToxicZone
    }

    public void ChooseNightRoad()
    {
        _chosenLocation = Location.NightRoad;
        foreach(GameObject mark in _choiceMarks)
        {
            mark.SetActive(false);
        }
        _choiceMarks[0].SetActive(true);
    }

    public void ChooseGreenCity()
    {
        if (PlayerPrefs.GetString("GreenCity") != "Unlocked")
            return;
        _chosenLocation = Location.GreenCity;
        foreach (GameObject mark in _choiceMarks)
        {
            mark.SetActive(false);
        }
        _choiceMarks[1].SetActive(true);
    }

    public void ChooseToxicZone()
    {
        if (PlayerPrefs.GetString("ToxicZone") != "Unlocked")
            return;
        _chosenLocation = Location.ToxicZone;
        foreach (GameObject mark in _choiceMarks)
        {
            mark.SetActive(false);
        }
        _choiceMarks[2].SetActive(true);
    }


    public void LoadChosenScene()
    {
        DOTween.Clear();
        SceneManager.LoadScene(_chosenLocation.ToString());
    }

    public void LoadFreeRace()
    {
        PlayerPrefs.SetInt("RegimeRace", 0);
    }

    public void LoadTimeRace()
    {
        PlayerPrefs.SetInt("RegimeRace", 1);

        //{ 0, "Eazy" },
        //{ 1, "Normal"},
        //{ 2, "Hard"} - ������� ����� ��������� �� PlayerPrefs

    }


    void Update()
    {
        
    }
}
