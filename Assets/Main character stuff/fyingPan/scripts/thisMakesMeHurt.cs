using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class thisMakesMeHurt : MonoBehaviour
{
    private PerformanceTallyLogicV1 tally;
    AirbornePanLogic fryingPan;
    [SerializeField] private float myDamage = 5f;
    EnemyHealth EnemyToDamage;
    // Start is called before the first frame update
    void Start()
    {
        tally = FindObjectOfType<PerformanceTallyLogicV1>();
        fryingPan = GetComponent<AirbornePanLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && fryingPan.isLethal) 
        {
            GrabComponent(collision.gameObject);
            if (EnemyToDamage != null)
            {
                InflictDamage();
                CheckIfDead();
            }
        }
    }
    void GrabComponent(GameObject obj)
    {
        if (obj.GetComponent<EnemyHealth>() != null)
        {
            EnemyToDamage = obj.GetComponent<EnemyHealth>();
        }
    }
    void InflictDamage()
    {
        EnemyToDamage.myHealth -= myDamage;
        tally.UpdateTally("+ P-l-anned", "Add");
    }
    void CheckIfDead()
    {
        if (EnemyToDamage.myHealth <= 0)
        {
            EnemyToDamage.Kys("Panetration");
        }
        else
        {
            fryingPan.isLethal = false;
        }
    }
}
