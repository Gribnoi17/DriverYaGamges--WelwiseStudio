using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private MusicController _musicController;


    private void Start()
    {
        _musicController = FindObjectOfType<MusicController>();
    }

    public void PauseStart()
    {
        try
        {
            StartCoroutine(_musicController.FindSliders());
        }
        catch
        {
            print("Слайдеры не найдены, PauseController");
        }
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0000001f;
    }

    public void OnlyFindObjects()
    {
        StartCoroutine(_musicController.FindSliders());
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void PauseEnd()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void LoadMenu()
    {
        PlayerPrefs.SetInt("DeatCount", PlayerPrefs.GetInt("DeatCount") + 1);
        SceneManager.LoadScene("Garage Scene");
    }


}
