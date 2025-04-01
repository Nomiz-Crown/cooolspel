using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersBulletLogic : MonoBehaviour
{
    EnemyHealth enemyToDamage;
    public GameObject obj;
    Vector3 mypos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mypos = transform.position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            InstExplosion();
            GankEnemyHealth(collision);
        }
    }
    void InstExplosion()
    {
        GameObject clone = Instantiate(obj);
        clone.transform.position = mypos;
    }
    void GankEnemyHealth(Collision2D Enemy)
    {
        if(Enemy.gameObject.GetComponent<EnemyHealth>() != null)
        {
            enemyToDamage = Enemy.gameObject.GetComponent<EnemyHealth>();
        }
    }
}
