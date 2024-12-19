using System.Collections;
using UnityEngine;

public class FadeInAndOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        Debug.Log("Enable called");
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        while (canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.deltaTime;
            yield return null;
        }
    }
}
