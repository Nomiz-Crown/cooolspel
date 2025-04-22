using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Fister : MonoBehaviour
{
    private List<GameObject> bulletListToParry = new();
    public bool isBulletAvailableToParry = false;

    private List<GameObject> punchLine = new();

    public GameObject parriedBulletPrefab;
    public float ParriedBulletVelocityMultiplier;

    private PerformanceTallyLogicV1 tally;
    [SerializeField] private float myDamage;
    public float knockbackForce;
    [SerializeField] private GameObject mushy;

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
        if (!collision.gameObject.CompareTag("Enemy")) return;

        GameObject snake = collision.gameObject;
        GooberBehaviour bomba = snake.GetComponent<GooberBehaviour>();

        if (bomba == null) return;

        if (bomba.canParry && !punchLine.Contains(bomba.gameObject)) punchLine.Add(bomba.gameObject);
        else if (bomba.canParry && punchLine.Contains(bomba.gameObject)) punchLine.Remove(bomba.gameObject);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            bulletListToParry.Remove(collision.gameObject);
            isBulletAvailableToParry = bulletListToParry.Count > 0;
        }
        else if (collision.gameObject.CompareTag("Enemy") && collision.gameObject.GetComponent<GooberBehaviour>() != null)
        {
            punchLine.Remove(collision.gameObject);
        }
    }

    private void HandleInput()
    {
        if (!FOnCooldown("nah")) return;
        Physics2D.OverlapCollider(GetComponent<PolygonCollider2D>(), filter, results);
        foreach(Collider2D cool in results)
        {
            if (punchLine.Contains<GameObject>(cool.gameObject)) return;
            punchLine.Add(cool.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isBulletAvailableToParry)
            {
                Parry();
                animOverride.isParry = true;
            }
            if (punchLine.Count > 0)
            {
                print(results[0]);
                getthisbozoouttahere();
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
    public GameObject gooberMissile;
    private void getthisbozoouttahere()
    {
        GameObject bozo = punchLine[0];
        Quaternion bozoRotation = punchLine[0].transform.rotation;
        GameObject newBozo = Instantiate(gooberMissile);
        newBozo.transform.position = punchLine[0].transform.position;
        punchLine.Remove(bozo);
        Rigidbody2D gooberBody = punchLine[0].GetComponent<Rigidbody2D>();
        Vector2 bozoVelocity = gooberBody.velocity;
        Destroy(punchLine[0]);
        Rigidbody2D bozoMissile = newBozo.GetComponent<Rigidbody2D>();
        bozoMissile.velocity = -bozoVelocity * ParriedBulletVelocityMultiplier;
        newBozo.transform.rotation = bozoRotation;
    }
    private void Parry()
    {
        StartCoroutine(DoSlowMotion());

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

        isBulletAvailableToParry = bulletListToParry.Count > 0;
        tally.UpdateTally("+ TILLBAKA-KAKA", "Add");

        if (me.TemperatureHealth > 0)
        {
            me.RestoreHealth(35);
        }
    }

    private IEnumerator DoSlowMotion()
    {
        float originalTimeScale = Time.timeScale;
        float slowDownFactor = 0f;
        float duration = 0.3f;

        if (mushy != null)
            mushy.SetActive(true);

        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = 0.02f;

        if (mushy != null)
            mushy.SetActive(false);
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

            tally.UpdateTally("+ LEE-SIN WANNA-BE", "Add");

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