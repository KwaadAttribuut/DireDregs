using System.Collections;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // UI display elements would be in a seperate UI script in order to avoid the game manager becoming a God Object
    public static GameManager Instance {get; private set;}
    [Header("Health Counter")]
    PlayerHealth healthcount;
    public TMP_Text healthText;

    [Header("Weapons and Ammo")]
    public int currentAmmoCount = 0;
    [SerializeField] int maxAmmoCount;
    public TMP_Text ammoCounterText;
    public bool isSuctionOn;

    [Header("Collectibles")]
    public int collectibleCount = 0;
    public TMP_Text collectibleText;

    [Header("Hitstop")]
    private bool waitingHitStop;

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
        // collectibleText.text = $"Collectibles: {collectibleCount}"; // lazy line, as mentioned it should be in its own UI script
    }

    void Start()
    {
        UpdateAmmoUI();
        updateHealthUI();
        UpdateCollectibleUI();
        isSuctionOn = false;
    }

    // COLLECTIBLE SYSTEM //

    public void updateHealthUI()
    {
        PlayerHealth healthcount = FindFirstObjectByType<PlayerHealth>();
        healthText.text = $"Health: {healthcount.currentPlayerHealth} / {healthcount.maxPlayerHealth}";
    }
    public void AddCollectible(int collectibleAmount)
    {
        collectibleCount += collectibleAmount;
        UpdateCollectibleUI();
    }
    private void UpdateCollectibleUI()
    {
        if(collectibleText != null)
        {
            collectibleText.text = $"Collection Score: {collectibleCount}";
        }
    }

    // AMMO SYSTEM //

    public void AddAmmo(int ammoAdd)
    {
        if(currentAmmoCount < maxAmmoCount)
        {
            currentAmmoCount += ammoAdd;
            UpdateAmmoUI();
        }
        else
        {
            refuseAmmo();
        }
    }

    public void RemoveAmmo(int ammoRemove)
    {
        if(currentAmmoCount > 0)
        {
            currentAmmoCount -= ammoRemove;
            UpdateAmmoUI();
        }
    }

    private void UpdateAmmoUI()
    {
        if(ammoCounterText != null)
        {
            ammoCounterText.text = $"Stored Ammo: {currentAmmoCount} / {maxAmmoCount}";
        }
    }

    private void refuseAmmo()
    {
        Debug.Log("Ammo Full");
    }

    // HIT STOP //

    public void Stop(float duration)
    {
        if (waitingHitStop)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(WaitForHitstop(duration));
    }

    private IEnumerator WaitForHitstop(float duration)
    {
        waitingHitStop = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waitingHitStop = false;
    }
}
