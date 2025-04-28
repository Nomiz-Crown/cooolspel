using System.IO;
using TMPro;
using UnityEngine;

public class HighScoreDisplay : MonoBehaviour
{
    private string filePath;
    TextMeshProUGUI tmp;
    public string myLevel;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        filePath = Path.Combine(Application.persistentDataPath, "highscore" + myLevel + ".json");
        if (!File.Exists(filePath) && myLevel!="heatwave")
        {
            File.WriteAllText(filePath, "99999999");
        }
        else if(!File.Exists(filePath) && myLevel == "heatwave")
        {
            File.WriteAllText(filePath, "0");
        }
        string ContentsOfJson = File.ReadAllText(filePath);
        if (ContentsOfJson == null || ContentsOfJson == "99999999")
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
            string secondsPassedToDisplay = (temp - minutesPassed * 60).ToString();

            LoadHighScore($"{minutesPassed.ToString()}:{secondsPassedToDisplay}");
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
