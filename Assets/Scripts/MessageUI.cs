using UnityEngine;
using System.Collections;
using TMPro;

public class MessageUI : MonoBehaviour
{
    private float moveTime = 0.5f; // Time to move the panel

    private Vector2 hiddenPosition = new Vector2(0, 40); // Original position
    private Vector2 visiblePosition = new Vector2(0, -40); // Target position

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void DisplayMessage(string message)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = message;
        StartCoroutine(ShowPanel());
    }

    public void HideMessage()
    {
        StartCoroutine(HidePanel());
    }

    private IEnumerator ShowPanel()
    {
        yield return MovePanel(hiddenPosition, visiblePosition, moveTime);
    }

    private IEnumerator HidePanel()
    {
        yield return MovePanel(visiblePosition, hiddenPosition, moveTime);
    }

    private IEnumerator MovePanel(Vector2 start, Vector2 end, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            rectTransform.anchoredPosition = Vector2.Lerp(start, end, t);
            yield return null; // Wait for the next frame
        }

        rectTransform.anchoredPosition = end; // Ensure it snaps precisely to the end
    }
}
