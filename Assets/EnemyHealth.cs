using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    new Collider2D collider;
    bool isDying;
    public float myHealth;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            print("FUCK");
        }
        isDying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDying)
        {
            print("ahh fuck lois im dying)");
        }
    }
    void Die()
    {
        isDying = true;
    }
}
