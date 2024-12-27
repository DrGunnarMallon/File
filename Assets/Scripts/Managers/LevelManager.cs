using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    [SerializeField] private Transform startingPosition;

    private SceneLoadManager sceneLoadManager;

    private void Awake()
    {
        sceneLoadManager = FindFirstObjectByType<SceneLoadManager>();
    }

    void Start()
    {
        UIManager.Instance.ShowHeartsPanel();
        UIManager.Instance.UpdateHeartsDisplay();
        GameManager.Instance.SetCheckpoint(startingPosition.position);
    }

    public void LoadNextLevel()
    {
        sceneLoadManager.LoadScene(nextLevel);
    }
}
