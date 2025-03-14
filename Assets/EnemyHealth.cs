using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    new Collider2D collider;
    bool DamageShouldBeTaken;
    public float myHealth;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            print("FUCK");
        }
        DamageShouldBeTaken = false;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void CheckTakeDamage()
    {
        if (DamageShouldBeTaken)
        {
            takeDamage();
        }
    }
    void takeDamage()
    {

    }
}
