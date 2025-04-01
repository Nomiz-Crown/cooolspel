using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletLogic : MonoBehaviour
{
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.CompareTag("Bullet"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                mchp guyObject = collision.gameObject.GetComponent<mchp>();
                guyObject.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
