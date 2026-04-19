using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMasterVolume();
            SetSFXVolume();
            SetMusicVolume();
        }
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        masterMixer.SetFloat("MasterVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        masterMixer.SetFloat("SFXVol", Mathf.Log10(volume)*20);
        masterMixer.SetFloat("UIVol", Mathf.Log10(volume)*20);
        masterMixer.SetFloat("EnemySFXVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        masterMixer.SetFloat("MusicVol", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    private void LoadVolume()
    {
        masterSlider.value=PlayerPrefs.GetFloat("masterVolume");
        sfxSlider.value=PlayerPrefs.GetFloat("sfxVolume");
        musicSlider.value=PlayerPrefs.GetFloat("musicVolume");

        SetMasterVolume();
        SetSFXVolume();
        SetMusicVolume();
    }
}
