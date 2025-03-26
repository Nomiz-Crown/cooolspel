using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mchp : MonoBehaviour
{
    [Range(0, 100)] public float TemperatureHealth = 0;
    public GameObject death; //death är gameobject med death animation btw

    // Start is called before the first frame update
    void Start()
    {
        if (death != null)
        {
            death.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // debug, du kan ta bort om du vill

        if (TemperatureHealth >= 100)
        {
            TemperatureHealth = 100;
            Die();
        }
    }

    void Die()
    {
        // mer debug

        gameObject.SetActive(false);

        if (death != null)
        {
            death.transform.parent = null;

            death.SetActive(true);
            death.transform.position = transform.position; 
            death.transform.rotation = transform.rotation; 

            // Deeeeeebuuuug
        }
    }
    void TakeDamage(float amount)
    {
        TemperatureHealth += amount;
    }
     public void RestoreHealth(float amount)
    {
        TemperatureHealth -= amount;
    }
}
