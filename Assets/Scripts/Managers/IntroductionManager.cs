using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // If using TextMeshPro
using UnityEngine.InputSystem;

public class IntroductionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] slides;
    [SerializeField] private float fadeInDuration = 2.0f;
    [SerializeField] private float fadeOutDuration = 1.0f;
    [SerializeField] private float transitionTime = 1.0f;
    [SerializeField] private float letterDelay = 0.05f;
    [SerializeField] private float initialDelay = 1.0f;
    [SerializeField] private float finalDelay = 0.7f;

    private PlayerInput playerInput;

    private bool nextScene = false;

    private void Start()
    {
        StartCoroutine(PlayIntro());
        foreach (var slide in slides)
        {
            slide.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
            slide.SetActive(false);
        }
    }

    private IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(initialDelay);

        foreach (var slide in slides)
        {
            slide.SetActive(true);

            Image backgroundImage = slide.GetComponentInChildren<Image>(true);
            TextMeshProUGUI text = slide.GetComponentInChildren<TextMeshProUGUI>(true);

            yield return StartCoroutine(FadeIn(backgroundImage, text));

            text.gameObject.SetActive(true);

            yield return StartCoroutine(ShowTextLetterByLetter(text));

            while (!nextScene) { yield return null; }

            yield return StartCoroutine(FadeOut(backgroundImage, text));
            yield return new WaitForSeconds(transitionTime);

            slide.SetActive(false);
            nextScene = false;
        }

        yield return new WaitForSeconds(finalDelay);
        SkipIntro();
    }

    private void OnJump(InputValue value)
    {
        NextSlide();
    }

    public void NextSlide()
    {
        nextScene = true;
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
            if (nextScene)
            {
                break;
            }
        }
    }

    public void SkipIntro()
    {
        StopAllCoroutines();
        GameManager.Instance.StartGame();
    }
}