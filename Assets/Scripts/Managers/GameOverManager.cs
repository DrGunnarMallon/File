using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.GoToMainMenu();
    }
}
