using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEditor;

public class TooltipManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] float distanceAboveGameObject = 2.2f;

    private GameObject currentTooltip;


    public void ShowTooltip(GameObject targetObject)
    {
        Canvas tooltipCanvas = targetObject.GetComponentInChildren<Canvas>(true);
        if (!tooltipCanvas) return;


        currentTooltip = tooltipCanvas.gameObject;
        currentTooltip.SetActive(true);

        currentTooltip.transform.localPosition = new Vector3(0, distanceAboveGameObject, 0); // Adjust offset if necessary
        currentTooltip.transform.localScale = Vector3.one * 0.02f;

        TextMeshProUGUI tooltipText = currentTooltip.GetComponentInChildren<TextMeshProUGUI>();
        if (tooltipText != null)
        {
            tooltipText.text = targetObject.GetComponent<NPC>().GetToolTipText();
        }
    }


    public void HideTooltip()
    {
        if (currentTooltip == null) return;

        currentTooltip.SetActive(false);
    }
}
