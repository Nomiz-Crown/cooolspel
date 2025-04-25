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
        print(filePath);
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (winner == null) return;
        tmp.text = displayMe;
        if (winner.saveScore)
        {
            print("hi i got in");
            SaveHighScore();
        }
    }

    public void SaveHighScore()
    {
        if (secondsPassed >= float.Parse(File.ReadAllText(filePath))) return;
        string gurt = $"{Mathf.Round(secondsPassed * 1000) / 1000}";
        print(gurt +" is what i save");
        File.WriteAllText(filePath, gurt);
    }
}
