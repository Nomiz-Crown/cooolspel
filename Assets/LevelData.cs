using UnityEngine;

public static class LevelData
{
    public static int levelCompleted
    {
        get => PlayerPrefs.GetInt("levelCompleted", 0); // Default to 0 (no levels completed)
        set
        {
            int current = PlayerPrefs.GetInt("levelCompleted", 0);
            if (value > current) // only update if new level is higher
            {
                PlayerPrefs.SetInt("levelCompleted", value);
                PlayerPrefs.Save();
            }
        }
    }
}
