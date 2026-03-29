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

    public Slider healthSlider;

    void Awake()
    {
        currentPlayerHealth = maxPlayerHealth;
        sprite = GetComponent<SpriteRenderer>();

        if(healthSlider != null)
        {
            healthSlider.maxValue = maxPlayerHealth;
            healthSlider.value = currentPlayerHealth;
        }
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
        GameManager.Instance.updateHealthUI();
        CameraShakeManager.Instance.Shake(2f, 0.25f);

        if(healthSlider != null)
            healthSlider.value = currentPlayerHealth;

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
    void Die()
    {
        SceneLoader.Instance.PauseGame();
        gameOverPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}