// using UnityEditor.Callbacks;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    [Header("Launch Settings")]
    public float launchForce = 700f;
    public Vector2 launchDirection;

    public string launchTrigger = "LaunchTrigger";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            Animator playerAnimator = collision.collider.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isJumping", true);
            }

            if (rb != null)
            {
                Debug.Log($"Launching trigger {launchTrigger}");
                animator.SetTrigger(launchTrigger);
                launchDirection = new Vector2(rb.linearVelocity.x, 1);
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.AddForce(launchDirection.normalized * launchForce);
            }
        }
    }
}