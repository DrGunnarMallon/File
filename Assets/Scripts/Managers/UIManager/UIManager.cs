using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Fade Screen")]
    [SerializeField] private GameObject fadeScreen;
    [SerializeField] private float fadeDuration = 1.0f;

    [Header("Life Counter")]
    [SerializeField] private GameObject lifePanel;
    [SerializeField] private Transform heartsLayoutGroup;
    [SerializeField] private GameObject fullHeartPrefab;
    [SerializeField] private GameObject emptyHeartPrefab;
    private List<GameObject> heartImages = new List<GameObject>();

    [Header("Message Box")]
    [SerializeField] private GameObject messageBox;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private float moveTime = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateHeartsDisplay(GameManager.Instance.CurrentHearts, GameManager.Instance.MaxHearts);
        lifePanel.SetActive(false);
    }

    #region Singelton Implementation

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    #endregion

    #region Fade Screen Methods

    public IEnumerator FadeToBlack()
    {
        fadeScreen.SetActive(true);
        CanvasGroup canvasGroup = fadeScreen.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    public IEnumerator FadeFromBlack()
    {
        fadeScreen.SetActive(true);

        CanvasGroup canvasGroup = fadeScreen.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;

        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        fadeScreen.SetActive(false);
    }

    #endregion

    #region Life Counter Methods

    public void UpdateHeartsDisplay(int currentHearts, int maxHearts)
    {
        Debug.Log($"Current Hearts: {currentHearts}, Max Hearts: {maxHearts}");

        ShowHeartsPanel();

        foreach (var heart in heartImages)
        {
            Destroy(heart);
        }
        heartImages.Clear();

        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heartPrefabToUse = (i < currentHearts) ? fullHeartPrefab : emptyHeartPrefab;
            GameObject heart = Instantiate(heartPrefabToUse, heartsLayoutGroup);
            heartImages.Add(heart);
        }
    }

    public void ShowHeartsPanel()
    {
        lifePanel.SetActive(true);
    }

    #endregion

    #region Message Box Methods

    private Vector2 hiddenPosition = new Vector2(0, 100);
    private Vector2 visiblePosition = new Vector2(0, -100);
    private RectTransform messageBoxRect;

    public IEnumerator DisplayMessage(string message, float displayTime = 2f)
    {
        messageText.text = message;

        messageBoxRect = messageBox.GetComponent<RectTransform>();
        hiddenPosition = new Vector2(0, messageBoxRect.rect.height / 2);
        visiblePosition = new Vector2(0, -messageBoxRect.rect.height / 2);

        messageBox.SetActive(true);
        messageBoxRect.anchoredPosition = hiddenPosition;

        yield return MovePanel(hiddenPosition, visiblePosition, moveTime);

        yield return new WaitForSeconds(displayTime);

        yield return MovePanel(visiblePosition, hiddenPosition, moveTime);

        messageBox.SetActive(false);
    }

    private IEnumerator MovePanel(Vector2 start, Vector2 end, float duration)
    {
        float elapsedTime = 0f;
        messageBoxRect.anchoredPosition = start;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            messageBoxRect.anchoredPosition = Vector2.Lerp(start, end, t);
            yield return null;
        }

        messageBoxRect.anchoredPosition = end;
    }

    #endregion
}