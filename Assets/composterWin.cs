using UnityEngine;
using UnityEngine.SceneManagement;

public class composterWin : MonoBehaviour
{
    public int levelCompleted;

    void Start()
    {
        Debug.Log("composterWin script active");
    }

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

            SceneManager.LoadScene("main menu");
        }
    }
}
