using UnityEngine;
using UnityEngine.SceneManagement;

public class composterWin : MonoBehaviour
{
    public int levelCompleted; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelData.levelCompleted = levelCompleted;

            SceneManager.LoadScene("main menu");
        }
    }
}
