using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("Effects", Mathf.Log10(volume) * 20);
    }

    public void SetBackgroundVolume(float volume)
    {
        audioMixer.SetFloat("Background", Mathf.Log10(volume) * 20);
    }

}
