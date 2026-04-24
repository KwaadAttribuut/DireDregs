using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer masterMixer;
    [Header("Audio Sources")]
    public AudioSource UISource;
    public AudioSource musicSource;
    public AudioSource BGM1;
    public AudioSource BGM2;
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

    public void CheckScene(string sceneName)
    {
        if (sceneName == "MainMenu")
        {
            if (musicSource != null)
            {
                if (musicSource.clip == null)
                {
                    musicSource.clip = backgroundMusic;
                    musicSource.loop = true;
                    musicSource.Play();
                }
                else if (musicSource.clip == bgmCalm)
                {
                    musicSource.Stop();
                    musicSource.clip = backgroundMusic;
                    musicSource.loop = true;
                    musicSource.Play();
                }
            }
            // if (backgroundMusic != null && musicSource != null)
            // {
            //     if (musicSource.clip = backgroundMusic) return;
            //     else
            //     {
            //         if (BGM1.isPlaying)
            //         {
            //             BGM1.Stop();
            //         }
            //         else if (BGM2.isPlaying)
            //         {
            //             BGM2.Stop();
            //         }
            //         musicSource.clip = backgroundMusic;
            //         musicSource.loop = true;
            //         musicSource.Play();
            //     }
            // }
        }
        else if (sceneName == "Tutorial")
        {
            if (backgroundMusic != null && musicSource != null)
            {
                if (musicSource.clip = backgroundMusic) return;
                else
                {
                    if (BGM1.isPlaying)
                    {
                        BGM1.Stop();
                    }
                    if (BGM2.isPlaying)
                    {
                        BGM2.Stop();
                    }
                    isPlayingBGM1 = true;
                    BGM1.clip = backgroundMusic;
                    musicSource.loop = true;
                    musicSource.Play();
                }
            }
        }
        else if (sceneName == "SampleScene")
        {
            if (musicSource != null)
            {
                if (musicSource.clip == null)
                {
                    musicSource.clip = bgmCalm;
                    musicSource.loop = true;
                    musicSource.Play();
                }
                else if (musicSource.clip == backgroundMusic)
                {
                    musicSource.Stop();
                    musicSource.clip = bgmCalm;
                    musicSource.loop = true;
                    musicSource.Play();
                }
            }
            // if (bgmCalm != null && musicSource != null)
            // {
            //     if (BGM1.clip = bgmCalm) return;
            //     else
            //     {
            //         if (musicSource.isPlaying)
            //         {
            //             musicSource.Stop();
            //         }
            //         BGM1.clip = bgmCalm;
            //         BGM2.clip = bgmCombat;
            //         BGM1.loop = true;
            //         BGM2.loop = true;
            //         BGM1.Play();
            //         BGM2.volume = 0;
            //         BGM2.Play();
            //     }
            // }
        }
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
        if (clip != null && UISource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip != null && musicSource != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void SwapTrack(AudioClip newClip)
    {
        StopAllCoroutines();

        StartCoroutine(FadeTrack());
    }

    private IEnumerator FadeTrack()
    {
        if (isPlayingBGM1)
        {
            float timeToFade = 0.25f;
            float timeElapsed = 0f;
            while (timeElapsed < timeToFade)
            {
                BGM1.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                BGM2.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            float timeToFade = 0.25f;
            float timeElapsed = 0f;
            while (timeElapsed < timeToFade)
            {
                BGM2.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                BGM1.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }

    public void PauseMusic(bool pauseActive)
    {
        if (pauseActive == true) masterMixer.SetFloat("MusicLowpass", 250);
        else if (pauseActive == false) masterMixer.SetFloat("MusicLowpass", 5000);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayEnemySFX(AudioClip clip)
    {
        if (clip != null && enemySource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}
