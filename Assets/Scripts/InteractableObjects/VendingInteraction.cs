using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] Dialogue healDialogue;
    [SerializeField] int healAmount;
    [SerializeField] AudioClip healSound;

    public void Interact()
    {
        if (GameManager.Instance.playerStats.currentHealth < GameManager.Instance.playerStats.maxHealth)
        {
            MusicManager.Instance.PlaySound(healSound);
            StartCoroutine(DialogueManager.Instance.ShowDialogue(healDialogue));
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Heal(healAmount);
        }
        else
        {
            StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
        }
    }
}
