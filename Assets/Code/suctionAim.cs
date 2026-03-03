using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class suctionAim : MonoBehaviour
{
    void Start()
    {
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
}
