using UnityEngine;

public class whichLevelsUnlocked : MonoBehaviour
{
    public int receivedLevel;

    void Start()
    {
        receivedLevel = LevelData.levelCompleted;

        Debug.Log("Level Completed: " + receivedLevel);
    }
}
