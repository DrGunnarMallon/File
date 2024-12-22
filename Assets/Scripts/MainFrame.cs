using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainFrame : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;
    [SerializeField] private CanvasGroup fadeCanvasGroup;
    [SerializeField] private float fadeDuration = 1.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PlayParticleEffect());
        }
    }

    private IEnumerator PlayParticleEffect()
    {
        // Play particle effect
        particleEffect.Play();
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene("WinScene");
    }

    private IEnumerator FadeOut()
    {
        float timer = 0;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0, 1, timer / fadeDuration);
            yield return null;
        }

        fadeCanvasGroup.alpha = 1f;
    }
}
