using UnityEngine;
using UnityEngine.SceneManagement;

public class composterWin : MonoBehaviour
{
    public int levelCompleted;

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Still touching trigger: " + other.name);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("collider triggerrrrrsdsdsd");
        if (other.CompareTag("Player"))
        {
            Debug.Log("player touchy");
            LevelData.levelCompleted = levelCompleted;

            if (levelCompleted == 3)
            {
                SceneManager.LoadScene("End Cutscene");
            }
            else
            {
                SceneManager.LoadScene("main menu");
            }
        }
    }
}
