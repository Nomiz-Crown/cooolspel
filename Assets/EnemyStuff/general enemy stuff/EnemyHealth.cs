using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    new Collider2D collider;
    public float myHealth;
    public GameObject canvas;
    private PerformanceTallyLogicV1 tally;

    // Start is called before the first frame update
    void Start()
    {
        tally = canvas.GetComponentInChildren<PerformanceTallyLogicV1>();
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
    public void Kys()
    {
        Destroy(gameObject);
    }
    private void CheckDead()
    {
        if (myHealth <= 0)
        {
            tally.UpdateTally("+ NATURAL CAUSES", "Add");
            Destroy(gameObject);
        }
    }
}
