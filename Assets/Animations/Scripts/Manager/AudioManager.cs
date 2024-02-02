using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public AudioMixer musicMixer, effectMixer;
    public AudioSource backgroundMusicAS;
    public static AudioManager instance;

    public Slider masterSldr, effectSldr;

    public Image musicHandle,effectHandle;
    public Sprite soundOf, soundOn;

    [Range(-80,20)]
    public float effectVol, masterVol;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void MasterVolume()
    {
        DataManager.instance.SetMusicData(masterSldr.value);
        musicMixer.SetFloat("masterVolume",PlayerPrefs.GetFloat("MusicVolume"));
    }
    public void EffectVolume()
    {
        DataManager.instance.SetFXData(effectSldr.value);
        effectMixer.SetFloat("effectVolume",PlayerPrefs.GetFloat("FXVolume"));
    }

    void Start()
    {
        PlayAudio(backgroundMusicAS);

       // masterSldr.value = masterVol;
       // effectSldr.value = effectVol;

        masterSldr.minValue = -80;
        masterSldr.maxValue = 20;

        effectSldr.minValue = -80;
        effectSldr.maxValue = 20;

        masterSldr.value = PlayerPrefs.GetFloat("MusicVolume", 0f);
        effectSldr.value = PlayerPrefs.GetFloat("FXVolume", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //MasterVolume();
        //EffectVolume();

        if(masterSldr.value <= -70)
        {
            musicHandle.sprite = soundOf;
        }else 
        { musicHandle.sprite = soundOn; }


        if (effectSldr.value <= -70)
        {
            effectHandle.sprite = soundOf;
        }
        else
        { effectHandle.sprite = soundOn; }

    }

    public void PlayAudio(AudioSource audio)
    {
        audio.Play();
    }
}
