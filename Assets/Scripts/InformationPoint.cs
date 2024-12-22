using UnityEngine;

public class InformationPoint : MonoBehaviour
{
    [SerializeField] private string message;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MessageUI messageUI = FindFirstObjectByType<MessageUI>();
            messageUI.DisplayMessage(message);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MessageUI messageUI = FindFirstObjectByType<MessageUI>();
            messageUI.HideMessage();
        }
    }
}
