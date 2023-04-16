using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlInstructions : MonoBehaviour
{
    [SerializeField] private GameObject _pcControlPanel;
    [SerializeField] private GameObject _slantControlPanel;
    [SerializeField] private GameObject _swipesControlPanel;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private GameObject _mainCanvas;

    private void Start()
    {

        if (PlayerPrefs.HasKey("SawGuide"))
            Destroy(gameObject);
        else
            StartCoroutine(FadeInCoroutine());

        if(PlayerPrefs.GetString("ControllerType") == "Swipe")
        {
            _swipesControlPanel.SetActive(true);
        }
        if(PlayerPrefs.GetString("ControllerType") == "Slant")
        {
            _slantControlPanel.SetActive(true);
        }
        if((PlayerPrefs.GetString("ControllerType") == "Keyboard"))
        {
            _pcControlPanel.SetActive(true);
        }

        PlayerPrefs.SetInt("SawGuide", 1);
    }


    public void OkayButton()
    {
        Time.timeScale = 1f;
        _mainCanvas.SetActive(true);
        Destroy(gameObject);
    }


    private IEnumerator FadeInCoroutine()
    {
        _mainCanvas.SetActive(false);
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / 0.7f;
            _canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }
        Time.timeScale = 0.0001f;
    }
}
