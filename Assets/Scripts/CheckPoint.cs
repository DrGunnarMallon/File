using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private int checkPointNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.SetCheckpointReached(checkPointNumber);
            if (GameManager.Instance.GetLastCheckpoint() < checkPointNumber)
            {
                MessageUI messageUI = FindFirstObjectByType<MessageUI>();
                messageUI.DisplayMessage("Checkpoint reached!");
            }
            {
                GameManager.Instance.ReduceHearts();
            }
        }
    }
}
