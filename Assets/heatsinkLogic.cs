using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class heatsinkLogic : MonoBehaviour
{
    mchp man;
    float timer = 0;
    public float blinkingSpeed;
    TextMeshProUGUI tmp;
    bool isRed = false;
    // Start is called before the first frame update
    void Start()
    {
        man = FindObjectOfType<mchp>();
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (man.tutorialPassed)
        {
            if (TimeTimer())
            {
                Blink();
            }
        }
    }
    bool TimeTimer()
    {
        if (timer >= blinkingSpeed)
        {
            return true;
        }
        else
        {
            timer += Time.deltaTime;
            return false;
        }
    }
    void Blink()
    {
        if (!isRed)
        {
            tmp.color = Color.red;
            isRed = true;
            blinkingSpeed *= 5;
        }
        else if (isRed)
        {
            tmp.color = Color.clear;
            isRed = false;
            blinkingSpeed /= 5;
        }
        timer = 0;
    }
}
