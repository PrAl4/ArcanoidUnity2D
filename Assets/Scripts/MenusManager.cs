using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class MenusManager : MonoBehaviour
{
    static bool gameWasStarted = false;

    static bool gameWasFinished = false;

    public static bool typeOfMenu = true; // true - главное

    [SerializeField]
    GameObject game;
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject exitButton;
    [SerializeField]
    GameObject settingsMenu;
    [SerializeField]
    GameObject mainMenuButtons;
    [SerializeField]
    GameObject pauseMenu;

    bool pauseMenuOpened = false;
    bool settingsMenuOpened = false;

    private void OnEnable()
    {
        MainMenu.MainToSettings += FromMainToSettings;
        SettingsMenu.SettingsToMain += FromSettingsToMain;
        Pause_Menu.PauseToSettings += FromPauseToSettings;
        Pause_Menu.PauseToGame += ClosePauseMenu;
        SettingsMenu.SettingsToPause += FromSettingsToPause;
        // что-то.что-то += ActivateExitButtonInMainMenu;
        // что-то.что-то += ActivateMainMenu;
    }

    private void OnDisable()
    {
        MainMenu.MainToSettings -= FromMainToSettings;
        SettingsMenu.SettingsToMain -= FromSettingsToMain;
        Pause_Menu.PauseToSettings -= FromPauseToSettings;
        Pause_Menu.PauseToGame -= ClosePauseMenu;
        SettingsMenu.SettingsToPause -= FromSettingsToPause;
        // что-то.что-то -= ActivateExitButtonInMainMenu;
        // что-то.что-то -= ActivateMainMenu;
    }

    void Start()
    {
        if (!gameWasStarted) 
        {
            gameWasStarted = true;
            game.SetActive(false);
            mainMenu.SetActive(true);
        }

        if (gameWasFinished) 
        {
            ActivateExitButtonInMainMenu();
        }
    }

    private void Update()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.P) && !typeOfMenu && !pauseMenuOpened && !settingsMenuOpened) // кнопочка P
        {
            OpenPauseMenu();
        }
        else if (UnityEngine.Input.GetKeyDown(KeyCode.P) && !typeOfMenu && pauseMenuOpened && !settingsMenuOpened) 
        {
            ClosePauseMenu();
        }
    }

    void OpenPauseMenu() 
    {
        pauseMenu.SetActive(true);
        pauseMenuOpened = true;
        Time.timeScale = 0;
        Cursor.visible = true;
    }

    void ClosePauseMenu() 
    {
        pauseMenu.SetActive(false);
        pauseMenuOpened = false;
        Time.timeScale = 1f;
        Cursor.visible = false;
    }

    void FromMainToSettings() 
    {
        settingsMenu.SetActive(true);
        mainMenuButtons.SetActive(false);
    }

    void FromSettingsToMain() 
    {
        settingsMenu.SetActive(false);
        mainMenuButtons.SetActive(true);
    }

    void FromPauseToSettings() 
    {
        pauseMenu.SetActive(false);
        settingsMenuOpened = true;
        settingsMenu.SetActive(true);
    }

    void FromSettingsToPause() 
    {
        pauseMenu.SetActive(true);
        settingsMenuOpened = false;
        settingsMenu.SetActive(false);
    }

    void ActivateExitButtonInMainMenu() 
    {
        exitButton.SetActive(true);
        gameWasFinished = true;
    }

    void ActivateMainMenu() 
    {
        game.SetActive(false);
        mainMenu.SetActive(true);
        typeOfMenu = true;
    }


}
