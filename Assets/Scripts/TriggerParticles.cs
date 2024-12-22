using UnityEngine;

public class TriggerParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem myParticleSystem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            myParticleSystem.Play();
        }
    }
}
