using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public float myDamage;
    GooberBehaviour goob;
    followParent trigger;
    mchp guessINeedThisGuyToo;
    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponentInChildren<followParent>();
        goob = GetComponent<GooberBehaviour>();
        guessINeedThisGuyToo = GameObject.FindGameObjectWithTag("Player").GetComponent<mchp>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDamage();
    }
    void CheckDamage()
    {
        if(trigger.touchingPlayer && goob.isLunging)
        {
            InflictDamage();
        }
    }
    void InflictDamage()
    {
        guessINeedThisGuyToo.hp -= myDamage;
        ResetConditions();
    }
    void ResetConditions()
    {
        goob.isLunging = false;
    }
}
