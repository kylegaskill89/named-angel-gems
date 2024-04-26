using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] Dialogue healDialogue;
    [SerializeField] int healAmount;

    public void Interact()
    {
        if (GameManager.Instance.playerStats.currentHealth < GameManager.Instance.playerStats.maxHealth)
        {
            StartCoroutine(DialogueManager.Instance.ShowDialogue(healDialogue));
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Heal(healAmount);
        }
        else
        {
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
        }
    }
}
