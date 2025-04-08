using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fister : MonoBehaviour
{
    private List<GameObject> bulletListToParry = new();
    private bool isBulletAvailableToParry = false;

    private List<Collider2D> punchLine = new();

    public GameObject parriedBulletPrefab;
    public float ParriedBulletVelocityMultiplier;

    private PerformanceTallyLogicV1 tally;
    [SerializeField] private float myDamage;
    public float knockbackForce;

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
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isBulletAvailableToParry = true;
            bulletListToParry.Add(collision.gameObject);
        }
    }

    ContactFilter2D filter = new ContactFilter2D().NoFilter();
    List<Collider2D> results = new();

    private void OnTriggerStay2D(Collider2D collision)
    {
        Physics2D.OverlapCollider(GetComponent<PolygonCollider2D>(), filter, results);
        foreach (Collider2D col in results)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                if (!punchLine.Contains(col))
                {
                    punchLine.Add(col);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletListToParry.Remove(collision.gameObject);
            isBulletAvailableToParry = bulletListToParry.Count > 0;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            punchLine.Remove(collision);
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
                Punch2();
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
        GameObject newBullet = Instantiate(parriedBulletPrefab, bulletPosition, Quaternion.identity, transform);
        Rigidbody2D newBulletRigidbody = newBullet.GetComponent<Rigidbody2D>();

        if (newBulletRigidbody != null)
        {
            newBulletRigidbody.velocity = -bulletVelocity * ParriedBulletVelocityMultiplier;
        }

        bulletListToParry.RemoveAt(0);
        Destroy(bulletToParry);

        isBulletAvailableToParry = bulletListToParry.Count > 0;
        tally.UpdateTally("+ TILLBAKA-KAKA", "Add");

        if (me.TemperatureHealth > 0)
        {
            me.RestoreHealth(35);
        }
    }

    private void Punch2()
    {
        if (punchLine.Count == 0) return;

        EnemyHealth thisbozo = punchLine[0].GetComponent<EnemyHealth>();
        if (thisbozo != null)
        {
            if (thisbozo.InflictDamage(myDamage))
            {
                punchLine.RemoveAt(0);
                me.RestoreHealth(40);
            }
            else
            {
                me.RestoreHealth(10);
            }

            tally.UpdateTally("+ SUCKER PUNCH", "Add");

            Vector2 knockbackDirection = (punchLine[0].transform.position - transform.position).normalized;
            Rigidbody2D rb = punchLine[0].gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(-knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}
