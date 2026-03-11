using UnityEngine;
using System.Collections;
using System.Data.Common;
using Unity.VisualScripting;

public class bounceBullet : MonoBehaviour
{
    [Header("Bullet Control")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float damage = 1f;
    private Rigidbody2D rb;
    [SerializeField] float damageTimer;
    private bool canDamage = true;

    [Header("Sprite Control")]
    [SerializeField] Sprite[] bulletSprites;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * moveSpeed;
        StartCoroutine(canDamageTimer(damageTimer));
    }
    
    void Update()
    {
        if(canDamage == true)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprites[0];
        }
        else if(canDamage == false)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprites[1];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player") && canDamage == true)
        {
            if(collision.gameObject.TryGetComponent(out iDamageable damageable))
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
        canDamage =  false;
    }
}
