using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;


public class realTimer : MonoBehaviour
{
    TextMeshProUGUI tmp;
    composterWin winner;
    [HideInInspector] public string displayMe;
    float thisLevel;
    string highScore;
    private string filePath;
    [HideInInspector] public float secondsPassed;
    // Start is called before the first frame update
    void Start()
    {
        winner = FindObjectOfType<composterWin>();
        if (winner == null) 
        {
            Debug.LogWarning("Composter Win Script is missing from scene, Timer and highscores are disabled."); 
            return;
        }
        thisLevel = winner.levelCompleted;
        filePath = Path.Combine(Application.persistentDataPath, "highscore" + thisLevel + ".json");
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (winner == null) return;
        tmp.text = displayMe;
        if (winner.saveScore)
        {
            SaveHighScore();
        }
    }

    public void SaveHighScore()
    {
        // Check if the file exists
        if (!File.Exists(filePath))
        {
            // If the file does not exist, create it with the current score
            string initialScore = "99999999"; // Default score
            File.WriteAllText(filePath, initialScore);
            Debug.Log("High score file created with initial score: " + initialScore);
        }

        // Read the high score from the file
        string highScoreString = File.ReadAllText(filePath);

        // Try to parse the high score
        float highScoreValue;
        if (!float.TryParse(highScoreString, out highScoreValue))
        {
            Debug.LogError("Failed to parse high score from file. Content: " + highScoreString);
            return; // Exit if parsing fails
        }

        // Compare and save the new high score if it's higher
        if (secondsPassed >= highScoreValue) return;
        string newHighScore = $"{Mathf.Round(secondsPassed * 1000) / 1000}";
        Debug.Log(newHighScore + " is what I save");
        File.WriteAllText(filePath, newHighScore);
    }

}
