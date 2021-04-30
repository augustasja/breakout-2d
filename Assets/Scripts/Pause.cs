using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool Paused = false;
    public GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (Paused)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            PauseMenu(true);
        }
        else
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            PauseMenu(false);
        }
    }

    public void PauseMenu(bool active)
    {
        if (active)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }
}
