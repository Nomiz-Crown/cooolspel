using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//handle damaging playa;
public class followParent : MonoBehaviour
{
    [HideInInspector] public bool touchingPlayer;
    // Start is called before the first frame update
    void Start()
    {
        touchingPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            touchingPlayer = false;
        }
    }

}
