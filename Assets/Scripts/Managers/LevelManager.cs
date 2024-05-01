using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public string levelName;
    [SerializeField] AudioClip backgroundMusic;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        MusicManager.Instance.StartMusicChange(backgroundMusic);
        MusicManager.Instance.PlayMusic();
    }
}