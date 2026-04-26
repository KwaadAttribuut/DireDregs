using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [Header("UI Text Display")]
    public Image healthDisplay;
    [SerializeField] Sprite[] healthStates;
    public Image ammoDisplay;
    [SerializeField] Sprite[] ammoStates;
    public TMP_Text collectionText;
    public TMP_Text depositText;
    public Image quotaImage;
    public TMP_Text quotaText;

    public GameObject menuCanvas;
    public GameObject playerUI;

    private bool waitingHitStop;

    void Awake()
    {
        //Singleton method
        if (Instance == null)
        {
            //First run, set the instance
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
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
    public void updateHealthUI()
    {
        PlayerHealth healthcount = FindFirstObjectByType<PlayerHealth>();
        if (healthcount != null)
        {
            healthDisplay.sprite = healthStates[(int)healthcount.currentPlayerHealth];
        }
    }

    public void OpenMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerUI.SetActive(menuCanvas.activeSelf);
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            if (menuCanvas.activeSelf == true)
            {
                PauseController.SetPause(true);
                AudioManager.Instance.PauseMusic(true);
            }
            else if (menuCanvas.activeSelf == false)
            {
                PauseController.SetPause(false);
                AudioManager.Instance.PauseMusic(false);
            }
        }
    }

    public void OpenMenuButton()
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

    public void UpdateCollectibleUI()
    {
        if (collectionText != null)
        {
            collectionText.text = $"{GameManager.Instance.collectibleCount}";
        }
        if (depositText != null)
        {
            depositText.text = $"{GameManager.Instance.depositedCollectibleCount}";
        }
        if (quotaText != null)
        {
            quotaText.text = $"{GameManager.Instance.currentQuota}";
            if (GameManager.Instance.collectibleCount + GameManager.Instance.depositedCollectibleCount >= GameManager.Instance.currentQuota)
            {
                quotaImage.color = new Color32(0, 255, 0, 255);
                quotaText.color = new Color32(0, 255, 0, 255);
            }
            else
            {
                quotaImage.color = new Color32(255, 255, 255, 100);
                quotaText.color = new Color32(255, 255, 255, 100);
            }
        }
    }
    public void UpdateAmmoUI()
    {
        if (ammoDisplay != null)
        {
            ammoDisplay.sprite = ammoStates[GameManager.Instance.currentAmmoCount];
        }
    }
}
