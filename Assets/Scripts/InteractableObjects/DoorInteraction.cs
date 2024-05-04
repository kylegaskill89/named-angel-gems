using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] AudioClip musicClip = null;
    float delaySpeed = 3;
    public Transform teleportLocation;
    GameObject playerObject;

    enum DoorType
    {
        regularDoor,
        teleportDoor,
    }
    [SerializeField] DoorType type;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        if (type == DoorType.regularDoor) 
        {
            playerObject.transform.position = teleportLocation.position + new Vector3 (0, 3.8f, 0);
            playerObject.transform.rotation = teleportLocation.rotation;
        }
        else if (type == DoorType.teleportDoor)
        {
            TeleportDoor();
        }

    }

    public void TeleportDoor()
    {
        StartCoroutine(UIManager.Instance.DoorFade(delaySpeed, teleportLocation));
        if (musicClip)
        {
            StartCoroutine(MusicManager.Instance.ChangeMusic(musicClip, delaySpeed / 2));
        }
    }

}
