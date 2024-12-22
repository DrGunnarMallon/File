using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerHealth), typeof(PlayerAnimation))]
public class PlayerController : MonoBehaviour
{
    // [SerializeField] private float jumpSpeed = 5f;
    // [SerializeField] private float runSpeed = 5f;

    // private Rigidbody2D rb;
    // private Vector2 moveInput;
    // private BoxCollider2D playerCollider;

    // private PlayerHealth health;
    // private PlayerAnimation animationHandler;

    // private bool isGrounded => playerCollider.IsTouchingLayers(LayerMask.GetMask("Platform", "Item"));

    // private void Awake()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     playerCollider = GetComponent<BoxCollider2D>();
    //     health = GetComponent<PlayerHealth>();
    //     animationHandler = GetComponent<PlayerAnimation>();
    // }

    // private void Update()
    // {
    //     if (!health.IsAlive) return;

    //     HandleAnimation();
    //     FlipSprite();
    // }

    // private void FixedUpdate()
    // {
    //     if (!health.IsAlive) return;
    //     Run();
    // }

    // void OnMove(InputValue value)
    // {
    //     moveInput = value.Get<Vector2>();
    // }

    // void OnJump(InputValue value)
    // {
    //     if (value.isPressed && isGrounded)
    //     {
    //         rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
    //     }
    // }

    // private void Run()
    // {
    //     rb.linearVelocity = new Vector2(moveInput.x * runSpeed, rb.linearVelocity.y);
    // }

    // private void FlipSprite()
    // {
    //     if (Mathf.Abs(rb.linearVelocity.x) > 0.01f)
    //     {
    //         transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocity.x), 1);
    //     }
    // }

    // private void HandleAnimation()
    // {
    //     animationHandler.UpdateState(
    //         isGrounded,
    //         rb.linearVelocity.y,
    //         Mathf.Abs(rb.linearVelocity.x) > 0.01f,
    //         health.IsAlive
    //     );
    // }
}