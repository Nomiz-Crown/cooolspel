using TMPro;
using UnityEngine;

public class TimerLogic : MonoBehaviour
{
    public GameObject Timer;
    private TextMeshProUGUI TimerTMP;

    float secondsPassed = 0;
    string secondsToDisplay;
    float minutesPassed = 0;
    string minutesToDisplay;
    // Start is called before the first frame update
    void Start()
    {
        TimerTMP = Timer.GetComponent<TextMeshProUGUI>();
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
        float smeg = Mathf.Round(secondsPassed * 100) / 100;
        if (smeg < 10) secondsToDisplay = $"0{smeg}";
        else if (smeg > 10) secondsToDisplay = $"{smeg}";

        if (minutesPassed < 10) minutesToDisplay = $"0{minutesPassed}";
        else if (minutesPassed > 10) minutesToDisplay = $"{minutesPassed}";
    }
    void UpdateText()
    {
        TimerTMP.text = ($"{minutesToDisplay}:{secondsToDisplay}");
    }
    void TimeyWimeyWibblyWobblyStuff()
    {
        SecondStuff();
    }
    void SecondStuff()
    {
        secondsPassed += Time.deltaTime;
        if(secondsPassed >= 60) { minutesPassed++; secondsPassed = 0; }
    }
}
