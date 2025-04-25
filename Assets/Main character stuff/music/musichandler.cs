using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musichandler : MonoBehaviour
{
    public GameObject introstuff;
    public GameObject RestOfTheLEvelStuff;
    // Start is called before the first frame update
    void Start()
    {
        RestOfTheLEvelStuff.SetActive(false);
        introstuff.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            RestOfTheLEvelStuff.SetActive(true);
            introstuff.SetActive(false);
        }
    }
}
