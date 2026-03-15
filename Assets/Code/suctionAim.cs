using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class suctionAim : MonoBehaviour
{
    [Obsolete]
    void Start()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = false;
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        Vector2 mouseDistance = mousePos - screenPos;
        float angle = Mathf.Atan2(mouseDistance.y, mouseDistance.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    [Obsolete]
    public void Vacuum(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
            gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = true;
        }
        else if (context.canceled)
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            gameObject.GetComponentInChildren<ParticleSystem>().enableEmission = false;
        }
    }
}
