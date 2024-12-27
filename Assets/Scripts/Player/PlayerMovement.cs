using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float runSpeed = 5f;

    [Header("Status Flags")]
    public bool IsAlive = true;
    private bool hasDied = false;

    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private PlayerAnimationController animationController;

    private Vector2 moveInput;
    private Vector3 originalScale;

    #region Basic Setup

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        animationController = GetComponent<PlayerAnimationController>();
    }

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (!IsAlive || hasDied)
        {
            return;
        }

        bool grounded = IsGrounded();
        bool horizontalSpeed = Math.Abs(rb.linearVelocityX) > 0.01f;
        float verticalSpeed = rb.linearVelocityY;

        animationController.UpdateAnimation(IsAlive, grounded, verticalSpeed, horizontalSpeed);

        FlipSpriteBasedOnMovement();
    }

    private void FixedUpdate()
    {
        if (!IsAlive || hasDied) return;

        Run();
    }

    #endregion

    #region Input Methods

    public void OnMove(InputValue value)
    {
        if (!IsAlive || hasDied) return;

        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        if (!IsAlive || hasDied) return;

        Debug.Log(IsGrounded());

        if (value.isPressed && IsGrounded())
        {
            AudioManager.Instance.PlayJumpSound();
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpSpeed);
        }
    }

    #endregion

    #region Movement Logic

    private void Run()
    {
        rb.linearVelocity = new Vector2(moveInput.x * runSpeed, rb.linearVelocityY);
    }

    private void FlipSpriteBasedOnMovement()
    {
        if (Math.Abs(rb.linearVelocityX) > 0.01f)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(rb.linearVelocityX) * Math.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }
    }

    private bool IsGrounded()
    {
        return playerCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
    }

    #endregion

    #region Public Methods

    public void DisableMovement()
    {
        hasDied = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        moveInput = Vector2.zero;
    }

    public void EnableMovement()
    {
        hasDied = false;
    }

    #endregion
}
