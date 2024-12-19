using UnityEngine;

public class NPC : MonoBehaviour
{
    [TextArea] public string dialog;
    [SerializeField][TextArea] private string toolTip;
    [SerializeField] private bool hasTooltip = false;

    private TooltipManager tooltipManager;

    private void Awake()
    {
        tooltipManager = FindFirstObjectByType<TooltipManager>();
    }

    public string GetToolTipText()
    {
        return toolTip;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (hasTooltip)
            {
                tooltipManager.ShowTooltip(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tooltipManager.HideTooltip();
        }
    }
}
