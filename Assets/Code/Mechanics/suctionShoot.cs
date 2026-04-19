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
    private float timePressed = 0f;
    public float timePressedBuffer = 0f;

    void Start()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }

    // Update is called once per frame
    [Obsolete]
    void Update()
    {
        ammoCount = GameManager.Instance.currentAmmoCount;
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 mouseDistance = mousePos - screenPos;
        float angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        if (PauseController.IsGamePaused)
        {
            if (gameObject.GetComponent<PolygonCollider2D>() != null)
            {
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            }
            if (gameObject.GetComponentInChildren<ParticleSystem>() != null)
            {
                gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = false;
            }
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (PauseController.IsGamePaused)
        {
            return;
        }
        if (context.started && !Input.GetMouseButton(0) && ammoCount > 0 && canShoot)
        {
            timePressed = Time.time;
        }
        if (context.canceled && !Input.GetMouseButton(0) && ammoCount > 0 && canShoot)
        {
            timePressedBuffer = Time.time - timePressed;
            if (timePressedBuffer < 3)
            {
                StartCoroutine(ShootCoroutine());
                CameraShakeManager.Instance.Shake(3.5f * timePressedBuffer, 0.15f * timePressedBuffer);
            }
            else if (timePressedBuffer >= 3)
            {
                StartCoroutine(ShootCoroutine());
                CameraShakeManager.Instance.Shake(10f, 0.45f);
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
        if (PauseController.IsGamePaused)
        {
            return;
        }
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
