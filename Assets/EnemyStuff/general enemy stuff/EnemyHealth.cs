using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    new Collider2D collider;
    bool isDying = false;
    public float myHealth;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            print("uuooohhhhhhhhhhhhh");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (myHealth > 0 || isDying == false) return;

        if (myHealth <= 0)
        {
            //natural causes!
        }
        else if (isDying)
        {
            //kill!
        }
        Destroy(gameObject);
    }
    void Kys()
    {
        isDying = true;
    }
}
