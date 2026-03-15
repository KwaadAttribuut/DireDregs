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
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
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
