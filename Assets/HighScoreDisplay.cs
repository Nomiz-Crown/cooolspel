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
    realTimer ok;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        ok = GetComponent<realTimer>();
        filePath = Path.Combine(Application.persistentDataPath, "highscore.json");
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadHighScore();
        }
    }

    public void SaveHighScore()
    {
        File.WriteAllText(filePath, ok.highScore);
    }

    public void LoadHighScore()
    {
        if (File.Exists(filePath))
        {
            ok.highScore = File.ReadAllText(filePath);
            tmp.text = "HighScore: " + ok.highScore;
        }
    }
}
