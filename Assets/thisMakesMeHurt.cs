using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class thisMakesMeHurt : MonoBehaviour
{
    AirbornePanLogic fryingPan;
    [SerializeField] private float myDamage = 5f;
    EnemyHealth flippy;
    // Start is called before the first frame update
    void Start()
    {
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
            print("i touch enemy and im lethal)");
            grabComponent(collision.gameObject);
            if (flippy != null)
            {
                InflictDamage();
                CheckIfDead();
            }
        }
    }
    void grabComponent(GameObject boy)
    {
        if (boy.GetComponent<EnemyHealth>() != null)
        {
            flippy = boy.GetComponent<EnemyHealth>();
            print("got health component from " + boy.name);
        }
    }
    void InflictDamage()
    {
        flippy.myHealth -= myDamage;
        print($"done {myDamage} to enemy health bar, which is now {flippy.myHealth}");
    }
    void CheckIfDead()
    {
        if (flippy.myHealth <= 0)
        {
            flippy.gameObject.SendMessage("Die");
            print("die");
        }
        else
        {
            fryingPan.isLethal = false;
        }
    }
}
