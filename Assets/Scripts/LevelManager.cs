using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIManager.Instance.ShowHeartsPanel();
        UIManager.Instance.UpdateHeartsDisplay();
    }

}
