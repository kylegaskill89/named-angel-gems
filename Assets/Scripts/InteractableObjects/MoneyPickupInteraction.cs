using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyPickupInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] int moneyAmount;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] int importantLine;

    public void Interact()
    {
        DialogueManager.Instance.OnChangeDialogue += CheckLine;
        DialogueManager.Instance.OnCloseDialogue += DestroyObject;
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
        GameManager.Instance.UpdateMoney(moneyAmount);
    }

    public void CheckLine()
    {
        if (DialogueManager.Instance.currentLine == importantLine)
        {
            MusicManager.Instance.PlaySound(pickupSound);
        }
    }
    public void DestroyObject()
    {
        DialogueManager.Instance.OnChangeDialogue -= CheckLine;
        DialogueManager.Instance.OnChangeDialogue -= DestroyObject;
        gameObject.SetActive(false);
    }
}
