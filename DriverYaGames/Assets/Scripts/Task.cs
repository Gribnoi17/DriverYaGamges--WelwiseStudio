using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] private int _numberAchiv;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _donePanel;
    [SerializeField] private TextMeshProUGUI _persent;
    private bool _completed = false;
    public bool Completed { get { return _completed; }}

    public int NumberAchiv { get { return _numberAchiv; } }

    public void ChangePersent(int newValueForPercent)
    {
        _persent.text = $"{newValueForPercent}%";
        _slider.value = newValueForPercent / 100;
    }

    public void ChangeCompleted()
    {
        _completed = true;
        _donePanel.SetActive(true);
    }
}
