using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float effectsVolume = 1f;

    public static AudioManager instance;
    public AudioClip MenuMusic;

    public AudioClip ChestOpen;
    public AudioClip Coin;
    public AudioClip FootStep1;
    public AudioClip FootStep2;
    public AudioClip Jump;
    public AudioClip Land;

    AudioSource[] audioSources;

    private void Awake()
    {
        audioSources = new AudioSource[2];
        for (int i = 0; i < 2; i++)
        {
            var newMusicSource = new GameObject("Music source: " + (i + 1));
            audioSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }
    }

    public void PlaySound(AudioClip clip, Vector2 position)
    {
        AudioSource.PlayClipAtPoint(clip, position, effectsVolume = masterVolume);
    }

    internal void SetVolume(float x, object master)
    {
        throw new NotImplementedException();
    }
}
