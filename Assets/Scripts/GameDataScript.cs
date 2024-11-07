using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Game Data", order = 51)]
public class GameDataScript : ScriptableObject
{
    public bool rest_on_start;
    public int level = 1;
    public int balls = 6;
    public int points = 0;
    public int points_to_ball = 0;
    public bool music = true;
    public bool sound = true;
    public int destroy_count = 0;
    public float music_volume = 0.5f;
    public float sfx_volume = 0.5f;

    public void Reset()
    {
        level = 1;
        balls = 6;
        points = 0;
        points_to_ball = 0;
        destroy_count = 0;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("balls", balls);
        PlayerPrefs.SetInt("points", points);
        PlayerPrefs.SetInt("points_to_ball", points_to_ball);
        PlayerPrefs.SetInt("music", music ? 1 : 0);
        PlayerPrefs.SetInt("sound", sound ? 1 : 0);
        PlayerPrefs.SetInt("destroy_count", destroy_count);
        PlayerPrefs.SetFloat("music_volume", music_volume);
        PlayerPrefs.SetFloat("sfx_volume", sfx_volume);

    }

    public void Load()
    {
        level = PlayerPrefs.GetInt("level", 1);
        balls = PlayerPrefs.GetInt("balls", 6);
        points = PlayerPrefs.GetInt("points", 0);
        points_to_ball = PlayerPrefs.GetInt("points_to_ball", 0);
        music = PlayerPrefs.GetInt("music", 1) == 1;
        sound = PlayerPrefs.GetInt("sound", 1) == 1;
        destroy_count = PlayerPrefs.GetInt("destroy_count", 0);
        music_volume = PlayerPrefs.GetFloat("music_volume", 0.5f);
        sfx_volume = PlayerPrefs.GetFloat("sfx_volume", 0.5f);
    }

    public void PlusBalls(int plus)
    {
        PlayerPrefs.SetInt("balls", balls + plus);
    }

}
