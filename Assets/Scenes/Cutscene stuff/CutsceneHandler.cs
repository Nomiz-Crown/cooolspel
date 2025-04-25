using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CutsceneLine
{
    public string sentence;
    public GameObject imageToDisable;
    public GameObject imageToEnable;
}

public class CutsceneHandler : MonoBehaviour
{
    public TMP_Text cutsceneText;
    public Button continueButton;
    public List<CutsceneLine> cutsceneLines;

    public Image fadePanelImage;
    public float fadeDuration = 1f;

    private int currentSentenceIndex = 0;
    private Coroutine typingCoroutine;

    void Start()
    {
        continueButton.onClick.AddListener(DisplayNextSentence);
        continueButton.gameObject.SetActive(false);
        StartCoroutine(FadeOutPanel());
    }

    IEnumerator FadeOutPanel()
    {
        float timer = 0f;
        Color color = fadePanelImage.color;
        color.a = 1f;
        fadePanelImage.color = color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadePanelImage.color = color;
            yield return null;
        }

        color.a = 0f;
        fadePanelImage.color = color;
        fadePanelImage.gameObject.SetActive(false);

        if (cutsceneLines.Count > 0)
        {
            HandleImageSwap(currentSentenceIndex);
            typingCoroutine = StartCoroutine(TypeSentence(cutsceneLines[currentSentenceIndex].sentence));
        }
    }

    void DisplayNextSentence()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        continueButton.gameObject.SetActive(false);

        if (currentSentenceIndex < cutsceneLines.Count - 1)
        {
            currentSentenceIndex++;
            HandleImageSwap(currentSentenceIndex);
            typingCoroutine = StartCoroutine(TypeSentence(cutsceneLines[currentSentenceIndex].sentence));
        }
        else
        {
            LevelData.levelCompleted = 3;
            SceneManager.LoadScene("main menu");
        }
    }

    IEnumerator TypeSentence(string sentence)
    {
        cutsceneText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            cutsceneText.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        continueButton.gameObject.SetActive(true);
    }

    void HandleImageSwap(int index)
    {
        CutsceneLine line = cutsceneLines[index];

        if (line.imageToDisable != null)
        {
            line.imageToDisable.SetActive(false);
        }

        if (line.imageToEnable != null)
        {
            line.imageToEnable.SetActive(true);
        }
    }
}
