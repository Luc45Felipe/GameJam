using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneCtrl
{
    public static void Pause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
    }

    public static void ExitGame()
    {
        Application.Quit();
    }

    public static void NextScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);
    }

    public static void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
