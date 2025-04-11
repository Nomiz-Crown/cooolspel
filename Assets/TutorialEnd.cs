using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject guy = collision.gameObject;
        if (guy.CompareTag("Player"))
        {
            mchp hp = guy.GetComponent<mchp>();
            hp.tutorialPassed = true;
        }
    }
}
