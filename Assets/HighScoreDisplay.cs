using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class HighScoreDisplay : MonoBehaviour
{
    private string filePath;
    TextMeshProUGUI tmp;
    public string myLevel;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        filePath = Path.Combine(Application.persistentDataPath, "highscore" + myLevel + ".json");
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "");
        }
        string ContentsOfJson = File.ReadAllText(filePath);
        if (ContentsOfJson == null || ContentsOfJson == "")
        {
            LoadHighScore("");
        }
        else if (myLevel == "heatwave")
        {
            LoadHighScore("Wave " + ContentsOfJson);
        }
        else
        {
            float temp = float.Parse(ContentsOfJson);
            float minutesPassed = Mathf.Floor(temp / 60);
            string secondsPassedToDisplay = $"{temp - minutesPassed * 60}";
            LoadHighScore($"{minutesPassed}:" + secondsPassedToDisplay);
        }
    }


    public void LoadHighScore(string score)
    {
        if (File.Exists(filePath) && score != "")
        {
            tmp.text = "HighScore: " + score;
        }
        else tmp.text = "";
    }
}
