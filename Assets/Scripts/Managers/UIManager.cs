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
    [SerializeField] Canvas gameplayCanvas;
    [SerializeField] Canvas pauseCanvas;

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
        playerObject.transform.position = teleportLocation.position + new Vector3(0, 3.8f, 0);
        playerObject.transform.rotation = teleportLocation.rotation;

        FadeImage.CrossFadeAlpha(0f, delay / 3, false);
        yield return new WaitForSeconds(delay / 3);
        GameManager.Instance.state = GameManager.GameState.Normal;

        yield return null;
    }

    public void ResumeGame()
    {
        gameplayCanvas.gameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
    }
    public void PauseGame()
    {
        Debug.Log("Pause");
        gameplayCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
    }

    public void PauseUpdate()
    {
        if (Input.GetButtonDown("Pause"))
        {
            ResumeGame();
            GameManager.Instance.state = GameManager.GameState.Normal;
        }
    }
}
