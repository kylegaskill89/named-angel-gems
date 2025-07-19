using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItemsButton : MonoBehaviour, IButtonBehavior
{

    [SerializeField] GameObject normalItemsScreen;
    
    public void OnUse()
    {
        StartCoroutine(UIManager.Instance.ChangeScreen(normalItemsScreen));
    }
}