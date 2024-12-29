using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainFrame : MonoBehaviour
{
    [SerializeField] ParticleSystem particleEffect;

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
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(UIManager.Instance.FadeToBlack());
        SceneManager.LoadScene("WinScene");
    }
}
