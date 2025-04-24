using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationsTozzer : MonoBehaviour
{
    Animator anim;
    TosserScript tosser;

    public float shootanimlength;
    float timer = 0;
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
            if (tosser.canShoot && tosser.HasClearLineOfSight())
            {
                anim.Play("Shoot");
                timer = 0;
            }
            else
            {
                if(ShootTimer()) anim.Play("Idle");
            }
        }
    }
    bool ShootTimer()
    {
        if (timer >= shootanimlength) return true;
        else
        {
            timer += Time.deltaTime;
            return false;
        }
    }
}
