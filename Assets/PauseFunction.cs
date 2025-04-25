using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseFunction : MonoBehaviour
{
    public GameObject pausePanel;
    public string mainMenuScene;
    public Slider volumeSlider; // assign in the Inspector

    private bool isPaused = false;

    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (volumeSlider != null)
        {
            // Set the slider to match saved volume
            volumeSlider.value = MusicVolumeManager.volumePercent;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isPaused)
                PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        if (pausePanel != null)
            pausePanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public void LoadScene()
    {
        Time.timeScale = 1f;
        if (!string.IsNullOrEmpty(mainMenuScene))
            SceneManager.LoadScene(mainMenuScene);
    }

    void OnVolumeChanged(float value)
    {
        MusicVolumeManager.SetVolume(value);
    }
}
