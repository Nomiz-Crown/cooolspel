using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fryingPanAnimation : MonoBehaviour
{
    Animator anim;
    AirbornePanLogic pan;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        pan = GetComponent<AirbornePanLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pan.isLethal)
        {
            anim.Play("Spin");
        }
        else
        {
            anim.Play("idle");
        }
    }
}
