using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class StyleLogicV2 : MonoBehaviour
{
    float styleScore;
    string scoreToDisplay;
    TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCycle();
    }
    void UpdateCycle()
    {
        FormatScore();
        skib();
        tmp.text = scoreToDisplay;
        print($"score is {styleScore} and score to display is {scoreToDisplay}");
    }
    void FormatScore()
    {
        if (styleScore < 10 && styleScore >= 0) scoreToDisplay = $"000{styleScore}";
        else if (styleScore < 100 && styleScore >= 10) scoreToDisplay = $"00{styleScore}";
        else if (styleScore < 1000 && styleScore >= 100) scoreToDisplay = $"0{styleScore}";
        else if (styleScore < 10000 && styleScore >= 1000) scoreToDisplay = $"{styleScore}";
        else if (styleScore < 0)
        {
            styleScore = 0;
            scoreToDisplay = $"fuck you";
        }
    }
    public void AdjustScore(int amt)
    {
        styleScore += Mathf.Round(Random.Range(amt - 10, amt + 10));
        print($"adjusted score, old score was {scoreToDisplay} new score is {styleScore}");
    }
    void skib()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            AdjustScore(100);
        }
    }
}
