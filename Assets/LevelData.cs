using UnityEngine;

public static class LevelData
{
    public static int levelCompleted
    {
        get => PlayerPrefs.GetInt("levelCompleted", 0);
        set
        {
            int current = PlayerPrefs.GetInt("levelCompleted", 0);
            if (value > current)
            {
                PlayerPrefs.SetInt("levelCompleted", value);
                PlayerPrefs.Save();
            }
        }
    }
}
