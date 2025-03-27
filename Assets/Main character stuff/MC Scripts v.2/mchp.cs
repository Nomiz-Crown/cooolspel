using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mchp : MonoBehaviour
{
    [Range(0, 100)] public float TemperatureHealth = 0;
    public GameObject scoreThingy;
    private StyleLogic score;
    public GameObject death; //death är gameobject med death animation btw

    // Start is called before the first frame update
    void Start()
    {
        score = scoreThingy.GetComponentInChildren<StyleLogic>();
        if (score == null) print("uooohhh");
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
    public void TakeDamage(float amount)
    {
        TemperatureHealth += Random.Range(amount - 10, amount + 10);
        score.ReduceScore(100);
    }
    public void RestoreHealth(float amount)
    {
        TemperatureHealth -= Random.Range(amount-10, amount+10);
    }
}
