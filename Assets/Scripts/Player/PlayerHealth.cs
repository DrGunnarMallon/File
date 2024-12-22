using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    // [SerializeField] private GameObject deathParticleSystem;

    // public bool IsAlive { get; private set; } = true;

    // private PlayerAnimation animationHandler;

    // private void Awake()
    // {
    //     animationHandler = GetComponent<PlayerAnimation>();
    // }

    // public void TakeDamage()
    // {
    //     if (!IsAlive) return;

    //     GameManager.Instance.ReduceHearts();
    //     IsAlive = false;
    //     animationHandler.TriggerDeathAnimation();

    //     if (GameManager.Instance.CurrentHearts > 0)
    //     {
    //         StartCoroutine(RestartLevelAfterDelay(1f));
    //     }
    //     else
    //     {
    //         Debug.Log("Game Over");
    //     }
    // }

    // private IEnumerator RestartLevelAfterDelay(float delay)
    // {
    //     yield return new WaitForSeconds(delay);
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }

    // public void SpawnDeathParticles()
    // {
    //     Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
    // }
}