using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void PauseStart()
    {
        Time.timeScale = 0.0000001f;
        pauseMenu.SetActive(true);
    }

    public void PauseEnd()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Garage Scene");
    }


}
