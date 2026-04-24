using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour
{
    // UI display elements would be in a seperate UI script in order to avoid the game manager becoming a God Object
    public static GameManager Instance { get; private set; }
    [Header("Health Counter")]
    PlayerHealth healthcount;

    [Header("Weapons and Ammo")]
    public int currentAmmoCount = 0;
    public int maxAmmoCount;
    [Header("Enemies")]
    public int combatPool = 0;

    [Header("Collectibles")]
    public int collectibleCount = 0;
    public int depositedCollectibleCount = 0;

    [Header("Hitstop")]
    private bool waitingHitStop;

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

    // ENEMY SCRIPTS //

    [System.Obsolete]
    public void GlobalRespawn()
    {
        combatPool = 0;
        collectibleCount = 0;
        currentAmmoCount = 3;
        BoundaryManager boundaryManager = FindFirstObjectByType<BoundaryManager>();
        boundaryManager.setToBoundary();
        DespositArea despositArea = FindAnyObjectByType<DespositArea>();
        despositArea.RespawnAllEnemies();
        UIManager.Instance.playerUI.SetActive(true);
        UIManager.Instance.updateHealthUI();
        UIManager.Instance.UpdateAmmoUI();
        UIManager.Instance.UpdateCollectibleUI();
    }

    // SCORE SYSTEM //

    public void AddCollectible(int collectibleAmount)
    {
        collectibleCount += collectibleAmount;
        UIManager.Instance.UpdateCollectibleUI();
    }

    public void DepositCollectibles()
    {
        depositedCollectibleCount += collectibleCount;
        collectibleCount = 0;
        UIManager.Instance.UpdateCollectibleUI();
    }

    // AMMO SYSTEM //

    public void AddAmmo(int ammoAdd)
    {
        if (currentAmmoCount < maxAmmoCount)
        {
            currentAmmoCount += ammoAdd;
            UIManager.Instance.UpdateAmmoUI();
        }
        else
        {
            refuseAmmo();
        }
    }

    public void RemoveAmmo(int ammoRemove)
    {
        if (currentAmmoCount > 0)
        {
            currentAmmoCount -= ammoRemove;
            UIManager.Instance.UpdateAmmoUI();
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
