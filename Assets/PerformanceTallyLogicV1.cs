using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PerformanceTallyLogicV1 : MonoBehaviour
{
    TextMeshProUGUI tmp;
    List<string> hamstringer = new List<string>();
    [SerializeField] float tallyDisapearTime;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        NewTally("flippy");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            NewTally("flippy");
        }
        RemoveFirstTally();
    }
    void NewTally(string Act)
    {
        tmp.text = "";
        hamstringer.Add(Act);
        for (int i = 0; i < hamstringer.Count; i++)
        {
            tmp.text += $"{hamstringer[i]}{i} \n";
        }
    }
    void RemoveFirstTally()
    {
        if (TallyTimer() && !hamstringer.Any())
        {
            hamstringer.Remove(hamstringer[0]);
        }
    }
    bool TallyTimer()
    {
        if (timer > tallyDisapearTime)
        {
            timer = 0;
            return true;
        }
        else
        {
            timer += Time.deltaTime;
            return false;
        }
    }
}
