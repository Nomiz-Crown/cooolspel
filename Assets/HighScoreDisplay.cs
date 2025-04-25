using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreDisplay : MonoBehaviour
{
    private string filePath;
    TextMeshProUGUI tmp;
    public float myLevel;
    string displayThis;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        filePath = Path.Combine(Application.persistentDataPath, "highscore" + myLevel + ".json");
        float temp = float.Parse(File.ReadAllText(filePath));
        float minutesPassed = Mathf.Floor(temp / 60);
        string secondsPassedToDisplay = $"{temp - minutesPassed * 60}";
        LoadHighScore($"{minutesPassed}:"+secondsPassedToDisplay);
    }


    public void LoadHighScore(string score)
    {
        if (File.Exists(filePath))
        {
            tmp.text = "HighScore: " + score;
        }
    }
}
