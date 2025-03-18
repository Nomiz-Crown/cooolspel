using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mchp : MonoBehaviour
{
    [Range(0, 100)] public float hp = 100;
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

        if (hp <= 0)
        {
            hp = 0;
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
            Debug.Log(death.transform.position);
        }
    }
    void TakeDamage(float damage)
    {
        hp-=damage;
    }
}
