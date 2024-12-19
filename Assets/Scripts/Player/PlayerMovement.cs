using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float runSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private BoxCollider2D playerCollider;
    private GravityControl gravityControl;
    private Animator animator;

    private Vector3 startingScale;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        gravityControl = GetComponent<GravityControl>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        startingScale = transform.localScale;
    }

    private void Update()
    {
        Run();
        FlipSprite();
        UpdateAnimationStates();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnAttack(InputValue value)
    {
        if (!value.isPressed) return;

        gravityControl.reversePolarity();
    }


    void OnJump(InputValue value)
    {
        if (!value.isPressed || !IsGrounded()) return;

        rb.linearVelocity += new Vector2(0f, jumpSpeed);
    }


    void Run()
    {
        rb.linearVelocity = new Vector2(moveInput.x * runSpeed, rb.linearVelocity.y);
    }

    void FlipSprite()
    {
        if (PlayerHasHorizontalSpeed())
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x) * startingScale.x, startingScale.y);
        }

    }

    private bool PlayerHasHorizontalSpeed()
    {
        return Mathf.Abs(rb.linearVelocity.x) > Mathf.Epsilon;
    }

    private bool IsGrounded()
    {
        return playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Item"));
    }

    private void UpdateAnimationStates()
    {
        if (IsGrounded())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
            animator.SetBool("isRunning", PlayerHasHorizontalSpeed());
        }
        else
        {
            if (rb.linearVelocity.y < Mathf.Epsilon)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
            }
            else
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }
        }
    }

}
