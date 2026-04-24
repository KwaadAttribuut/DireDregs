using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(SpriteRenderer))]

public class PlayerHealth: MonoBehaviour, iDamageable
{
    public float maxPlayerHealth = 5f;
    [SerializeField] float invulnerabilityDuration = 1f;
    [SerializeField] float blinkInterval = 0.1f;
    public GameObject gameOverPanel;

    public float currentPlayerHealth;
    float invulnerabilityTimer;

    SpriteRenderer sprite;
    float blinkTimer;
    bool blinking;

    Vector2 playerRespawnPoint;

    void Awake()
    {
        playerRespawnPoint = transform.position;
        currentPlayerHealth = maxPlayerHealth;
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(invulnerabilityTimer > 0f)
        {
            invulnerabilityTimer-=Time.deltaTime;
            HandleBlink();
        }
    }
    public bool ApplyDamage(float amount)
    {
        if(currentPlayerHealth <= 0f || invulnerabilityTimer > 0f)
        return false;

        currentPlayerHealth -= amount;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.damageSFX);
        UIManager.Instance.updateHealthUI();
        CameraShakeManager.Instance.Shake(2f, 0.25f);

        if(currentPlayerHealth <= 0)
        {
            Die();
            return true;
        }
        invulnerabilityTimer = invulnerabilityDuration;
        StartBlink(invulnerabilityDuration);
        return true;
    }

    void StartBlink(float duration)
    {
        blinking = true;
        blinkTimer = duration;
    }
    void HandleBlink()
    {
        if(!blinking) return;
        blinkTimer -= Time.deltaTime;
        if(blinkTimer <= 0f)
        {
            blinking = false;
            sprite.enabled = true;
            return;
        }
        sprite.enabled = 
        Mathf.FloorToInt(blinkTimer/blinkInterval) % 2 == 0;
    }

    public void UpdateSpawn(Vector2 playerRespawnPos)
    {
        playerRespawnPoint = playerRespawnPos;
    }

    public void PlayerRespawn()
    {
        transform.position = playerRespawnPoint;
        currentPlayerHealth = maxPlayerHealth;
        SceneLoader.Instance.ResumeGame();
        gameOverPanel.SetActive(false);
        gameObject.SetActive(true);
    }
    void Die()
    {
        SceneLoader.Instance.PauseGame();
        UIManager.Instance.playerUI.SetActive(false);
        gameOverPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}