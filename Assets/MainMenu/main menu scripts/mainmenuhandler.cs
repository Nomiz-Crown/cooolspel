using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenuhandler : MonoBehaviour
{
    public string mainMenuSceneName = "main menu";
    public GameObject level2Button;
    public GameObject level3Button;
    public GameObject level4Button;

    public int receivedLevel;
    public GameObject panelToActivate;
    public string scene1Name;
    public string scene2Name;
    public string scene3Name;
    public string scene4Name;

    [Header("New Additions")]
    public GameObject settingsButton;
    public GameObject playButton;
    public GameObject settingsPanel;
    public GameObject graphicToZoom;
    public float zoomDuration = 0.5f;
    public Vector3 zoomScale = new Vector3(1.5f, 1.5f, 1f);
    public Vector3 leftPosition = new Vector3(-300f, 0f, 0f);
    public Vector3 rightPosition = new Vector3(300f, 0f, 0f);

    public mainmenugraphicstuff graphicStuff;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Coroutine currentZoomCoroutine;

    void Start()
    {
        receivedLevel = LevelData.levelCompleted;

        if (graphicToZoom != null)
        {
            originalScale = graphicToZoom.transform.localScale;
            originalPosition = graphicToZoom.transform.localPosition;
        }

        // Setup level buttons
        SetButtonState(level2Button?.GetComponent<Button>(), receivedLevel >= 1);
        SetButtonState(level3Button?.GetComponent<Button>(), receivedLevel >= 2);
        SetButtonState(level4Button?.GetComponent<Button>(), receivedLevel >= 3);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Check if we are in the Main Menu scene
            if (SceneManager.GetActiveScene().name == mainMenuSceneName)
            {
                // Reset progress
                PlayerPrefs.DeleteKey("levelCompleted");
                PlayerPrefs.Save();
                Debug.Log("Progress reset!");

                // Reload the main menu scene
                SceneManager.LoadScene(mainMenuSceneName);
            }
        }
    }



    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void OnPlayButtonClicked()
    {
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }

        if (playButton != null) playButton.SetActive(false);
        if (settingsButton != null) settingsButton.SetActive(false);

        if (graphicStuff != null)
        {
            graphicStuff.SetMaxOffset(0.2f);  
        }

        if (graphicToZoom != null)
        {
            if (currentZoomCoroutine != null) StopCoroutine(currentZoomCoroutine);
            currentZoomCoroutine = StartCoroutine(ZoomAndMove(graphicToZoom, leftPosition));
        }
    }

    private void SetButtonState(Button button, bool unlocked)
    {
        if (button == null) return;

        button.interactable = unlocked;

        ColorBlock colors = button.colors;
        colors.normalColor = unlocked ? Color.white : new Color(0.5f, 0.5f, 0.5f); // Light gray when locked
        colors.highlightedColor = unlocked ? new Color(0.9f, 0.9f, 0.9f) : new Color(0.5f, 0.5f, 0.5f);
        colors.pressedColor = unlocked ? new Color(0.8f, 0.8f, 0.8f) : new Color(0.4f, 0.4f, 0.4f);
        colors.disabledColor = new Color(0.4f, 0.4f, 0.4f);
        button.colors = colors;
    }



    public void OnSettingsButtonClicked()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }

        if (playButton != null) playButton.SetActive(false);
        if (settingsButton != null) settingsButton.SetActive(false);

        if (graphicStuff != null)
        {
            graphicStuff.SetMaxOffset(0.2f); 
        }

        if (graphicToZoom != null)
        {
            if (currentZoomCoroutine != null) StopCoroutine(currentZoomCoroutine);
            currentZoomCoroutine = StartCoroutine(ZoomAndMove(graphicToZoom, rightPosition));
        }
    }

   
    public void OnHidePanelButtonClicked()
    {
        print("hi!");
        if (panelToActivate != null) panelToActivate.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);

        if (playButton != null) playButton.SetActive(true);
        if (settingsButton != null) settingsButton.SetActive(true);

        if (graphicStuff != null)
        {
            graphicStuff.SetMaxOffset(0.15f); 
        }

        if (graphicToZoom != null)
        {
            if (currentZoomCoroutine != null) StopCoroutine(currentZoomCoroutine);
            currentZoomCoroutine = StartCoroutine(ZoomAndMove(graphicToZoom, originalPosition, originalScale));
        }
    }

    // ?? Overload for resetting both scale and position
    private IEnumerator ZoomAndMove(GameObject obj, Vector3 targetLocalPos)
    {
        yield return ZoomAndMove(obj, targetLocalPos, zoomScale);
    }

    private IEnumerator ZoomAndMove(GameObject obj, Vector3 targetLocalPos, Vector3 targetScale)
    {
        float elapsed = 0f;
        Vector3 startScale = obj.transform.localScale;
        Vector3 startPos = obj.transform.localPosition;

        while (elapsed < zoomDuration)
        {
            float t = elapsed / zoomDuration;
            obj.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            obj.transform.localPosition = Vector3.Lerp(startPos, targetLocalPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = targetScale;
        obj.transform.localPosition = targetLocalPos;
    }
}
