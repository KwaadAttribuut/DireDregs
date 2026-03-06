using System;
using Unity.VisualScripting;
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

        if (Input.GetMouseButtonDown(0))
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    }

    void Shoot()
    {
        
    }

    /* public void Suction(InputAction.CallbackContext context)
    {
        var action = new InputAction(
        type: InputActionType.Button,
        binding: "mouse0");
        if (context.canceled)
        {
            float exitTime = 3f;
            while(exitTime > 0)
            {
                exitTime -= Time.deltaTime;
            }
            gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        }
    } */ // return to script optimization once main mechanics are working! also figure out how this is meant to work!!!
}
