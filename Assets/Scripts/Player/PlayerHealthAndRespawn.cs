using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealthAndRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    [SerializeField] private GameObject deathParticleSystem;
    [SerializeField] private float deathAnimationWait = 0.8f;

    private PlayerMovement playerMovement;
    private Animator animator;
    private bool isRespawning = false;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    public void SpawnDeathParticles()
    {
        if (deathParticleSystem != null)
        {
            Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
        }
    }

    #region Death and Respawn Logic

    public void Die()
    {
        if (!playerMovement.IsAlive || isRespawning)
        {
            return;
        }

        playerMovement.IsAlive = false;
        playerMovement.DisableMovement();

        animator.SetInteger("state", (int)PlayerState.Dying);

        AudioManager.Instance.PlayDeathSound();
        SpawnDeathParticles();

        GameManager.Instance.PlayerDied();

        if (GameManager.Instance.CurrentHearts > 0)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    private IEnumerator RespawnRoutine()
    {
        isRespawning = true;

        yield return new WaitForSeconds(deathAnimationWait);
        yield return StartCoroutine(UIManager.Instance.FadeToBlack());

        Vector3 checkPoint = GameManager.Instance.GetLastCheckpoint();
        transform.position = new Vector2(checkPoint.x, checkPoint.y);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        yield return new WaitForFixedUpdate();

        playerMovement.IsAlive = true;
        playerMovement.EnableMovement();

        yield return StartCoroutine(UIManager.Instance.FadeFromBlack());

        animator.SetInteger("state", (int)PlayerState.Idle);

        isRespawning = false;
    }

    #endregion
}
