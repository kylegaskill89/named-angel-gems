using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private AudioSource musicSource;
    public AudioSource sfxSource;
    [SerializeField] AnimationCurve volumeCurve;


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

    public IEnumerator ChangeMusic(AudioClip clip, float fadeTime)
    {
        float timer = 0;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);
            musicSource.volume = volumeCurve.Evaluate(timer);
            yield return null;
        }        

        musicSource.clip = clip;
        musicSource.Play();

        timer = fadeTime;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            musicSource.volume = volumeCurve.Evaluate(timer);
            yield return null;
        }
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
