using System.Collections.Generic;
using UnityEngine;

public class Fister : MonoBehaviour
{
    private List<GameObject> bulletListToParry = new List<GameObject>();
    private bool isBulletAvailableToParry;
    public GameObject parriedBulletPrefab;

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            isBulletAvailableToParry = true;
            bulletListToParry.Add(collision.gameObject);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isBulletAvailableToParry)
            {
                Parry();
            }
            else
            {
                Punch();
            }
        }
    }

    private void Parry()
    {
        if (bulletListToParry.Count == 0) return;

        GameObject bulletToParry = bulletListToParry[0];
        Vector2 bulletPosition = bulletToParry.transform.position;
        Rigidbody2D bulletRigidbody = bulletToParry.GetComponent<Rigidbody2D>();

        if (bulletRigidbody == null) return;

        Vector2 bulletVelocity = bulletRigidbody.velocity;
        GameObject newBullet = Instantiate(parriedBulletPrefab, bulletPosition, Quaternion.identity);
        Rigidbody2D newBulletRigidbody = newBullet.GetComponent<Rigidbody2D>();

        if (newBulletRigidbody != null)
        {
            newBulletRigidbody.velocity = -bulletVelocity;
        }

        bulletListToParry.RemoveAt(0);
        isBulletAvailableToParry = bulletListToParry.Count > 0; // Update the state
    }

    private void Punch()
    {
        // Implement punch logic here
    }
}
