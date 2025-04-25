using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musichandler : MonoBehaviour
{
    public GameObject introstuff;
    public GameObject RestOfTheLEvelStuff;
    public CourtMusic courtMusicScript;

    void Start()
    {
        RestOfTheLEvelStuff.SetActive(false);
        introstuff.SetActive(true);
    }

    void Update()
    {
        if (courtMusicScript != null && courtMusicScript.hasTouched)
        {
            RestOfTheLEvelStuff.SetActive(false);
        }
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
