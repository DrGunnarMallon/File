using UnityEngine;

public class CollidableItem : MonoBehaviour
{
    private bool hasCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MessageUI messageUI = FindFirstObjectByType<MessageUI>();
            if (hasCollected)
            {
                messageUI.DisplayMessage("Welcome back!\nGood luck getting to the mainframe");
            }
            else
            {
                hasCollected = true;
                GameManager.Instance.IncreaseHearts();
                FindFirstObjectByType<HeartUI>()?.UpdateHearts();
                messageUI.DisplayMessage("Welcome to the game, File.\nHere's an extra life for you <3");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MessageUI messageUI = FindFirstObjectByType<MessageUI>();
            messageUI?.HideMessage();
        }
    }
}
