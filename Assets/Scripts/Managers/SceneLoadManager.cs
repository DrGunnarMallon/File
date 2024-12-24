using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public IEnumerator TransitionScene(string sceneName, LoadSceneMode sceneMode = LoadSceneMode.Single, bool fadeFirst = true)
    {
        if (fadeFirst)
        {
            yield return StartCoroutine(UIManager.Instance.FadeToBlack());
            yield return new WaitForSeconds(1f);
        }
        SceneManager.LoadScene(sceneName, sceneMode);
        yield return StartCoroutine(UIManager.Instance.FadeFromBlack());
    }
}