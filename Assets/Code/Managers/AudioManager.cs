using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}
    [Header("Audio Sources")]
    public AudioSource UISource;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource enemySource;
    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip UISelect;
    public AudioClip playerShoot;
    public AudioClip playerDash;
    public AudioClip damageSFX;
    public AudioClip collectibleSFX;

    void Awake()
    {
        //Singleton method
        if (Instance == null) {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this) {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(Instance.gameObject);
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    void Start()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayUI(AudioClip clip)
    {
        if(clip != null && UISource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if(clip != null && musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if(clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayEnemySFX(AudioClip clip)
    {
        if(clip != null && enemySource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
