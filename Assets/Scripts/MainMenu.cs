using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform mainMenu;

    [SerializeField]
    private RectTransform optionsMenu;

    private void Start()
    {
        ShowMainMenu();
    }

    public void Play()
    {
        Scenes.LoadNextScene();
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Scenes.Exit();
    }

    public void ShowMainMenu()
    {
        Scenes.Show(mainMenu);
        Scenes.Hide(optionsMenu);
    }

    public void ShowOptionsMenu()
    {
        Scenes.Show(optionsMenu);
        Scenes.Hide(mainMenu);
    }
}
