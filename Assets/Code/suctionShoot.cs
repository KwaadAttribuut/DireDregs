using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class suctionShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    [SerializeField] float shotCooldown;
    bool canShoot = true;
    private int ammoCount;

    // Hold button code 
    public float timePressed = 0f;

    void Start()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ammoCount = GameManager.Instance.currentAmmoCount;
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 mouseDistance = mousePos - screenPos;
        float angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed && !Input.GetMouseButton(0) && ammoCount > 0 && canShoot)
        {
            timePressed = Time.time;
        }
        if (context.canceled && !Input.GetMouseButton(0) && ammoCount > 0 && canShoot)
        {
            timePressed = Time.time - timePressed;
            if (timePressed <= 1)
            {
                StartCoroutine(ShootCoroutine());
                CameraShakeManager.Instance.Shake(0.5f, 0.25f);
            }
            else if (1 < timePressed && timePressed <= 2)
            {
                StartCoroutine(ShootCoroutine());
                CameraShakeManager.Instance.Shake(3f, 0.25f);
            }
            else if (2 < timePressed)
            {
                StartCoroutine(ShootCoroutine());
                CameraShakeManager.Instance.Shake(10f, 0.45f);
            }
            else
            {
                Debug.Log("Shoot hold value error");
            }
        }
    }

    private IEnumerator ShootCoroutine()
    {
        canShoot = false;

        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerShoot);
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        GameManager.Instance.RemoveAmmo(1);

        yield return new WaitForSeconds(shotCooldown);
        canShoot = true;
    }

    public void Vacuum(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (context.canceled)
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

}
