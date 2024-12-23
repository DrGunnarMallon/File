using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public enum PlayerState
{
    Idle = 0,
    Running = 1,
    Jumping = 2,
    Falling = 3,
    Dying = 4
}

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private GameObject deathParticleSystem;
    [SerializeField] private TextMeshProUGUI gameOverText;
    // [SerializeField] private GameObject laser;
    // [SerializeField] private Transform laserSpawnPoint;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private BoxCollider2D playerCollider;
    private Animator animator;

    private Vector3 startingScale;
    private bool isAlive = true;
    private bool hasDied = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        startingScale = transform.localScale;
    }

    private void Update()
    {
        if (!isAlive || hasDied)
        {
            return;
        }

        Die();
        UpdateAnimationStates();
        FlipSprite();
    }

    private void FixedUpdate()
    {
        if (!isAlive && hasDied) return;

        Run();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive && hasDied) return;

        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) return;

        if (value.isPressed && IsGrounded())
        {
            AudioManager.Instance.PlayJumpSound();
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }
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
        return Mathf.Abs(rb.linearVelocity.x) > 0.01f;
    }

    private bool IsGrounded()
    {
        return playerCollider.IsTouchingLayers(LayerMask.GetMask("Platform", "Item"));
    }




    private void UpdateAnimationStates()
    {
        if (!isAlive)
        {
            animator.SetInteger("state", (int)PlayerState.Dying);
            return;
        }

        PlayerState state = PlayerState.Idle;

        if (IsGrounded())
        {
            state = PlayerHasHorizontalSpeed() ? PlayerState.Running : PlayerState.Idle;
        }
        else
        {
            state = rb.linearVelocity.y > 0 ? PlayerState.Jumping : PlayerState.Falling;
        }

        animator.SetInteger("state", (int)state);
    }


    private void Die()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")) && isAlive)
        {
            isAlive = false;

            GameManager.Instance.ReduceHearts();
            FindFirstObjectByType<HeartUI>()?.UpdateHearts();

            AudioManager.Instance.PlayDeathSound();

            if (GameManager.Instance.CurrentHearts > 0)
            {
                animator.SetInteger("state", (int)PlayerState.Dying);
                StartCoroutine(RestartLevelAfterDelay(2f));
            }
            else
            {
                StartCoroutine(GameOver());
            }
        }
    }

    private IEnumerator RestartLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(1f);

        gameOverText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        gameOverText.gameObject.SetActive(false);

        GameManager.Instance.ResetGameParameters();

        SceneManager.LoadScene("GameOver");
    }


    public void SpawnDeathParticles()
    {
        Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
    }

}
