using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class StyleLogic : MonoBehaviour
{
    TextMeshProUGUI tmp;
    float Score = 0;
    string ScoreToDisplay;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        IncreaseScore(100);
    }

    // Update is called once per frame
    void Update()
    {
        FormatScore();
        tmp.text = ScoreToDisplay;
        print($"score is {Score} and score to display is {ScoreToDisplay}");
    }
    void blahbal()
    {
        if(Score < 0)
        {
            Score = 0;
        }
    }
    void FormatScore()
    {
        if (Score < 10)
        {
            ScoreToDisplay = $"000{Score}";
        }
        else if (Score < 100)
        {
            ScoreToDisplay = $"00{Score}";
        }
        else if (Score < 1000)
        {
            ScoreToDisplay = $"0{Score}";
        }
        else if (Score < 10000)
        {
            ScoreToDisplay = $"{Score}";
        }
        else if (Score <= 0)
        {
            ScoreToDisplay = "0000";
        }
    }
    public void ReduceScore(float amt)
    {
        print($"Decreasin score old score is {Score}");
        Score -= Mathf.Round( Random.Range(amt - 10, amt + 10) );
        print($"new score is {Score}");

    }
    public void IncreaseScore(float amt)
    {
        print($"increasing score, old score is {Score}");
        Score += Mathf.Round( Random.Range(amt - 10, amt + 10) );
        print($"new score is {Score}");
    }
}
