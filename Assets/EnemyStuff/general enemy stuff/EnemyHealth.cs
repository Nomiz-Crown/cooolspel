using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    new Collider2D collider;
    public float myHealth;
    public GameObject canvas;
    private SummonTally tally;

    // Start is called before the first frame update
    void Start()
    {
        tally = canvas.GetComponentInChildren<SummonTally>();
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
        tally.AddTally("+ MANSLAUGHTER");
        Destroy(gameObject);
    }
    private void CheckDead()
    {
        if (myHealth <= 0)
        {
            tally.AddTally("+ NATURAL CAUSES");
            Destroy(gameObject);
        }
    }
}
