using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("Effects", volume);
    }

    public void SetBackgroundVolume(float volume)
    {
        audioMixer.SetFloat("Background", volume);
    }

}
