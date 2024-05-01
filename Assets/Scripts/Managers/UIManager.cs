using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] Image FadeImage;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator FadeToBlack(float delay)
    {
        GameManager.Instance.state = GameManager.GameState.Dialogue;

        float timer = 0;
        float alpha = 0;

        while (timer < delay)
        {
            timer += Time.deltaTime;
            alpha += .1f;
            FadeImage.color = new Color(0, 0, 0, alpha);
        }

        yield return new WaitForSeconds(delay);
        timer = 0;

        while (timer < delay)
        {
            timer += Time.deltaTime;
            alpha -= .1f;
            FadeImage.color = new Color(0, 0, 0, alpha);
        }
        yield return new WaitForSeconds(1f);

        GameManager.Instance.state = GameManager.GameState.Normal;

        yield return null;
    }
}
