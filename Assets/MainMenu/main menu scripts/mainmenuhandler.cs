using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenuhandler : MonoBehaviour
{
    public GameObject panelToActivate; // Assign the panel in the Inspector
    public string scene1Name; // Assign scene names in the Inspector
    public string scene2Name;
    public string scene3Name;
    public string scene4Name;

    // Method to show the panel
    public void ShowPanel()
    {
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(true);
        }
    }

    // Method to hide the panel
    public void HidePanel()
    {
        if (panelToActivate != null)
        {
            panelToActivate.SetActive(false);
        }
    }

    // Method to load a scene (call this from buttons)
    public void LoadScene(string sceneName)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}