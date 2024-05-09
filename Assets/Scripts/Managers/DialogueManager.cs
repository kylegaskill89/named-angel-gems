using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{

    [SerializeField] GameObject dialogueBox;
    [SerializeField] TextMeshProUGUI dialogueText;
    public int lettersPerSecond;
    public int defaultPerSecond;
    public int fastlettersPerSecond;

    public event Action OnShowDialogue;
    public event Action OnCloseDialogue;
    public event Action OnChangeDialogue;

    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    Dialogue dialogue;
    public int currentLine = 0;
    bool isTyping;

    public IEnumerator ShowDialogue(Dialogue dialogue)
    {
        yield return new WaitForEndOfFrame();
        OnShowDialogue?.Invoke();

        this.dialogue = dialogue;
        dialogueBox.SetActive(true);
        StartCoroutine(TypeDialogue(dialogue.Lines[0]));
    }


    public void HandleUpdate()
    {
        if (Input.GetButtonDown("Slow") && !isTyping)
        {
            lettersPerSecond = defaultPerSecond;
            currentLine++;
            OnChangeDialogue?.Invoke();
            if (currentLine < dialogue.Lines.Count)
            {
                StartCoroutine(TypeDialogue(dialogue.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogueBox.SetActive(false);
                OnCloseDialogue?.Invoke();
            }
        }
        else if (Input.GetButtonDown("Slow") && isTyping)
        {
            lettersPerSecond = fastlettersPerSecond;
        }
    }

    public IEnumerator TypeDialogue(string dialogue)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (var letter in dialogue.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
