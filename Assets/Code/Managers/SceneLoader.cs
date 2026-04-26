using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    public bool isPaused = false;
    public GameObject playerObject;

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

    // ---------- SCENE LOADING ----------
    public void LoadScene(string sceneName)
    {
        AudioManager.Instance.PauseMusic(false);
        Time.timeScale = 1f;   // ensure unpaused when changing scenes
        isPaused = false;
        SceneManager.LoadScene(sceneName);
    }

    public void NextScene()
    {
        AudioManager.Instance.PauseMusic(false);
        Time.timeScale = 1f;   // ensure unpaused when changing scenes
        isPaused = false;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex +  1);
    }

    [System.Obsolete]
    public void ReloadScene()
    {
        Time.timeScale = 1f;
        PauseController.SetPause(false);
        isPaused = false;
        DespositArea despositArea = FindAnyObjectByType<DespositArea>();
        despositArea.ResetPlayerSpawn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GameManager.Instance.GlobalRespawn();

    }

    [System.Obsolete]
    public void RespawnPlayer()
    {
        playerObject.SetActive(true);
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.PlayerRespawn();
        GameManager.Instance.GlobalRespawn();
    }

    // ---------- PAUSE ----------
    public void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        PauseController.SetPause(true);
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        PauseController.SetPause(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    // ---------- QUIT ----------
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
