using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class LosePanelActivation : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;

    private Generator _carGenerator;
    private CarSceneSetter _carSceneSetter;
    private Odometer _odometer;
    private Speedometer _speedometer;
    private Animator _anim;
    private bool _watchedAdv = false;

    private void Start()
    {
        _watchedAdv = false;
        _anim = _losePanel.GetComponent<Animator>();
        _speedometer = FindObjectOfType<Speedometer>();
        _carSceneSetter = FindObjectOfType<CarSceneSetter>();
        _carGenerator = FindObjectOfType<Generator>();
        _odometer = FindObjectOfType<Odometer>();
        EventManager.PlayerDied += ActivateLosePanel;
    }

    private void OnDestroy()
    {
        EventManager.PlayerDied -= ActivateLosePanel;
    }

    private void ActivateLosePanel()
    {
        _speedometer.enabled = false;
        _odometer.IsCounting(false);
        _losePanel.SetActive(true);
        if (_watchedAdv == false)
        {
            _anim.Play("LosePanelAnim");
            _watchedAdv = true;
        }
        else
        {        
            _anim.Play("LosePanelAnim Only Menu");
        } 
        _carGenerator.RemoveAllChildren();
        _carGenerator.gameObject.SetActive(false);
    }

    public void ReturnToGame()
    {
        _speedometer.enabled = true;
        _odometer.IsCounting(true);
        _carSceneSetter.SetAndActivateCar();
        _losePanel.SetActive(false);
        _carGenerator.gameObject.SetActive(true);
    }



}
