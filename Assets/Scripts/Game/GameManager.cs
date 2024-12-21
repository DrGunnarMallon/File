using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private int maxHearts = 4;
    [SerializeField] public int currentHearts = 4;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentHearts = maxHearts;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
