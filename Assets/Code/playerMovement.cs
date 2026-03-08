using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    [Header("Dashing")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float dashCooldown;
    bool isDashing;
    bool canDash = true;
    TrailRenderer trailRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (isDashing)
        {
            return;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if(context.performed && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;

        trailRenderer.emitting = true;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x * dashSpeed, rb.linearVelocity.y * dashSpeed);

        yield return new WaitForSeconds(dashDuration);

        rb.linearVelocity = new Vector2(0f, 0f);

        isDashing = false;
        trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
