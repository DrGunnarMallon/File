using UnityEngine;
using UnityEngine.SceneManagement;

public class BootstrapManager : MonoBehaviour
{
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject audioManagerPrefab;
    [SerializeField] private GameObject sceneLoadManager;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private string startingLevel;

    private GameObject bootstrapCamera;
    private GameObject loadingCanvas;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (FindFirstObjectByType<GameManager>() == null)
        {
            Instantiate(gameManagerPrefab);
        }

        if (FindFirstObjectByType<AudioManager>() == null)
        {
            Instantiate(audioManagerPrefab);
        }

        if (FindFirstObjectByType<SceneLoadManager>() == null)
        {
            Instantiate(sceneLoadManager);
        }

        if (FindFirstObjectByType<UIManager>() == null)
        {
            Instantiate(uiManager);
        }

        bootstrapCamera = GameObject.FindGameObjectWithTag("Bootstrap Camera");
        loadingCanvas = GameObject.FindGameObjectWithTag("Loading Canvas");
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        // SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        SceneManager.LoadScene(startingLevel, LoadSceneMode.Additive);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!bootstrapCamera) return;

        Camera mainSceneCamera = FindFirstObjectByType<Camera>();

        if (mainSceneCamera != null && bootstrapCamera != null && mainSceneCamera != bootstrapCamera)
        {
            Destroy(bootstrapCamera);
            bootstrapCamera = null;
        }

        Destroy(loadingCanvas);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
