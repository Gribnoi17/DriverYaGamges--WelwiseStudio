using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GarageSetUp : MonoBehaviour
{
    [SerializeField] private List<GameObject> _choiceMarks;
    [Tooltip("������ ���� ������������ � ����� �� �������, ��� � ������� � ������")]

    private Location _chosenLocation;

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
        _chosenLocation = Location.GreenCity;
        foreach (GameObject mark in _choiceMarks)
        {
            mark.SetActive(false);
        }
        _choiceMarks[1].SetActive(true);
    }

    public void ChooseToxicZone()
    {
        _chosenLocation = Location.ToxicZone;
        foreach (GameObject mark in _choiceMarks)
        {
            mark.SetActive(false);
        }
        _choiceMarks[2].SetActive(true);
    }


    public void LoadChosenScene()
    {
        SceneManager.LoadScene(_chosenLocation.ToString());
    }

    public void LoadFreeRace()
    {
        //PlayerPrefs.SetInt("")
    }

    public void LoadTimeRace()
    {

    }


    void Update()
    {
        
    }
}
