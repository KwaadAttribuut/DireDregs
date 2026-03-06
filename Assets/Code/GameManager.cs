using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // UI display elements would be in a seperate UI script in order to avoid the game manager becoming a God Object
    public static GameManager Instance {get; private set;}
    public int collectibleCount = 0;
    public TMP_Text collectibleText;
    public int currentAmmoCount = 0;
    [SerializeField] int maxAmmoCount;
    public TMP_Text ammoCounterText;
    public bool isSuctionOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        isSuctionOn = false;
    }

    public void AddCollectible()
    {
        collectibleCount++;
        UpdateCollectibleUI();
    }
    private void UpdateCollectibleUI()
    {
        if(collectibleText != null)
        {
            collectibleText.text = $"Collectibles: {collectibleCount}";
        }
    }

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
}
