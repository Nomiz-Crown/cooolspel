using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersBulletLogic : MonoBehaviour
{
    public GameObject obj;
    Vector3 mypos;
    public  float bulletLifeTime;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void DestroyAfterExpire()
    {
        if (timer >= bulletLifeTime) { InstExplosion(); Destroy(gameObject); }
        else timer += Time.deltaTime;
    }
    // Update is called once per frame
    void Update()
    {
        mypos = transform.position;
        DestroyAfterExpire();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        InstExplosion();
        Destroy(gameObject);
    }
    void InstExplosion()
    {
        GameObject clone = Instantiate(obj);
        clone.transform.position = mypos;
    }
}
