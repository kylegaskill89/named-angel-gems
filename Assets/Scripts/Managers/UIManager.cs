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
    [SerializeField] GameObject currentScreen;
    [SerializeField] GameObject pauseScreen;

    [SerializeField] Canvas gameplayCanvas;
    [SerializeField] Canvas pauseCanvas;

    [SerializeField] GameObject[] pauseItems;
    [SerializeField] Button[] pauseMainButtons;
    [SerializeField] Transform[] pauseMainPoints;
    [SerializeField] Transform defaultPoint;

    [SerializeField] Button[] newScreenButtons;
    [SerializeField] Transform[] newScreenPoints;

    [SerializeField] TextMeshProUGUI descriptionText, timeText, moneyText;

    [SerializeField] Color defaultColor;
    [SerializeField] Color selectedColor;

    int selectedButton;
    bool inSecondaryMenu = false;

    private void Awake()
    {
        Instance = this;
        currentScreen = pauseScreen;
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
            MusicManager.Instance.PlaySound(menuCancelSound);
            PauseScreen();
        }

        if (selectedButton > -1)
        {
            if (Input.GetButtonDown("Slow"))
        {
            pauseMainButtons[selectedButton].TryGetComponent<IButtonBehavior>(out var buttonInteract);
            MusicManager.Instance.PlaySound(menuConfirmSound);
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
    }

    /*public void ChangeSelectedButton(int newButton)
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
    }*/

    public void ChangeSelectedButton(int newButton)
    {
        if (!inSecondaryMenu)
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
        else selectedButton = -1;
    }

    public IEnumerator ChangeScreen(GameObject newScreen)
    {
        currentScreen.SetActive(false);
        currentScreen = newScreen;
        currentScreen.SetActive(true);            
        inSecondaryMenu = true;
        selectedButton = 0;
        yield return null;
    }

    public void PauseScreen()
    {
        currentScreen.SetActive(false);
        currentScreen = pauseScreen;
        currentScreen.SetActive(true);
        /*foreach (var obj in pauseItems)
        {
            obj.gameObject.SetActive(true);
        }*/
        inSecondaryMenu = false;
        selectedButton = 0;
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
