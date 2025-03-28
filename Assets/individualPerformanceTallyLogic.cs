using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class individualPerformanceTallyLogic : MonoBehaviour
{
    TextMeshProUGUI tmp;
    SummonTally myList;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        myList = GetComponentInParent<SummonTally>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int index = myList.flip.IndexOf(gameObject);
        transform.position = new Vector3(transform.position.x, index * 50);
    }
    public void UpdateAct(string act)
    {
        tmp.text = act;
    }
}
