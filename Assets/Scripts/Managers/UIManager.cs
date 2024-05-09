using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] AudioClip selectionSound;
    [SerializeField] AudioClip menuConfirmSound;
    [SerializeField] AudioClip menuCancelSound;

    [SerializeField] Image FadeImage;
    [SerializeField] CanvasRenderer fadeCanvas;

    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject selectedImageGO;

    [SerializeField] Canvas gameplayCanvas;
    [SerializeField] Canvas pauseCanvas;

    [SerializeField] GameObject[] pauseItems;
    [SerializeField] GameObject[] itemsToDisable;
    [SerializeField] Button[] pauseMainButtons;
    [SerializeField] Transform[] pauseMainPoints;
    [SerializeField] Transform defaultPoint;

    [SerializeField] TextMeshProUGUI descriptionText, timeText, moneyText;

    [SerializeField] Color defaultColor;
    [SerializeField] Color selectedColor;

    int selectedButton;
    bool inSecondaryMenu = false;

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        PauseScreen();
        pauseCanvas.gameObject.SetActive(false);
    }

    public IEnumerator DoorFade(float delay, Transform teleportLocation)
    {
        FadeImage.enabled = true;
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
        FadeImage.enabled = false;
        yield return null;
    }

    public void ResumeGame()
    {
        gameplayCanvas.gameObject.SetActive(true);
        pauseCanvas.gameObject.SetActive(false);
        GameManager.Instance.state = GameManager.GameState.Normal;
        selectedButton = 0;

        foreach (Button button in pauseMainButtons)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().color = defaultColor;
        }
    }
    public void PauseGame()
    {

        selectedImageGO.transform.position = defaultPoint.transform.position;
        pauseMainButtons[0].GetComponentInChildren<TextMeshProUGUI>().color = selectedColor;
        gameplayCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
    }

    public void PauseUpdate()
    {
        if (Input.GetButtonDown("Pause") && inSecondaryMenu == false)
        {
            ResumeGame();            
        }
        if (Input.GetButtonDown("Pause") && inSecondaryMenu)
        {
            PauseScreen();
        }

        if (Input.GetButtonDown("Slow"))
        {
            pauseMainButtons[selectedButton].TryGetComponent<IButtonBehavior>(out var buttonInteract);
            buttonInteract.OnUse();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeSelectedButton(selectedButton - 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            ChangeSelectedButton(selectedButton + 1);
        }
    }

    public void ChangeSelectedButton(int newButton)
    {
        if (newButton < 0)
        {
            newButton = pauseMainButtons.Length - 1;
        }
        else if (newButton > pauseMainButtons.Length - 1)
        {
            newButton = 0;
        }

        pauseMainButtons[selectedButton].GetComponentInChildren<TextMeshProUGUI>().color = defaultColor;
        selectedButton = newButton;
        pauseMainButtons[selectedButton].GetComponentInChildren<TextMeshProUGUI>().color = selectedColor;
        MusicManager.Instance.PlaySound(selectionSound);
        pauseMainButtons[selectedButton].TryGetComponent<ButtonInfo>(out var info);
        descriptionText.text = info.descriptionText;
        selectedImageGO.transform.position = pauseMainPoints[selectedButton].transform.position;
    }

    public IEnumerator ChangeScreen(GameObject[] newObjects)
    {
        foreach (var obj in pauseItems)
        {
            obj.SetActive(false);
        }

        yield return new WaitForSeconds(1);

        foreach (var obj in newObjects)
        {
            obj.gameObject.SetActive(true);
        }

        inSecondaryMenu = true;
        Debug.Log(inSecondaryMenu);
        yield return null;
    }

    public void PauseScreen()
    {
        foreach (var obj in itemsToDisable)
        {
            obj.SetActive(false);
        }
        foreach (var obj in pauseItems)
        {
            obj.gameObject.SetActive(true);
        }
        inSecondaryMenu = false;
    }

    public void UpdateTimeText(int hours, int minutes)
    {
        timeText.text = hours.ToString("D2") + ":" + minutes.ToString("D2");
    }

    public void UpdateMoneyText(int money)
    {
        moneyText.text = "$" + money.ToString();
    }
}
