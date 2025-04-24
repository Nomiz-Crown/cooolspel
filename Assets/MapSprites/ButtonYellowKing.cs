using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonYellowKing : MonoBehaviour
{
    public TextMeshProUGUI statusText;
    public GameObject doorToDelete;

    private bool playerInTrigger = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.yellow;
            }

            if (statusText != null)
            {
                statusText.text = "A door has opened";
            }

            if (doorToDelete != null)
            {
                Destroy(doorToDelete);
            }

            playerInTrigger = false; // Optional: prevent retriggering if needed
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }
}
