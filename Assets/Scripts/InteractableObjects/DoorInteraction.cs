using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour, IInteractable
{

    public Transform teleportLocation;
    GameObject playerObject;

    private void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        Debug.Log("Teleport to" + teleportLocation.gameObject.transform.parent.name);
        playerObject.transform.position = teleportLocation.position;
        playerObject.transform.rotation = teleportLocation.rotation;
    }

}
