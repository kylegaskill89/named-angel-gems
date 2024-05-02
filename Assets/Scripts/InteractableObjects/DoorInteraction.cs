using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] bool regularDoor = true;
    [SerializeField] AudioClip musicClip = null;
    [SerializeField] float delaySpeed = 3;
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
            TeleportDoor();
        }

    }

    public void TeleportDoor()
    {
        StartCoroutine(UIManager.Instance.DoorFade(delaySpeed, teleportLocation));
        StartCoroutine(MusicManager.Instance.ChangeMusic(musicClip));
    }

}
