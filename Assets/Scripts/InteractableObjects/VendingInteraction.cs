using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] Dialogue healDialogue;
    [SerializeField] Dialogue noMoneyDialogue;
    [SerializeField] int healAmount;
    [SerializeField] AudioClip healSound;
    [SerializeField] int cost;

    public void Interact()
    {
        if (GameManager.Instance.playerStats.currentHealth < GameManager.Instance.playerStats.maxHealth)
        {
            if (GameManager.Instance.currentMoney > cost)
            {
                MusicManager.Instance.PlaySound(healSound);
                StartCoroutine(DialogueManager.Instance.ShowDialogue(healDialogue));
                GameManager.Instance.UpdateMoney(-cost);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Heal(healAmount);
            }
            else
            {
                StartCoroutine(DialogueManager.Instance.ShowDialogue(noMoneyDialogue));
            }
        }
        else
        {
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
        }
    }
}
