using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public static event Action SettingsToMain;
    public static event Action SettingsToPause;

    [SerializeField]
    AudioSource musicAudioSource;
    [SerializeField]
    AudioMixer Mixer;
    [SerializeField]
    AudioSource sfxAudioSource;


    [SerializeField]
    GameDataScript game_data;

    [SerializeField]
    Toggle musicToggle;
    [SerializeField]
    Toggle sfxToggle;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider sfxSlider;

    private void OnEnable()
    {
        sfxToggle.isOn = game_data.sound;
        musicToggle.isOn = game_data.music;
        sfxSlider.enabled = game_data.sound;
        musicSlider.enabled = game_data.music;
        sfxSlider.value = game_data.sfx_volume;
        musicSlider.value = game_data.music_volume;
        Mixer.SetFloat("MusicVolume", Mathf.Log10(game_data.music_volume) * 20);
        Mixer.SetFloat("SfxVolume", Mathf.Log10(game_data.sfx_volume) * 20);
    }

    public void sfxToggleChange(bool flag) 
    {
        game_data.sound = flag;
        sfxSlider.enabled = flag;
        if (!flag)
            sfxAudioSource.Stop();
        else
            sfxAudioSource.Play();

    }

    public void musicToggleChange(bool flag)
    {
        game_data.music = flag;
        musicSlider.enabled = flag;
        if (!flag)
            musicAudioSource.Pause();
        else 
            musicAudioSource.Play();

    }

    public void onMusicValueChanged(float value) 
    {
        game_data.music_volume = value;
        Mixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void onSfxValueChanged(float value) 
    {
        game_data.sfx_volume = value;
        Mixer.SetFloat("SfxVolume", Mathf.Log10(value) * 20);
    }

    public void ResumeButton() 
    {
        if (MenusManager.typeOfMenu)
        {
            SettingsToMain?.Invoke();
        }
        else 
        {
            SettingsToPause?.Invoke();
        }
    }

}
