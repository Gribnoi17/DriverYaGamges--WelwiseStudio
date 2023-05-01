using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class Odometer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI odomText;
    [SerializeField] private GameObject getterSpeed;
    private float distSpeed;
    private float kilomOfMoney = 100;
    private Speedometer _speedometr;
    private float dist;
    private float kilom;
    private float kilomForMoneyTemp;
    private bool isCarAlive = true;


    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);


    private void Awake()
    {
        kilomForMoneyTemp = kilomOfMoney;
        dist = 0f;
         _speedometr = getterSpeed.GetComponent<Speedometer>();
    }

    private void Start()
    {
        EventManager.PlayerDied += SaveToLeaderboard;
        if (!PlayerPrefs.HasKey("BestMilage"))
        {
            PlayerPrefs.SetInt("BestMilage", 0);
        }
    }


    private void OnDestroy()
    {
        EventManager.PlayerDied -= SaveToLeaderboard;
    }

    public int GetCurrentMilage()
    {
        return (int)Mathf.Round(dist);
    }

    public void SetDistSpeed(float val)
    {
        distSpeed = val;
    }

    public void SetKm4Money(float val)
    {
        kilomOfMoney = val;
    }

    private void SaveToLeaderboard()
    {
        SaveToAchiv();
        if(dist > PlayerPrefs.GetInt("BestMilage"))
        {
            PlayerPrefs.SetInt("BestMilage", (int)dist);
            try
            {
                SetToLeaderboard(PlayerPrefs.GetInt("BestMilage"));
            }
            catch
            {
                print("Лидерборд не сохранил результаты");
            }
        }
    }

    private void SaveToAchiv()
    {
        if(SceneManager.GetActiveScene().name == "NightRoad")
            PlayerPrefs.SetInt("MileageOnFirstLocation", PlayerPrefs.GetInt("MileageOnFirstLocation") + (int)dist);
        else if(SceneManager.GetActiveScene().name == "GreenCity")
            PlayerPrefs.SetInt("MileageOnSecondLocation", PlayerPrefs.GetInt("MileageOnSecondLocation") + (int)dist);

    }

    private void Update()
    {
        if (isCarAlive == false)
            return;
        kilom = _speedometr.GetCurSpeed() * (Time.deltaTime / distSpeed);
        dist += kilom;
        odomText.text = $"{Mathf.Round(dist)} km";
        if (dist > kilomForMoneyTemp)
        {
            kilomForMoneyTemp += kilomOfMoney;
            EventManager.OnDroveKmForMoney();
        }
    }


    public void IsCounting(bool state)
    {
        isCarAlive = state;
    }


}
