using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    public void GoToMainMenu()
    {
        Debug.Log("Go to main menu");
        GameManager.Instance.GoToMainMenu();
    }
}
