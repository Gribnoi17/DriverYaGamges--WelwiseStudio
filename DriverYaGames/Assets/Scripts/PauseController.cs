using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void PauseStart()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void PauseEnd()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
