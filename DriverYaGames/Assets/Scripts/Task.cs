using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Task : MonoBehaviour
{
    [SerializeField] private int _numberAchiv;
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _donePanel;
    [SerializeField] private GameObject _receivePanel;
    [SerializeField] private TextMeshProUGUI _persent;
    [SerializeField] private int _moneyForReward;


    private bool _completed;
    public bool Completed { get { return _completed; } set { _completed = value; } }

    private bool _needReceive;

    public bool NeedReceive { get { return _needReceive; }}

    public int NumberAchiv { get { return _numberAchiv; } }

    public void ChangePersent(int newValueForPercent)
    {
        _persent.text = $"{newValueForPercent}%";
        _slider.value = newValueForPercent / 100f;
    }

    public void ChangeReceivePanel()
    {
        ChangePersent(100);
        _receivePanel.SetActive(true);
        _needReceive= true;
    }

    public int GetCountMoneuForReward()
    {
        return _moneyForReward;
    }

    public void ChangeCompleted()
    {
        _needReceive= false;
        _receivePanel.SetActive(false);
        _completed = true;
        _donePanel.SetActive(true);
        ChangePersent(100);
    }
}
