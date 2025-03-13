using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitdetection : MonoBehaviour
{
    GooberBehaviour goob;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<GooberBehaviour>() != null)
        {
            goob = GetComponent<GooberBehaviour>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<GooberBehaviour>() != null)
        {
            goob = collision.gameObject.GetComponent<GooberBehaviour>();
        }
        else
        {
            //other;
        }
        if (collision.gameObject.CompareTag("Enemy") && goob.isLunging )
        {
            print("player should have taken damage");
        }
    }
}
