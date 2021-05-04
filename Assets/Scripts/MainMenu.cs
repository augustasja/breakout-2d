using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

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
