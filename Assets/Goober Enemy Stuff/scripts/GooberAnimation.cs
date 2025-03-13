using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberAnimation : MonoBehaviour
{
    Animator anim;
    GooberBehaviour me;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        me = GetComponent<GooberBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (me.isLunging)
        {
            anim.Play("GooberLunge");
        }
        else if (me.isIdle)
        {
            anim.Play("GooberI.dle");
        }
    }
}
