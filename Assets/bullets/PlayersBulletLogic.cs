using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersBulletLogic : MonoBehaviour
{
    public GameObject obj;
    Vector3 mypos;
    public  float bulletLifeTime;
    float timer = 0;
    CameraMoveToPlayer cam1;
    // Start is called before the first frame update
    void Start()
    {
        cam1 = FindObjectOfType<CameraMoveToPlayer>();
        if (cam1 == null) Debug.LogWarning("unable to find camera. screenshake disabled");
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
        cam1.ShakeCamera();
        Destroy(gameObject);
    }
    void InstExplosion()
    {
        GameObject clone = Instantiate(obj);
        clone.transform.position = mypos;
    }
}
