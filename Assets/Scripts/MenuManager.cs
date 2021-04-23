using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public Slider[] Volume;
    public Toggle[] Resolution;
    public int[] ScreenWidth;

    private int activeResolutionIndex;

    public void Start()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("First Level");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Main()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void SetResolution(int i)
    {
        if (Resolution[i].isOn)
        {
            activeResolutionIndex = i;
            float aspect = 16 / 9f;
            Screen.SetResolution(ScreenWidth[i], 
                (int)(ScreenWidth[i] / aspect), false);
        }
    }

    public void SetFullscreen(bool fullScreen)
    {
        for (int i = 0; i < Resolution.Length; i++)
        {
            Resolution[i].interactable = !fullScreen;
        }

        if (fullScreen)
        {
            var allResolutions = Screen.resolutions;
            var maxResolutions = allResolutions[allResolutions.Length - 1];
            Screen.SetResolution(maxResolutions.width, maxResolutions.height, true);
        }

        else
        {
            SetResolution(activeResolutionIndex);
        }
    }

    //public void SetMasterVolume(float x)
    //{
    //    AudioManager.instance.SetVolume(x, AudioManager.AudioChannel.Master);
    //}

    //public void SetMusicVolume(float x)
    //{
    //    AudioManager.instance.SetVolume(x, AudioManager.AudioChannel.Music);

    //}

    //public void SetEffectsVolume(float x)
    //{
    //    AudioManager.instance.SetVolume(x, AudioManager.AudioChannel.Effects);
    //}
}
