using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private void Start()
    {
        if (!AudioManager.Instance.IsMenuMusicPlaying())
        {
            AudioManager.Instance.PlayMenuMusic();
        }
    }
}
