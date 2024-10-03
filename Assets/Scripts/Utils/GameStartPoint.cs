using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR

[DefaultExecutionOrder(-10000)]
public class GameStartPoint : MonoBehaviour
{
    private static bool isStarted = false;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) isStarted = true;

        if (isStarted) return;

        foreach (var go in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            go.SetActive(false);
        }

        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}

#endif
