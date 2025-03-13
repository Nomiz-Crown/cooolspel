using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fryingPanStuffV1 : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouse();   
    }
    void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Input.mousePosition;
            TossFryingPan(mousePosition);
        }
    }
    void TossFryingPan(Vector2 Mouse)
    {
        SpawnObject(CalculateAngle(Mouse, transform.position));
    }
    float CalculateAngle(Vector2 a, Vector2 b)
    {
        Vector2 direction = b - a;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }
    void SpawnObject(float spawnAngle)
    {
        GameObject spawnedObject = Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        Vector2 direction = Quaternion.Euler(0, 0, spawnAngle) * Vector2.right;
        spawnedObject.GetComponent<Rigidbody2D>().velocity = direction * spawnVelocity;
    }
}
