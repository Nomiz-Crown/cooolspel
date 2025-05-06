using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PerformanceTallyLogicV1 : MonoBehaviour
{
    TextMeshProUGUI tmp;
    List<string> TallyList = new();
    [SerializeField] private float tallyDisappearTime;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        RemoveFirstTally();
    }
    public void UpdateTally(string Act, string Scenario)
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = "";
        if (Scenario == "Add")
        {
            TallyList.Add(Act);
            for (int i = 0; i < TallyList.Count; i++)
            {
                tmp.text += $"{TallyList[i]} \n";
            }
        }
        else if (Scenario == "Update")
        {
            for (int i = 0; i < TallyList.Count; i++)
            {
                tmp.text += $"{TallyList[i]} \n";
            }
        }
    }
    void RemoveFirstTally()
    {
        if (TallyTimer() && TallyList.Count != 0)
        {
            TallyList.Remove(TallyList[0]);
            timer = 0;
            UpdateTally("", "Update");
        }
    }
    bool TallyTimer()
    {
        if(!IsListEmpty(TallyList))
        {
            if (timer > tallyDisappearTime)
            {
                return true;
            }
            else
            {
                timer += Time.deltaTime;
                return false;
            }
        }
        return false;
    }
    public bool IsListEmpty(List<string> list)
    {
        return list.Count == 0;
    }
}
