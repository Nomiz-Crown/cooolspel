using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonTally : MonoBehaviour
{
    [SerializeField] GameObject tally;
    [HideInInspector] public List<GameObject> flip = new List<GameObject>();
    float timer;
    [SerializeField] private float Duration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DeleteFirstAfter(Duration);
    }
    public void Summon(string act)
    {
        GameObject clone = Instantiate(tally, transform);
        individualPerformanceTallyLogic bingo = clone.GetComponent<individualPerformanceTallyLogic>();
        flip.Add(clone);
        bingo.UpdateAct(act);
    }

    void DeleteFirstAfter(float time)
    {
        if (flip.Count > 0)
        {
            if (timer > time)
            {
                Destroy(flip[0]);
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            timer = 0;
        }
    }
}
