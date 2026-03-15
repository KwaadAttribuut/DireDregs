using UnityEngine;
using System.Collections;
using System.Data.Common;
using Unity.VisualScripting;

public class bounceBullet : MonoBehaviour
{
    [Header("Bullet Control")]
    [SerializeField] private float[] moveSpeed;
    [SerializeField] private float damage = 1f;
    private Rigidbody2D rb;
    [SerializeField] float[] damageTimer;
    private bool canDamage = true;
    private float shootHoldTime;

    [Header("Sprite Control")]
    [SerializeField] Sprite[] bulletSprites;

    [System.Obsolete]
    void Start()
    {
        GetComponent<Rigidbody2D>().AddTorque(360, ForceMode2D.Impulse);
        suctionShoot sctnShoot = FindObjectOfType<suctionShoot>();
        if (sctnShoot != null)
        {
            shootHoldTime = sctnShoot.timePressed;
        }
        rb = GetComponent<Rigidbody2D>();
        if (shootHoldTime <= 1)
        {
            rb.linearVelocity = transform.right * moveSpeed[0];
            StartCoroutine(canDamageTimer(damageTimer[0]));
        }
        else if (1 < shootHoldTime && shootHoldTime <= 2)
        {
            rb.linearVelocity = transform.right * moveSpeed[1];
            StartCoroutine(canDamageTimer(damageTimer[1]));
        }
        else if (2 < shootHoldTime)
        {
            rb.linearVelocity = transform.right * moveSpeed[2];
            StartCoroutine(canDamageTimer(damageTimer[2]));
        }
        else
        {
            Debug.Log("Shoot hold value error");
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
