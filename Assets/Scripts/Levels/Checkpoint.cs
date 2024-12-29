using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.GetLastCheckpoint() == transform.position) return;

        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckpoint(transform.position);
            StartCoroutine(UIManager.Instance.DisplayMessage("Checkpoint unlocked"));
        }
    }
}
