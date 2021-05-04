using UnityEngine;
using UnityEngine.SceneManagement;

public static class Scenes
{
    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        
#endif
    }

    private static void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void Show(Component component)
    {
        component.gameObject.SetActive(true);
    }

    public static void Hide(Component component)
    {
        component.gameObject.SetActive(false);
    }
}
