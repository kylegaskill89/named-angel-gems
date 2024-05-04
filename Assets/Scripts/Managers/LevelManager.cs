using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
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
        StartCoroutine(MusicManager.Instance.ChangeMusic(backgroundMusic, 0f));
        MusicManager.Instance.PlayMusic();
    }
}