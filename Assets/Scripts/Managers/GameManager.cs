using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public enum GameState { Normal, Dialogue, Pause }
    [SerializeField] ControlCharacter controlCharacter;
    [SerializeField] public PlayerStats playerStats;

    public GameState state;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = GameState.Normal;

        DialogueManager.Instance.OnShowDialogue += () =>
        {
            state = GameState.Dialogue;
        };

        DialogueManager.Instance.OnCloseDialogue += () =>
        {
            if (state == GameState.Dialogue)
            {
                state = GameState.Normal;
            }
        };
    }

    public void Update()
    {
        if (state == GameState.Normal)
        {
            controlCharacter.HandleUpdate();
        }
        else if (state == GameState.Dialogue)
        {
            DialogueManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Pause)
        {
            UIManager.Instance.PauseUpdate();
        }
    }
}
