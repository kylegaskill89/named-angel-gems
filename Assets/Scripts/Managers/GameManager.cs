using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public enum GameState { Normal, Dialogue, Pause }
    [SerializeField] ControlCharacter controlCharacter;
    [SerializeField] public PlayerStats playerStats;

    public int currentMoney;
    [HideInInspector] public int playHours, playMinutes;
    private float timer;

    public GameState state;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UIManager.Instance.UpdateTimeText(playHours, playMinutes);
        UIManager.Instance.UpdateMoneyText(currentMoney);

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

        if (state != GameState.Pause)
        {
            timer += Time.deltaTime;
            if (timer >= 60)
            {
                playMinutes++;
                timer = 0;
                if (playMinutes == 60)
                {
                    playHours++;
                    playMinutes = 0;
                }
                UIManager.Instance.UpdateTimeText(playHours, playMinutes);
            }
        }
    }

    public void UpdateMoney(int changeAmt)
    {
        currentMoney = changeAmt + currentMoney;
        UIManager.Instance.UpdateMoneyText(currentMoney);
    }
}
