using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class trainingWheels : MonoBehaviour
{
    TextMeshProUGUI tmp;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = new Vector3(-6, 3, -2);
            i++;
            tmp.text = "rip x" + i/2;
        }
    }
}
