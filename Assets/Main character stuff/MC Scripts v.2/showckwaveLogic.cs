using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class showckwaveLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float deleteAfter;
    float timer;
    // Update is called once per frame
    void Update()
    {
        smeg();
    }
    void smeg()
    {
        if (timer >= deleteAfter) Destroy(gameObject);
        else timer += Time.deltaTime;
    }
}