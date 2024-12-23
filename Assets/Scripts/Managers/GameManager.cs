using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Game Settings")]
    [SerializeField] private int maxHearts = 5;
    [SerializeField] private int startingHearts = 5;

    public int MaxHearts => maxHearts;

    public int CurrentHearts { get; private set; }
    public bool hasCollectedLife { get; private set; } = false;

    // Singelton pattern

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CurrentHearts = startingHearts;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Game Management

    public void ResetGameParameters()
    {
        CurrentHearts = startingHearts;
        hasCollectedLife = false;
    }

    public void RestartGame()
    {
        ResetGameParameters();
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        ResetGameParameters();
        SceneManager.LoadScene("MainMenu");
    }

    // Health

    public void ReduceHearts() => CurrentHearts = CurrentHearts > 0 ? CurrentHearts-- : CurrentHearts = 0;
    public void IncreaseHearts() => CurrentHearts = CurrentHearts < maxHearts ? CurrentHearts++ : CurrentHearts = maxHearts;


    public void StartGame()
    {
        AudioManager.Instance.SwitchMusic(AudioManager.Instance.gameMusic);
        SceneManager.LoadScene("Game");
    }
}