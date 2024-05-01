using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource musicSource;
    public AudioSource sfxSource;
    bool doneFading;


    private void Awake()
    {
        Instance = this;
        musicSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        musicSource.volume = 1;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void SetTrack(AudioClip clip)
    {
        musicSource.clip = clip;
    }

    public void StartMusicChange(AudioClip clip)
    {
        StartCoroutine(ChangeMusic(clip));
    }

    public IEnumerator ChangeMusic(AudioClip clip)
    {
        bool lerped = false;
        while (lerped == false)
        {
            musicSource.volume -= .1f * Time.deltaTime;

            if (musicSource.volume <= 0)
            {
                lerped = true;
            }
        }

        musicSource.clip = clip;
        PlayMusic();
        yield return null;
    }

    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void StopSound()
    {
        sfxSource.Stop();
    }
}
