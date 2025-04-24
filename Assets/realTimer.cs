using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class realTimer : MonoBehaviour
{
    TextMeshProUGUI tmp;
    composterWin winner;
    [HideInInspector] public string displayMe;
    float thisLevel;
    public string highScore;
    HighScoreDisplay bomb;
    // Start is called before the first frame update
    void Start()
    {
        bomb = GetComponent<HighScoreDisplay>();
        winner = FindObjectOfType<composterWin>();
        thisLevel = winner.levelCompleted;
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelData.levelCompleted != thisLevel)
        {
            tmp.text = displayMe;
            highScore = displayMe;
        }
        else
        {
            bomb.SaveHighScore();
        }
    }
}
