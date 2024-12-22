using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    [SerializeField] private int maxHearts = 5;
    [SerializeField] private int startingHearts = 4;
    [SerializeField] private Transform[] checkpoints;

    private int currentHearts;
    private bool hasCollectedLife = false;
    private bool canShoot = false;
    private int lastCheckPoint = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            currentHearts = startingHearts;

            Debug.Log($"Checkpoints loaded: {checkpoints.Length}");
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
        hasCollectedLife = false;
        SceneManager.LoadScene("Game");
    }

    public void GoToMainMenu()
    {
        currentHearts = startingHearts;
        hasCollectedLife = false;
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

    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }

    public bool CanShoot()
    {
        return canShoot;
    }

    public void SetCheckpointReached(int value)
    {
        lastCheckPoint = value;
    }

    public Transform GetCheckpoint()
    {
        return checkpoints[lastCheckPoint];
    }

    public int GetLastCheckpoint()
    {
        return lastCheckPoint;
    }

    public void ResetHearts()
    {
        currentHearts = maxHearts;
    }

    public void ResetLastCheckpoint()
    {
        lastCheckPoint = 0;
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