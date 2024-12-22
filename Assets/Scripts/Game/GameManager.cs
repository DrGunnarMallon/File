using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private int maxHearts = 5;
    [SerializeField] private int startingHearts = 4;
    private int currentHearts;

    private bool hasCollectedLife = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentHearts = startingHearts;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReduceHearts()
    {
        currentHearts -= 1;
    }

    public void IncreaseHearts()
    {
        if (currentHearts < maxHearts)
        {
            currentHearts += 1;
        }
    }

    public int GetCurrentHearts()
    {
        return currentHearts;
    }

    public int GetMaxHearts()
    {
        return maxHearts;
    }

    public void RestartGame()
    {
        currentHearts = startingHearts;
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        currentHearts = startingHearts;
        SceneManager.LoadScene("MainMenu");
    }

    public void CollectLife()
    {
        hasCollectedLife = true;
    }

    public bool HasCollectedLife()
    {
        return hasCollectedLife;
    }
}

// using UnityEngine;

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance;

//     [Header("Game Settings")]
//     [SerializeField] private int maxHearts = 4;
//     public int CurrentHearts { get; private set; }

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//             CurrentHearts = maxHearts;
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public void ReduceHearts()
//     {
//         CurrentHearts--;
//         Debug.Log($"Player has {CurrentHearts} hearts left");
//     }
// }