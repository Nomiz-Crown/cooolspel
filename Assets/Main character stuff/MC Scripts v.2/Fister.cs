using System.Collections.Generic;
using UnityEngine;

public class Fister : MonoBehaviour
{
    private List<GameObject> bulletListToParry = new();
    private bool isBulletAvailableToParry = false;

    private List<Collider2D> punchLine = new();
    private bool goonToPunch = false;


    public GameObject parriedBulletPrefab;
    public float ParriedBulletVelocityMultiplier;

    private PerformanceTallyLogicV1 tally;
    public Collider smeg;

    mchp me;
    private void Start()
    {
        tally = FindObjectOfType<PerformanceTallyLogicV1>();
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
        else if (collision.CompareTag("Enemy"))
        {
            goonToPunch = true;
            punchLine.Add(collision);
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
        if (collision.CompareTag("Enemy"))
        {
            for (int i = 0; i < punchLine.Count; i++)
            {
                if (punchLine[i] == collision.gameObject)
                {
                    punchLine.Remove(punchLine[i]);
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
            newBulletRigidbody.velocity = -bulletVelocity * ParriedBulletVelocityMultiplier;
        }

        if (bulletListToParry.Count == 0) return;
        tally.UpdateTally("+ TILLBAKA-KAKA", "Add");

        bulletListToParry.RemoveAt(0);
        Destroy(bulletToParry);

        isBulletAvailableToParry = bulletListToParry.Count > 0;
        if (bulletListToParry.Count <= 0) isBulletAvailableToParry = false;
        // Update the state

        if (me.TemperatureHealth <= 0) return;
        me.RestoreHealth(20);

    }

    private void Punch()
    {
        me.RestoreHealth(10);
        tally.UpdateTally("+ SUCKER PUNCH", "Add");
    }
}
