using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public PlayerMovement playerMovement;
    public Canvas soundMenuCanvas;

    public static bool isPaused;


    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        isPaused = false;
        AudioListener.pause = false;
    }

    public void OpenSoundSettings()
    {
        gameObject.SetActive(false);
        soundMenuCanvas.gameObject.SetActive(true);
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToPauseMenu()
    {
        soundMenuCanvas.gameObject.SetActive(false);
        gameObject.SetActive(true);

    }
}
