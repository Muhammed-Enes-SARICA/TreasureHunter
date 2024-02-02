using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance= this;
        }
        else if (instance!=null) {
            Destroy(instance.gameObject);
            instance = this;
        }
          
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void SetMusicData(float value)
    {
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetFXData(float value)
    {
        PlayerPrefs.SetFloat("FXVolume", value);
    }

    public void ExperienceData(float value)
    {
        PlayerPrefs.SetFloat("Experience", value);
    }
    public void LevelData(int value)
    {
        PlayerPrefs.SetInt("CurrentLevel", value);
    }
    public void ExperienceToNextLevelData(float value)
    {
        PlayerPrefs.SetFloat("ExperienceTNL", value);
    }

    public void CurrentStars(int value)
    {
        PlayerPrefs.SetInt("StarAmount", value);
    }
    public void CurrentCoins(int value)
    {
        PlayerPrefs.SetInt("CoinAmount", value);
    }

    public void Maxhealth(float value)
    {
        PlayerPrefs.SetFloat("MaxHealth", value);
    }
    public void Currenthealth(float value)
    {
        PlayerPrefs.SetFloat("CurrentHealth", value);
    }

    public void LastSavedScene(int value)
    {
        PlayerPrefs.SetInt("LastSavedScene",value);
    }
}

