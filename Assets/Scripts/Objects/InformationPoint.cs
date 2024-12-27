using UnityEngine;

public class InformationPoint : MonoBehaviour
{
    [SerializeField] private string message;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(UIManager.Instance.DisplayMessage(message));
        }
    }
}
