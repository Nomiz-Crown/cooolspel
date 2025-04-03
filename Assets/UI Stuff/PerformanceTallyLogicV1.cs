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
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = "";
        if (Scenario == "Add")
        {
            hamstringer.Add(Act);
            for (int i = 0; i < hamstringer.Count; i++)
            {
                tmp.text += $"{hamstringer[i]} \n";
            }
        }
        else if (Scenario == "Update")
        {
            for (int i = 0; i < hamstringer.Count; i++)
            {
                tmp.text += $"{hamstringer[i]} \n";
            }
        }
    }
    void RemoveFirstTally()
    {
        if (TallyTimer() && hamstringer.Count != 0)
        {
            hamstringer.Remove(hamstringer[0]);
            timer = 0;
            UpdateTally("", "Update");
        }
    }
    bool TallyTimer()
    {
        if(!IsListEmpty(hamstringer))
        {
            if (timer > tallyDissapearTime)
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
