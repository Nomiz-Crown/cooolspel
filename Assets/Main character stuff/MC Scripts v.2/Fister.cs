using System.Collections.Generic;
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
    MCAnimationV2 animOverride;
    private void Start()
    {
        tally = FindObjectOfType<PerformanceTallyLogicV1>();
        me = GetComponent<mchp>();
        animOverride = GetComponent<MCAnimationV2>();
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
                if (!punchLine.Contains(col))  // den addar bara nya colliders om dem inte redan fins
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
        if (!FOnCooldown("nah")) return;
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
            animOverride.isPunch = true;
            FOnCooldown("reset");
        }
    }
    [SerializeField] float armCooldown;
    float timer = 0;
    private bool FOnCooldown(string resetCond)
    {
        if (resetCond == "reset")
        {
            timer = 0;
        }
        else if (timer < armCooldown) timer += Time.deltaTime;
        return timer >= armCooldown;
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

        // explenation lite mer ner orka skriva här
        punchLine.RemoveAll(col => col == null || !col.gameObject.activeInHierarchy);

        if (punchLine.Count == 0) return;

        EnemyHealth thisbozo = punchLine[0].GetComponent<EnemyHealth>();
        if (thisbozo != null)
        {
            if (thisbozo.InflictDamage(myDamage))
            {
                punchLine.RemoveAt(0);  //den tar bort enemie colliders när dem dör
                me.RestoreHealth(40);
            }
            else
            {
                me.RestoreHealth(10);
            }

            tally.UpdateTally("+ Lee Sin abuser", "Add");

            // Apply knockback
            Vector2 knockbackDirection = (punchLine[0].transform.position - transform.position).normalized;
            Rigidbody2D rb = punchLine[0].gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(-knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}