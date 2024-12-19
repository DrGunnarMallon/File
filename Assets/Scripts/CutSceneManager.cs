using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // If using TextMeshPro
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    [System.Serializable]
    public class IntroSlide
    {
        public GameObject slideObject;
        public Image backgroundImage;
        public TextMeshProUGUI text;
    }

    public IntroSlide[] slides;
    public float fadeInDuration = 2.0f;
    public float holdDuration = 10.0f;
    public float fadeOutDuration = 1.0f;
    public float transitionTime = 1.0f;
    public float letterDelay = 0.05f;
    public float initialDelay = 1.0f;
    public float finalDelay = 0.7f;


    private void Start()
    {
        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(initialDelay);

        foreach (var slide in slides)
        {
            slide.slideObject.SetActive(true);

            yield return StartCoroutine(FadeIn(slide.backgroundImage, slide.text));

            slide.text.gameObject.SetActive(true);

            yield return StartCoroutine(ShowTextLetterByLetter(slide.text));

            yield return new WaitForSeconds(holdDuration);

            yield return StartCoroutine(FadeOut(slide.backgroundImage, slide.text));

            yield return new WaitForSeconds(transitionTime);

            slide.slideObject.SetActive(false);
        }

        yield return new WaitForSeconds(finalDelay);

        SkipIntro();
    }

    private IEnumerator FadeIn(Image image, TextMeshProUGUI text)
    {
        float timer = 0;
        Color imageColor = image.color;
        Color textColor = text.color;

        while (timer <= fadeInDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, timer / fadeInDuration);
            image.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
            text.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }
    }

    private IEnumerator FadeOut(Image image, TextMeshProUGUI text)
    {
        float timer = 0;
        Color imageColor = image.color;
        Color textColor = text.color;

        while (timer <= fadeOutDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, timer / fadeOutDuration);
            image.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
            text.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null;
        }
    }

    private IEnumerator ShowTextLetterByLetter(TextMeshProUGUI textComponent)
    {
        string fullText = textComponent.text;
        textComponent.text = "";

        foreach (char letter in fullText)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }
    }

    public void SkipIntro()
    {
        StopAllCoroutines();
        AudioManager.Instance.SwitchMusic(AudioManager.Instance.gameMusic);
        SceneManager.LoadScene("Game");
    }
}