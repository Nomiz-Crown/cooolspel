using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessStuff : MonoBehaviour
{
    public GameObject followerObject;

    private Transform playerTransform;
    private bool shouldFollow = false;

    void Update()
    {
        if (shouldFollow && playerTransform != null)
        {
            followerObject.transform.position = playerTransform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            followerObject.SetActive(true);
            shouldFollow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            shouldFollow = false;
            followerObject.SetActive(false);
            playerTransform = null;
        }
    }

    public void DeactivateAll()
    {
        followerObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
