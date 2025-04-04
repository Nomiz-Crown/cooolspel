using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    new Collider2D collider;
    public float myHealth;
    private PerformanceTallyLogicV1 tally;

    // Start is called before the first frame update
    void Start()
    {
        tally = FindObjectOfType<PerformanceTallyLogicV1>();
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

    // Update is called once per frame
    void Update()
    {
        CheckDead();
    }
    public void Kys(string cond)
    {
        tally.UpdateTally($"+ {cond}", "Add");
        Destroy(gameObject);
    }
    private void CheckDead()
    {
        if (myHealth <= 0)
        {
            tally.UpdateTally("+ MANSLAUGHTER", "Add");
            Destroy(gameObject);
        }
    }
}
