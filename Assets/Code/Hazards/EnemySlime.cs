using System.Collections;
using UnityEngine;

public class EnemySlime : MonoBehaviour, iDamageable
{
    private Rigidbody2D rb;
    private Transform player;
    Vector2 moveDirection;

    [Header("Stats")]
    [SerializeField] private float maxHealth = 2f;
    private float currentHealth;
    [SerializeField] private float enemyDamage = 1;
    private float rngLoot = 0;
    [SerializeField] private ParticleSystem damageParticles;
    private ParticleSystem damageParticlesInstance;

    [Header("Spawn State")]
    private Animator animator;
    Vector2 respawnPosition;

    [Header("Invulnerability")]
    [SerializeField] float invulnerabilityDuration = 1f;
    [SerializeField] float blinkInterval = 0.1f;
    float invulnerabilityTimer;
    SpriteRenderer sprite;
    float blinkTimer;
    bool blinking;

    [Header("Movement")]
    public bool AwareOfPlayer { get; private set; }
    public Vector2 DirectionToPlayer { get; private set; }
    [SerializeField] private float _playerAwarenessDistance;
    [SerializeField] private float enemySpeed;
    private Vector2 targetDirection;
    private bool knockedBack = false;

    public GameObject[] lootObj;

    [System.Obsolete]
    void Awake()
    {
        respawnPosition = transform.position;
        currentHealth = maxHealth;
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            player = playerHealth.transform;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseController.IsGamePaused)
        {
            return;
        }
        if (player != null)
        {
            if (invulnerabilityTimer > 0f)
            {
                invulnerabilityTimer -= Time.deltaTime;
                HandleBlink();
            }
            Vector2 enemyToPlayerVector = player.position - transform.position;
            DirectionToPlayer = enemyToPlayerVector.normalized;
            if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
            {
                AwareOfPlayer = true;
                animator.SetBool("isHostile", true);
            }
            else
            {
                AwareOfPlayer = false;
                animator.SetBool("isHostile", false);
            }
        }
    }

    void FixedUpdate()
    {
        if (PauseController.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            return;
        }
        if (player != null && knockedBack == false)
        {
            UpdateTargetDirection();
            SetVelocity();
        }
        else if (knockedBack == true)
        {
            animator.SetBool("isMoving", false);
            return;
        }
    }

    private void UpdateTargetDirection()
    {
        if (AwareOfPlayer)
        {
            targetDirection = DirectionToPlayer;
        }
        else
        {
            targetDirection = Vector2.zero;
        }
    }

    private void SetVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
        else
        {
            rb.linearVelocity = new Vector2(targetDirection.x, targetDirection.y) * enemySpeed;
            animator.SetBool("isMoving", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PauseController.IsGamePaused)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out iDamageable damageable))
            {
                damageable.ApplyDamage(enemyDamage);
            }
        }
    }

    public bool ApplyDamage(float amount)
    {
        if (currentHealth <= 0f || invulnerabilityTimer > 0f)
            return false;
        damageParticle();
        currentHealth -= amount;
        AudioManager.Instance.PlayEnemySFX(AudioManager.Instance.damageSFX);
        GameManager.Instance.Stop(0.15f);

        if (currentHealth <= 0)
        {
            Die();
            return true;
        }
        invulnerabilityTimer = invulnerabilityDuration;
        StartBlink(invulnerabilityDuration);
        return true;
    }

    private void damageParticle()
    {
        damageParticlesInstance = Instantiate(damageParticles, transform.position, Quaternion.identity);
    }

    void StartBlink(float duration)
    {
        blinking = true;
        blinkTimer = duration;
    }
    void HandleBlink()
    {
        if (!blinking) return;
        blinkTimer -= Time.deltaTime;
        if (blinkTimer <= 0f)
        {
            blinking = false;
            sprite.enabled = true;
            return;
        }
        sprite.enabled =
        Mathf.FloorToInt(blinkTimer / blinkInterval) % 2 == 0;
    }
    public void Knockback(Transform bulletTransform, float knockbackForce, float knockbackTime, float stunTime)
    {
        if (gameObject.activeSelf == false) return;
        knockedBack = true;
        StartCoroutine(EnemyStunTimer(knockbackTime, stunTime));
        Vector2 knockbackDirection = (transform.position - bulletTransform.position).normalized;
        rb.linearVelocity = knockbackDirection * knockbackForce;
    }

    IEnumerator EnemyStunTimer(float knockbackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockbackTime);
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        knockedBack = false;
    }

    public void RespawnEnemies()
    {
        transform.position = respawnPosition;
        currentHealth = maxHealth;
        gameObject.SetActive(true);
    }

    void Die()
    {
        for (int i = 0; i < 3; i++)
        {
            rngLoot = Random.Range(0f, 1f);
            if (rngLoot < 0.75f)
            {
                Instantiate(lootObj[0], transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), transform.rotation);
            }
            else if (rngLoot >= 0.75f)
            {
                Instantiate(lootObj[1], transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), transform.rotation);
            }
        }
        gameObject.SetActive(false);
    }
}
