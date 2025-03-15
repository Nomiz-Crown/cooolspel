using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersBulletLogic : MonoBehaviour
{
    List<GameObject> enemies;
    GameObject nearestEnemy;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject flipp in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemies.Add(flipp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        foreach (GameObject flippy in enemies)
        {
            print("zzzzzzzz");
        }
    }
}
