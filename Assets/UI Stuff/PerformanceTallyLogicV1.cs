using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PerformanceTallyLogicV1 : MonoBehaviour
{
    TextMeshProUGUI tmp;
    List<string> hamstringer = new();
    [SerializeField] private float tallyDissapearTime;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        UpdateTally("flippy", "Add");
    }

    // Update is called once per frame
    void Update()
    {
        stuff();
        RemoveFirstTally();
    }
    void stuff()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            UpdateTally("flippy", "Add");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            UpdateTally("", "Update");
        }
    }
    public void UpdateTally(string Act, string Scenario)
    {
        if (Scenario == "Add")
        {
            tmp.text = "";
            hamstringer.Add(Act);
            for (int i = 0; i < hamstringer.Count; i++)
            {
                tmp.text += $"{hamstringer[i]}{i} \n";
            }
        }
        else if (Scenario == "Update")
        {
            tmp.text = "";
            for (int i = 0; i < hamstringer.Count; i++)
            {
                tmp.text += $"{hamstringer[i]}{i} \n";
            }
        }
    }
    void RemoveFirstTally()
    {
        if (TallyTimer() && hamstringer.Count != 0)
        {
            print("tallyTimer returned true and hamstringer contains atleast one enumeration so removing the hamstringer[0]");
            hamstringer.Remove(hamstringer[0]);
            timer = 0;
            UpdateTally("Update", "Update");
        }
    }
    bool TallyTimer()
    {
        if(!IsListEmpty(hamstringer))
        {
            if (timer > tallyDissapearTime)
            {
                print($" {timer} > {tallyDissapearTime}, so returning true");
                return true;
            }
            else
            {
                timer += Time.deltaTime;
                print($"timer {timer} < duration {tallyDissapearTime} so adding time to {timer}");
                return false;
            }
        }
        print($"list is empty, hamstringer[0] is null");
        return false;
    }
    public bool IsListEmpty(List<string> list)
    {
        return list.Count == 0;
    }
}
