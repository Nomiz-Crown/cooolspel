using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fister : MonoBehaviour
{
    private List<GameObject> bulletListToParry = new();
    [HideInInspector] public bool isBulletAvailableToParry = false;

    private List<GameObject> objectsMaybeParry = new();

    public GameObject parriedBulletPrefab;
    public float ParriedBulletVelocityMultiplier;

    private PerformanceTallyLogicV1 tally;
    [SerializeField] private float myDamage;
    public float knockbackForce;
    [SerializeField] private GameObject parryVfx;
    private float parryTime;
    mchp me;
    MCAnimationV2 animOverride;
    private void Start()
    {
        parryTime = armCooldown;
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

        GooberBehaviour bomba = collision.gameObject.GetComponent<GooberBehaviour>();

        if (bomba == null) return;

        if (bomba.canParry && !objectsMaybeParry.Contains(bomba.gameObject))
        {
            objectsMaybeParry.Add(bomba.gameObject);
        }
        else if (bomba.canParry && objectsMaybeParry.Contains(bomba.gameObject))
        {
            objectsMaybeParry.Remove(bomba.gameObject);
        }

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
            objectsMaybeParry.Remove(collision.gameObject);
        }
    }
    void UpdateParryableObject()
    {
        Physics2D.OverlapCollider(GetComponent<PolygonCollider2D>(), filter, results);

        foreach (Collider2D cool in results)
        {
            if (objectsMaybeParry.Contains<GameObject>(cool.gameObject) || !cool.gameObject.CompareTag("Enemy")) continue;
            objectsMaybeParry.Add(cool.gameObject);
        }
    }
    private void HandleInput()
    {
        if (!FOnCooldown("nah")) return;

        UpdateParryableObject();

        if (Input.GetKeyDown(KeyCode.F))
        {
            int o = 0;
            if (isBulletAvailableToParry)
            {
                if (Parry()) o++;
            }
            if (objectsMaybeParry.Count > 0)
            {
                if (getthisbozoouttahere()) o++;
            }
            if (o > 0)
            {
                if (!animOverride.isParry)
                {
                    animOverride.isParry = true;
                    Invoke(nameof(Skibidi), parryTime);
                }
                StartCoroutine(DoSlowMotion());
            }
            FOnCooldown("reset");
        }
    }
    void Skibidi()
    {
        animOverride.isParry = false;
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
    private bool getthisbozoouttahere()
    {
        if (objectsMaybeParry.Count == 0) return false; // Check if punchLine is empty

        GameObject bozoToParry = null;
        GooberBehaviour gooberToParry = null;

        for (int i = 0; i < objectsMaybeParry.Count; i++)
        {
            if (objectsMaybeParry[i] == null)
            {
                objectsMaybeParry.Remove(objectsMaybeParry[i]);
                continue;
            }
            if((gooberToParry = objectsMaybeParry[i].GetComponent<GooberBehaviour>()) != null)
            {
                bozoToParry = objectsMaybeParry[i];
                continue;
            }
        }

        if (bozoToParry == null || !gooberToParry.canParry) return false; //return if no goober to parry was found OR if cant parry goober
        
        objectsMaybeParry.Remove(bozoToParry);
        Quaternion bozoRotation = bozoToParry.transform.rotation;
        GameObject newBozo = Instantiate(gooberMissile);
        newBozo.transform.position = bozoToParry.transform.position;

        Rigidbody2D gooberBody = bozoToParry.GetComponent<Rigidbody2D>();
        if (gooberBody != null)
        {
            Vector2 bozoVelocity = gooberBody.velocity;
            Destroy(bozoToParry);
            Rigidbody2D bozoMissile = newBozo.GetComponent<Rigidbody2D>();
            if (bozoMissile != null)
            {
                bozoMissile.velocity = -bozoVelocity * ParriedBulletVelocityMultiplier;
                newBozo.transform.rotation = bozoRotation;
            }
        }
        tally.UpdateTally("+ IM WALKIN' HERE", "Add");
        if (me.TemperatureHealth > 0)
        {
            me.RestoreHealth(35);
        }
        return true;
    }
    private bool Parry()
    {

        if (bulletListToParry.Count == 0) return false;

        GameObject bulletToParry = bulletListToParry[0];
        Vector2 bulletPosition = bulletToParry.transform.position;
        Rigidbody2D bulletRigidbody = bulletToParry.GetComponent<Rigidbody2D>();

        if (bulletRigidbody == null) return false;

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
        return true;
    }

    private IEnumerator DoSlowMotion()
    {
        float originalTimeScale = Time.timeScale;
        float slowDownFactor = 0f;
        float duration = 0.3f;

        if (parryVfx != null)
            parryVfx.SetActive(true);

        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = originalTimeScale;
        Time.fixedDeltaTime = 0.02f;

        if (parryVfx != null)
            parryVfx.SetActive(false);
    }


    private void Punch2()
    {
        if (objectsMaybeParry.Count == 0) return;

        // explenation lite mer ner orka skriva här
        objectsMaybeParry.RemoveAll(col => col == null || !col.gameObject.activeInHierarchy);

        if (objectsMaybeParry.Count == 0) return;

        EnemyHealth thisbozo = objectsMaybeParry[0].GetComponent<EnemyHealth>();
        if (thisbozo != null)
        {
            if (thisbozo.InflictDamage(myDamage))
            {
                objectsMaybeParry.RemoveAt(0);  //den tar bort enemie colliders när dem dör
                me.RestoreHealth(40);
            }
            else
            {
                me.RestoreHealth(10);
            }

            tally.UpdateTally("+ LEE-SIN WANNA-BE", "Add");

            // Apply knockback
            Vector2 knockbackDirection = (objectsMaybeParry[0].transform.position - transform.position).normalized;
            Rigidbody2D rb = objectsMaybeParry[0].gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(-knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }
}