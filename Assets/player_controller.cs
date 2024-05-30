using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float jumpForce = 7f;
    private Vector2 moveInput;
    private bool isFacingRight = true;

    public Transform groundCheck;
    public float groundCheckRadius = 0.5f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody2D rb;
    public BeamController beamController;
    private Animator animator;

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving {
        get { return _isMoving; }
        private set {
            _isMoving = value;
            if (animator) animator.SetBool("isMoving", value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning {
        get { return _isRunning; }
        set {
            _isRunning = value;
            if (animator) animator.SetBool("isRunning", value);
        }
    }

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        //Debug.Log($"Grounded: {isGrounded}");
        animator.SetBool("IsGrounded", isGrounded);

        
    }
    
    private void FixedUpdate() {
        rb.velocity = new Vector2(moveInput.x * walkSpeed, rb.velocity.y);
        if (moveInput.x != 0) {
            Flip(moveInput.x > 0 ? true : false);
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }

    public void OnRun(InputAction.CallbackContext context) {
        IsRunning = context.performed;
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed && isGrounded) {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            animator.SetTrigger("Jump");
        }
    }

   public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && beamController != null)
        {
            beamController.FireBeam();
            animator.SetTrigger("Shoot");
        }
        else
        {
            Debug.LogWarning("No beam controller assigned to the player.");
        }
    }
    
    private void Flip(bool shouldFaceRight) {
        if (shouldFaceRight != isFacingRight) {
            isFacingRight = shouldFaceRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
