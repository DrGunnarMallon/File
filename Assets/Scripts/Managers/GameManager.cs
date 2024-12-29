using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [Header("Game Settings")]
    [SerializeField] private int maxHearts = 5;
    [SerializeField] private int startingHearts = 5;
    [SerializeField] private string startingLevel;

    public int MaxHearts => maxHearts;
    public int CurrentHearts { get; private set; }
    public bool hasCollectedLife { get; private set; } = false;

    [SerializeField] public Vector3 lastCheckPoint;

    #region Singlton Pattern

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

    #endregion

    #region Checkpoint Logic

    public void SetCheckpoint(Vector3 position)
    {
        Debug.Log("Checkpoint set at: " + position);
        lastCheckPoint = position;
    }

    public Vector3 GetLastCheckpoint()
    {
        return lastCheckPoint;
    }

    #endregion

    #region Game Management

    public void ResetGameParameters()
    {
        CurrentHearts = startingHearts;
        hasCollectedLife = false;
    }

    public void RestartGame()
    {
        ResetGameParameters();
        SceneManager.LoadScene(startingLevel);
        StartCoroutine(UIManager.Instance.FadeFromBlack());
    }

    public void GoToMainMenu()
    {
        ResetGameParameters();
        SceneManager.LoadScene("Bootstrap");
        StartCoroutine(UIManager.Instance.FadeFromBlack());
    }

    private IEnumerator HandleGameOver()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }

    #endregion

    #region Health Management

    public void ReduceHearts()
    {
        if (CurrentHearts > 0)
        {
            CurrentHearts--;
            UIManager.Instance.UpdateHeartsDisplay();
        }
    }

    public void IncreaseHearts()
    {
        if (CurrentHearts < maxHearts)
        {
            CurrentHearts++;
            UIManager.Instance.UpdateHeartsDisplay();
        }
        else
        {
            CurrentHearts = maxHearts;
        }
    }


    public void StartGame()
    {
        AudioManager.Instance.SwitchMusic(AudioManager.Instance.gameMusic);
        SceneManager.LoadScene("Level1");
    }

    public void PlayerDied()
    {
        ReduceHearts();
        UIManager.Instance.UpdateHeartsDisplay();

        if (CurrentHearts <= 0)
        {
            StartCoroutine(HandleGameOver());
        }
    }

    #endregion
}