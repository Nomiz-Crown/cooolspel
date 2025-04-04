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
        print(punchLine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            isBulletAvailableToParry = true;
            bulletListToParry.Add(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            punchLine.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (bulletListToParry.Contains(collision.gameObject)) bulletListToParry.Remove(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if (punchLine.Contains(collision)) punchLine.Remove(collision);
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
        GameObject newBullet = Instantiate(parriedBulletPrefab, bulletPosition, Quaternion.identity, transform);
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
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new();

        Physics2D.OverlapCollider(GetComponent<PolygonCollider2D>(), filter, results);
        print("assigned value to results-list" + results);

        if (results.Count <= 0) return; 
        print("results was not empty");
        for (int i = 0; i < results.Count; i++)
        {
            if (results[i].CompareTag("Enemy") && results[i] != null && !punchLine.Contains(results[i]))
            {
                print("object was enemy, added to punchLine");
                punchLine.Add(results[i]);
            }
        }
        if (punchLine.Count <= 0) return;
        EnemyHealth thisbozo = punchLine[0].GetComponent<EnemyHealth>();
        if (thisbozo != null)
        {
            if (thisbozo.InflictDamage(myDamage))
            {
                punchLine.Remove(punchLine[0]);
                me.RestoreHealth(10);
                return;
            }
            print("thisbozo was not null so appied damage");
            me.RestoreHealth(5);
            tally.UpdateTally("+ SUCKER PUNCH", "Add");

            if (punchLine.Count <= 0) return;

            //play the punch anim

            if (punchLine.Count <= 0) return;

            Vector2 knockbackDirection = (punchLine[0].transform.position - transform.position).normalized;
            Rigidbody2D rb = punchLine[0].gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                print("thisbozos rigidbody was not null so applying force");
                rb.AddForce(-knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
        results = null;
    }
}
