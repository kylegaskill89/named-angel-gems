using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCancelButton : MonoBehaviour, IButtonBehavior
{
    public void OnUse()
    {
        UIManager.Instance.ResumeGame();
    }
}
