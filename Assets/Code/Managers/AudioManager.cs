using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance {get; private set;}
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer masterMixer;
    [Header("Audio Sources")]
    public AudioSource UISource;
    public AudioSource musicSource;
    private bool isPlayingBGM1;
    public AudioSource sfxSource;
    public AudioSource enemySource;
    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip bgmCalm;
    public AudioClip bgmCombat;
    public AudioClip UISelect;
    public AudioClip playerShoot;
    public AudioClip playerDash;
    public AudioClip damageSFX;
    public AudioClip collectibleSFX;

    void Awake()
    {
        if (Instance == null)
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

    public void MenuMusic()
    {
        
    }
    public void StartMusic()
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

    public void SwapTrack(AudioClip newClip)
    {
        
    }

    public void PauseMusic(bool pauseActive)
    {
        if (pauseActive == true) masterMixer.SetFloat("MusicLowpass", 250);
        else if (pauseActive == false) masterMixer.SetFloat("MusicLowpass", 5000);
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
