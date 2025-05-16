using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider music;
    [SerializeField] Slider sfx;

    public static AudioManager instance;

    private bool updatingSliders = false;
    private const float MIN_VOLUME = 0.0001f; // Para evitar log(0)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (music != null)
            music.onValueChanged.AddListener(UpdateMusicVolume);

        if (sfx != null)
            sfx.onValueChanged.AddListener(UpdateSFXVolume);
    }

    private void Start()
    {
        LoadVolumes();
    }

    private void UpdateMusicVolume(float value)
    {
        if (updatingSliders) return;

        updatingSliders = true;

        float volumeValue = Mathf.Max(value, MIN_VOLUME);
        mixer.SetFloat("VolumeMusic", Mathf.Log10(volumeValue) * 20);
        PlayerPrefs.SetFloat("VolumeMusic", volumeValue);

        updatingSliders = false;
    }

    private void UpdateSFXVolume(float value)
    {
        if (updatingSliders) return;

        updatingSliders = true;

        float volumeValue = Mathf.Max(value, MIN_VOLUME);
        mixer.SetFloat("VolumeSFX", Mathf.Log10(volumeValue) * 20);
        PlayerPrefs.SetFloat("VolumeSFX", volumeValue);

        updatingSliders = false;
    }

    public void LoadVolumes()
    {
        float defaultVolume = 1f;

        float musicVolume = PlayerPrefs.GetFloat("VolumeMusic", defaultVolume);
        float sfxVolume = PlayerPrefs.GetFloat("VolumeSFX", defaultVolume);

        updatingSliders = true;

        if (music != null)
            music.value = musicVolume;

        if (sfx != null)
            sfx.value = sfxVolume;

        updatingSliders = false;

        mixer.SetFloat("VolumeMusic", Mathf.Log10(Mathf.Max(musicVolume, MIN_VOLUME)) * 20);
        mixer.SetFloat("VolumeSFX", Mathf.Log10(Mathf.Max(sfxVolume, MIN_VOLUME)) * 20);
    }
}