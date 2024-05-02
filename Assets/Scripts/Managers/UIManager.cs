using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] Image FadeImage;
    [SerializeField] CanvasRenderer fadeCanvas;
    [SerializeField] GameObject playerObject;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator DoorFade(float delay, Transform teleportLocation)
    {
        GameManager.Instance.state = GameManager.GameState.Dialogue;
        Color fixedColor = FadeImage.color;
        fixedColor.a = 1;
        FadeImage.color = fixedColor;
        fadeCanvas.SetAlpha(0f);
        FadeImage.CrossFadeAlpha(1f, delay / 3, false);

        yield return new WaitForSeconds(delay / 2);
        playerObject.transform.position = teleportLocation.position;
        playerObject.transform.rotation = teleportLocation.rotation;

        FadeImage.CrossFadeAlpha(0f, delay / 3, false);
        yield return new WaitForSeconds(delay / 3);
        GameManager.Instance.state = GameManager.GameState.Normal;

        yield return null;
    }
}
