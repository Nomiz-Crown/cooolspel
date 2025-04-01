using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StyleLogicV3 : MonoBehaviour
{
    TextMeshProUGUI tmp;
    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tmp.text = $"{score}";
    }
    public void AdjustScore(int amt)
    {
        score += amt;
    }
}
