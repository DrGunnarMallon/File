using UnityEngine;

public class HazardManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealthAndRespawn playerHealth = other.gameObject.GetComponent<PlayerHealthAndRespawn>();

            if (playerHealth != null)
            {
                playerHealth.Die();
            }
        }
    }
}
