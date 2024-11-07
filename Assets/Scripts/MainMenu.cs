using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public static Action MainToSettings;

    public static bool gameFinished = false;


    [SerializeField]
    GameObject game;

    public void PlayButton()
    {
       gameObject.SetActive(false);
       game.SetActive(true);
       MenusManager.typeOfMenu = false;
    }

    public void SettingsButton() 
    {
        MainToSettings?.Invoke();
    }

    public void ExitButton() 
    {
        Application.Quit();
    }

}
    
