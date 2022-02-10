using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI,GameOverUI;
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        if (!GameOverUI.activeSelf)
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }
    public void NewGame(string name)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(name);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting GAme: ");
        Application.Quit();
    }  
}
