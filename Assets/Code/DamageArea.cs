using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [SerializeField] AreaType areaType;
    enum AreaType { Constant, Timed };
    [SerializeField] float damage = 1f;
    [SerializeField] float damageTimer = 3f;
    private bool targetEntered = false;
    private bool timerActive = false;

    void OnTriggerStay2D(Collider2D collision)
    {
            targetEntered = true;
            if (collision.TryGetComponent(out iDamageable damageable))
            {
                if (areaType == AreaType.Constant)
                {
                    damageable.ApplyDamage(damage);
                }
                if (areaType == AreaType.Timed)
                {
                    if (targetEntered == true && timerActive == false)
                    {
                        StartCoroutine(DamageTimer(collision));
                    }
                }
            }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            targetEntered = false;
        }
    }

    private IEnumerator DamageTimer(Collider2D collision)
    {
        collision.TryGetComponent(out iDamageable damageable);
        timerActive = true;
        yield return new WaitForSeconds(damageTimer);
        if(targetEntered == true)
        {
            damageable.ApplyDamage(damage);
        }
        timerActive = false;
    }
}