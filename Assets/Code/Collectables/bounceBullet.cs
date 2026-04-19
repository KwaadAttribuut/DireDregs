using UnityEngine;
using System.Collections;
using System;

public class bounceBullet : MonoBehaviour
{
    [Header("Bullet Control")]
    [SerializeField] private float moveSpeed = 8.3f;
    [SerializeField] private float damage = 1f;
    private Rigidbody2D rb;
    private bool canDamage = true;
    private float shootHoldTime;
    [SerializeField] float knockbackForce;
    [SerializeField] float knockbackTime;
    [SerializeField] float stunTime;

    [Header("Sprite Control")]
    [SerializeField] Sprite[] bulletSprites;

    [Obsolete]
    void Start()
    {
        suctionShoot sctnShoot = FindObjectOfType<suctionShoot>();
        if (sctnShoot != null)
        {
            shootHoldTime = sctnShoot.timePressedBuffer;
        }
        rb = GetComponent<Rigidbody2D>();
        if (shootHoldTime < 3)
        {
            rb.linearVelocity = transform.right * (moveSpeed * shootHoldTime);
            StartCoroutine(canDamageTimer(shootHoldTime));
        }
        else if (shootHoldTime >= 3)
        {
            rb.linearVelocity = transform.right * (moveSpeed * 3);
            StartCoroutine(canDamageTimer(3));
        }
    }

    void Update()
    {
        if (canDamage == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprites[0];
        }
        else if (canDamage == false)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprites[1];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && canDamage == true)
        {
            if (collision.gameObject.TryGetComponent(out iDamageable damageable))
            {
                damageable.ApplyDamage(damage);
                if (collision.gameObject.GetComponent<EnemySlime>())
                {
                    collision.gameObject.GetComponent<EnemySlime>().Knockback(transform, knockbackForce, knockbackTime, stunTime);    
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("collectionArea"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.collectibleSFX);
            GameManager.Instance.AddAmmo(1);
            Destroy(gameObject);
        }
    }

    private IEnumerator canDamageTimer(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);
        canDamage = false;
    }
}
