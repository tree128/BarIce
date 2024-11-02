using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider soundEffectVolumeSlider;
    public AudioSource musicAudioSource;
    public AudioSource[] soundEffectAudioSource;

    void Start()
    {
        musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        soundEffectVolumeSlider.onValueChanged.AddListener(ChangeSoundEffectVolume);

        // BGM
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            if(musicAudioSource != null)
            {
                musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
            }
        }
        else
        {
            musicVolumeSlider.value = 0.5f;
            if (musicAudioSource != null)
            {
                musicAudioSource.volume = 0.5f;
            }
        }

        // SE
        if (PlayerPrefs.HasKey("SEVolume"))
        {
            soundEffectVolumeSlider.value = PlayerPrefs.GetFloat("SEVolume");
            if(soundEffectAudioSource != null)
            {
                foreach (AudioSource se in soundEffectAudioSource)
                {
                    se.volume = PlayerPrefs.GetFloat("SEVolume");
                }
            }
        }
        else
        {
            soundEffectVolumeSlider.value = 0.5f;
            if (soundEffectAudioSource != null)
            {
                foreach (AudioSource se in soundEffectAudioSource)
                {
                    se.volume = 0.5f;
                }
            }
        }

    }

    void ChangeMusicVolume(float volume)
    {
        if(musicAudioSource != null)
        {
            musicAudioSource.volume = volume;
        }
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    void ChangeSoundEffectVolume(float volume)
    {
        if(soundEffectAudioSource != null)
        {
            foreach (AudioSource se in soundEffectAudioSource)
            {
                se.volume = volume;
            }
        }
        PlayerPrefs.SetFloat("SEVolume", volume);
    }

    private void OnDestroy()
    {
        PlayerPrefs.Save();
    }
}
