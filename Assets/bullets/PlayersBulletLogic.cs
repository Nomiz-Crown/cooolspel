using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersBulletLogic : MonoBehaviour
{
    List<GameObject> enemies;
    GameObject nearestEnemy;
    public GameObject obj;
    Vector3 mypos;
    // Start is called before the first frame update
    void Start()
    {
        /*
        foreach(GameObject flipp in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(flipp);
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        mypos = transform.position;
    }
    private void FixedUpdate()
    {
        /*
        foreach (GameObject flippy in enemies)
        {
            print("zzzzzzzz");
        }
        */
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       GameObject clone = Instantiate(obj);
       clone.transform.position = mypos;
    }
}
