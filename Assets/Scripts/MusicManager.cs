using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource musicSource;
    public AudioSource sfxSource;


    private void Awake()
    {
        Instance = this;
        musicSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ChangeMusic(AudioClip clip)
    {
        musicSource.clip = clip;
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void StopSound()
    {
        sfxSource.Stop();
    }
}
