using NUnit.Framework.Constraints;
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

    [Header("Spawn State")]
    private Animator animator;

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

    public GameObject[] lootObj;

    [System.Obsolete]
    void Awake()
    {
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
        if (player != null)
        {
            UpdateTargetDirection();
            SetVelocity();
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
    void Die()
    {
        Instantiate(lootObj[0], transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), transform.rotation);
        Instantiate(lootObj[0], transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), transform.rotation);
        Instantiate(lootObj[0], transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), transform.rotation);
        Instantiate(lootObj[1], transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0), transform.rotation);
        Destroy(gameObject);
    }
}
