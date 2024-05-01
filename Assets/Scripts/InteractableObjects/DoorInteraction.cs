using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] bool regularDoor = true;
    [SerializeField] AudioClip musicClip = null;
    public Transform teleportLocation;
    GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        if (regularDoor) 
        {
            playerObject.transform.position = teleportLocation.position;
            playerObject.transform.rotation = teleportLocation.rotation;
        }
        else
        {
            StartCoroutine(SpecialDoor());
        }

    }

    public IEnumerator SpecialDoor()
    {
        GameManager.Instance.state = GameManager.GameState.Dialogue;
        StartCoroutine(UIManager.Instance.FadeToBlack(1f));
        StartCoroutine(MusicManager.Instance.ChangeMusic(musicClip));
        playerObject.transform.position = teleportLocation.position;
        playerObject.transform.rotation = teleportLocation.rotation;
        GameManager.Instance.state = GameManager.GameState.Normal;
        yield return null;
    }

}
