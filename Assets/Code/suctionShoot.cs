using System.Collections;
using Mono.Cecil.Cil;
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
    [SerializeField] private float downTime, upTime = 0;
    public float pressTime = 0;
    [SerializeField] private float countDown = 2.0f;
    [SerializeField] private bool ready = false;

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
            // StartCoroutine(ShootCoroutine());
            downTime = Time.time;
            pressTime = downTime + countDown;
            ready = true;
        }
        if (context.canceled && !Input.GetMouseButton(0) && ammoCount > 0 && canShoot)
        {
            if (Time.time >= pressTime && ready == true)
            {
                ready = false;
                StartCoroutine(ShootCoroutine());
            }

        }
    }

    private IEnumerator ShootCoroutine()
    {
        canShoot = false;

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
