using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mchp : MonoBehaviour
{
    [Range(0, 100)] public float hp = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 0) hp = 0;
    }
}