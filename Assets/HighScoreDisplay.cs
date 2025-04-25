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
    public float myLevel;
    public bool iAmHeatwave;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        if (iAmHeatwave)
        {
            filePath = Path.Combine(Application.persistentDataPath, "highscoreheatwave.json");
            string ContentsOfJson = File.ReadAllText(filePath);
            if (ContentsOfJson == null || ContentsOfJson == "")
            {
                LoadHighScore("");
            }
            else
            {
                float waveHighScore = float.Parse(File.ReadAllText(filePath));
                LoadHighScore("Wave " + waveHighScore);
            }
        }
        else
        {
            filePath = Path.Combine(Application.persistentDataPath, "highscore" + myLevel + ".json");
            string ContentsOfJson = File.ReadAllText(filePath);
            if (ContentsOfJson == null || ContentsOfJson == "")
            {
                LoadHighScore("");
            }
            else
            {
                float temp = float.Parse(ContentsOfJson);
                float minutesPassed = Mathf.Floor(temp / 60);
                string secondsPassedToDisplay = $"{temp - minutesPassed * 60}";
                LoadHighScore($"{minutesPassed}:" + secondsPassedToDisplay);
            }
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
