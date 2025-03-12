using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenugraphicstuff : MonoBehaviour
{
    public GameObject[] objectsToSpawn; // inspector grejer yk yk
    public float moveSpeed = 5f; //du vet vad move speed är -_-
    public float spawnInterval = 2f; //spawnrate ig
    public float destroyX = -10f; //när dem blir destroyade ta typ minst -20

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObject()
    {
        if (objectsToSpawn.Length == 0) return;

        
        GameObject original = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

       
        GameObject clone = Instantiate(original, original.transform.position, Quaternion.identity);

        
        StartCoroutine(MoveAndDestroy(clone));
    }

    IEnumerator MoveAndDestroy(GameObject obj)
    {
        while (obj != null && obj.transform.position.x > destroyX)
        {
            obj.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            yield return null; 
        }

        if (obj != null)
            Destroy(obj); 
    }
}