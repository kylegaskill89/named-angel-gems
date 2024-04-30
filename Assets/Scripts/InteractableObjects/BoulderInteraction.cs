using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoulderInteraction : MonoBehaviour, IInteractable, IDamageable
{
    [SerializeField] Dialogue dialogue;
    [SerializeField] DialogueManager manager;
    [SerializeField] int health;
    [SerializeField] AudioClip destroySound;

    public void Interact()
    {
        StartCoroutine(DialogueManager.Instance.ShowDialogue(dialogue));
    }


    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            MusicManager.Instance.PlaySound(destroySound);
            Destroy(gameObject);
        }
    }

     public void OnDestroy()
     {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 24f);       
        

        for (int i = 0; i < hitColliders.Length; i++)
        {
            GameObject go = hitColliders[i].gameObject;
            if (go.TryGetComponent<PlayerStats>(out var playerStats))
            {
                Debug.Log("Ouch");
                playerStats.Damage(40);
            }
        }
     }
}
