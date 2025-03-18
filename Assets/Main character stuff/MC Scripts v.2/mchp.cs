using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mchp : MonoBehaviour
{
    [Range(0, 100)] public float hp = 100;
    public GameObject death; // Reference to the "death" GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Make sure 'death' is not active initially
        if (death != null)
        {
            death.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Debugging line to track the current HP
        Debug.Log("Current HP: " + hp);

        if (hp <= 0)
        {
            Debug.Log("HP is 0, triggering death!");
            hp = 0;
            Die(); // Call the Die function when HP reaches 0
        }
    }

    // This function handles deactivating the current object and activating the "death" GameObject
    void Die()
    {
        // Debugging line to check if this function is being triggered
        Debug.Log("Die() function triggered!");

        // Deactivate the current object (the parent)
        gameObject.SetActive(false);

        // Activate the "death" GameObject
        if (death != null)
        {
            // Detach the "death" GameObject from its parent to keep it active
            death.transform.parent = null;

            death.SetActive(true); // Activate the "death" GameObject
            death.transform.position = transform.position; // Set the "death" GameObject's position to match the current object's position
            death.transform.rotation = transform.rotation; // Set the "death" GameObject's rotation to match the current object's rotation

            // Debugging line to verify the death object is correctly activated
            Debug.Log("Death object activated at position: " + death.transform.position);
        }
        else
        {
            Debug.LogError("Death object not assigned!");
        }
    }
}
