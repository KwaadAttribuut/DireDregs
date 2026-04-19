using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [Header("UI Text Display")]
    public TMP_Text healthText;
    public TMP_Text ammoCounterText;

    public TMP_Text collectibleText;
    public GameObject menuCanvas;
    public GameObject playerUI;

    private bool waitingHitStop;

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
        menuCanvas.SetActive(false);
        UpdateAmmoUI();
        updateHealthUI();
        UpdateCollectibleUI();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void updateHealthUI()
    {
        PlayerHealth healthcount = FindFirstObjectByType<PlayerHealth>();
        healthText.text = $"Health: {healthcount.currentPlayerHealth} / {healthcount.maxPlayerHealth}";
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerUI.SetActive(menuCanvas.activeSelf);
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            if (menuCanvas.activeSelf == true)
            {
                AudioManager.Instance.PauseMusic(true);
            }
            else if (menuCanvas.activeSelf == false)
            {
                AudioManager.Instance.PauseMusic(false);
            }
        }
    }

    public void UpdateCollectibleUI()
    {
        if (collectibleText != null)
        {
            collectibleText.text = $"Collection Score: {GameManager.Instance.collectibleCount} ({GameManager.Instance.depositedCollectibleCount})";
        }
    }
    public void UpdateAmmoUI()
    {
        if (ammoCounterText != null)
        {
            ammoCounterText.text = $"Stored Ammo: {GameManager.Instance.currentAmmoCount} / {GameManager.Instance.maxAmmoCount}";
        }
    }
}
