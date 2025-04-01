using System.Collections.Generic;
using UnityEngine;

public class Fister : MonoBehaviour
{
    private List<GameObject> bulletListToParry = new List<GameObject>();
    private bool isBulletAvailableToParry;

    public GameObject parriedBulletPrefab;
    public float ParriedBulletVelocityMultiplier;

    public GameObject canvas;
    private PerformanceTallyLogicV1 tally;

    mchp me;
    private void Start()
    {
        tally = canvas.GetComponentInChildren<PerformanceTallyLogicV1>();
        me = GetComponent<mchp>();
    }
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            for (int i = 0; i < bulletListToParry.Count; i++)
            {
                if (bulletListToParry[i] == collision.gameObject)
                {
                    bulletListToParry.Remove(bulletListToParry[i]);
                }
            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isBulletAvailableToParry)
            {
                Parry();
                me.RestoreHealth(20);
                tally.UpdateTally("+ TILLBAKA-KAKA", "Add");
                print("send tillbaka-kaka to tally");
            }
            else
            {
                Punch();
                me.RestoreHealth(10);
                tally.UpdateTally("+ SUCKER PUNCH", "Add");
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
            newBulletRigidbody.velocity = -bulletVelocity * ParriedBulletVelocityMultiplier;
        }
        bulletListToParry.RemoveAt(0);
        Destroy(bulletToParry);
        isBulletAvailableToParry = bulletListToParry.Count > 0; // Update the state
    }

    private void Punch()
    {
        // Implement punch logic here
    }
}
