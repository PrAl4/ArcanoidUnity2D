using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Menu : MonoBehaviour
{
    public static event Action PauseToSettings;
    public static event Action PauseToGame;

    [SerializeField]
    GameDataScript game_data;

    public void NewGameButton() 
    {
        game_data.Reset();
        SceneManager.LoadScene("MainScene");
    }

    public void ResumeButton() 
    {
        PauseToGame?.Invoke();
    }

    public void SettingsButton() 
    {
        PauseToSettings?.Invoke();
    }

    public void ExitButton() 
    {
        Application.Quit();
    }

}
