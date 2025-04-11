using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationsTozzer : MonoBehaviour
{
    Animator anim;
    TosserScript tosser;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        tosser = GetComponent<TosserScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tosser != null)
        {
            if (tosser.canShoot)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
                {
                    anim.SetTrigger("Shoot");
                }
            }
            else
            {
                anim.SetTrigger("Idle");
            }
        }
    }
}
