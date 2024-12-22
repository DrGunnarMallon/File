using UnityEngine;

public class Win : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Debug.Log("Go to main menu");
        GameManager.Instance.GoToMainMenu();
    }
}
