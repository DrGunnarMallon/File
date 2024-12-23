using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [Header("Heart Settings")]
    [SerializeField] private Sprite fullHeartSprite;
    [SerializeField] private Sprite emptyHeartSprite;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private int heartSpacing = 1;
    [SerializeField] private float scaleFactor = 1f;

    private List<Image> heartImages = new List<Image>();

    private void Start()
    {
        InitializeHearts(GameManager.Instance.MaxHearts);
        UpdateHearts();
    }

    private void InitializeHearts(int maxHearts)
    {
        Debug.Log($"Initializing {maxHearts} hearts");
        for (int i = 0; i < maxHearts; i++)
        {
            GameObject heart = new GameObject($"Heart_{i + 1}", typeof(Image));
            heart.transform.SetParent(transform);
            heart.transform.localScale = Vector3.one * scaleFactor;

            Image heartImage = heart.GetComponent<Image>();
            heartImage.sprite = emptyHeartSprite;

            heartImages.Add(heartImage);
        }

        HorizontalLayoutGroup layout = gameObject.AddComponent<HorizontalLayoutGroup>();
        layout.childAlignment = TextAnchor.MiddleCenter;
        layout.spacing = heartSpacing;
    }

    public void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < GameManager.Instance.CurrentHearts)
            {
                heartImages[i].sprite = fullHeartSprite;
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite;
            }
        }
    }
}
