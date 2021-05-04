using UnityEngine;

public class GamePlayCanvas : MonoBehaviour
{
    [Header("Colletcibles")]
    [SerializeField]
    private RectTransform hearthContainer;

    [SerializeField]
    private GameObject hearthPrefab;

    [Header("Menus")]
    [SerializeField]
    private RectTransform pausedMenu;

    [SerializeField]
    private RectTransform gameOverMenu;

    private void Start()
    {
        Scenes.Hide(pausedMenu);
        Scenes.Hide(gameOverMenu);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePauseGame();
        }
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void UpdateHearthsUI(int newLives)
    {
        UpdateChildCount(hearthContainer, hearthPrefab, newLives);
    }

    public void ShowGameOverMenu()
    {
        Scenes.Hide(pausedMenu);
        Scenes.Show(gameOverMenu);
        Debug.Log("Game Over.");
    }

    public void ResumeGame()
    {
        Scenes.Hide(pausedMenu);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Scenes.RestartScene();
    }

    public void ExitGame()
    {
        Scenes.LoadPreviousScene();
    }

    private void TogglePauseGame()
    {
        if (IsGameOver())
        {
            return;
        }

        if (IsGamePaused())
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Scenes.Show(pausedMenu);
        Time.timeScale = 0;
    }

    private bool IsGameOver()
    {
        return gameOverMenu.gameObject.activeInHierarchy;
    }

    private bool IsGamePaused()
    {
        return pausedMenu.gameObject.activeInHierarchy;
    }

    private static void UpdateChildCount(Transform container,
        GameObject prefab, int newCount)
    {
        var childCount = container.childCount;
        var change = Mathf.Min(Mathf.Abs(childCount - newCount), childCount);
        if (childCount < newCount)
        {
            AddChildren(container, prefab, change);
        }
        else
        {
            RemoveChildren(container, change);
        }
    }

    private static void AddChildren(Transform container, GameObject prefab,
        int count)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, container);
        }
    }

    private static void RemoveChildren(Transform container, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Destroy(container.GetChild(i).gameObject);
        }
    }
}
