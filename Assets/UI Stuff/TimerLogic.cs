using TMPro;
using UnityEngine;

public class TimerLogic : MonoBehaviour
{
    private realTimer TimerTMP;

    float secondsPassed = 0;
    string secondsToDisplay;
    float tempvalue = 0;
    float minutesPassed = 0;
    string minutesToDisplay;
    // Start is called before the first frame update
    void Start()
    {
        TimerTMP = FindObjectOfType<realTimer>();

        if (TimerTMP == null) print("uuuooooohhhh2");
    }

    // Update is called once per frame
    void Update()
    {
        TimeyWimeyWibblyWobblyStuff();
        UpdateTimeToDisplay();
        UpdateText();
    }
    void UpdateTimeToDisplay()
    {
        float SecondPassedRounded = Mathf.Round(tempvalue * 100) / 100;
        if (SecondPassedRounded < 10) secondsToDisplay = $"0{SecondPassedRounded}";
        else if (SecondPassedRounded > 10) secondsToDisplay = $"{SecondPassedRounded}";

        if (minutesPassed < 10) minutesToDisplay = $"0{minutesPassed}";
        else if (minutesPassed > 10) minutesToDisplay = $"{minutesPassed}";
    }
    void UpdateText()
    {
        TimerTMP.displayMe = ($"{minutesToDisplay}:{secondsToDisplay}");
        TimerTMP.secondsPassed = secondsPassed;
    }
    void TimeyWimeyWibblyWobblyStuff()
    {
        SecondStuff();
        FormatSeconds();
    }
    void SecondStuff()
    {
        secondsPassed += Time.deltaTime;
        if(secondsPassed >= 60)
        {
            minutesPassed = Mathf.Floor(secondsPassed / 60);
        }
    }
    void FormatSeconds()
    {
        tempvalue = secondsPassed - (60 * minutesPassed);
    }
    public void ReduceTime(float time)
    {
        secondsPassed -= time;
    }
}
