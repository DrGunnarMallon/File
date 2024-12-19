using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().EnableGravityFlip();
            Destroy(gameObject);
        }
    }
}
