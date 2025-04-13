using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    new Collider2D collider;
    public float myHealth;
    private PerformanceTallyLogicV1 tally;
    private mchp hp;

    public GameObject deathEffectPrefab;
    public float effectLifetime = 2f;

    void Start()
    {
        tally = FindObjectOfType<PerformanceTallyLogicV1>();
        hp = FindObjectOfType<mchp>();
        if (tally == null)
        {
            print("uuooohhhhhhhhhhhhh");
        }
        collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            print("uuooohhhhhhhhhhhhh");
        }
    }

    void Update()
    {

    }

    public void Kys(string cond)
    {
        tally.UpdateTally($"+ {cond}", "Add");
        SpawnDeathEffect();
        Destroy(gameObject);
    }

    private void CheckDead()
    {
        if (myHealth <= 0)
        {
            tally.UpdateTally("+ MANSLAUGHTER", "Add");
            SpawnDeathEffect();
            Destroy(gameObject);
        }
    }

    public bool InflictDamage(float dmg)
    {
        myHealth -= dmg;
        if (myHealth == 0) return true;
        CheckDead();
        return false;
    }

    private void SpawnDeathEffect()
    {
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectLifetime);
        }
    }
}
