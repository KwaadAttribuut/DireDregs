using Mono.Cecil.Cil;
using UnityEngine;

public class suctionShoot : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    [SerializeField] float shotCooldown;
    private float shotCounter;
    private int ammoCount;
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

        if (Input.GetMouseButton(0))
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
        if (Input.GetMouseButton(1) && !Input.GetMouseButton(0) && ammoCount != 0)
        {
            shotCounter -= Time.deltaTime;

            if(shotCounter <= 0)
            {
                shotCounter = shotCooldown;
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                GameManager.Instance.RemoveAmmo(1);
            }
        }   
    }
}
