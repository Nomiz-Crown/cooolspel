using UnityEngine;

public class AirbornePanLogic : MonoBehaviour
{
    fryingPanStuffV1 guy;
    [SerializeField] private LayerMask PlayerLayer;
    Rigidbody2D rb;
    public float PickupRange;
    [HideInInspector] public bool isLethal;
    public float rotationSpeed;
    CapsuleCollider2D SecondaryCollider;
    bool ReturnedToRegularCollider;
    // Start is called before the first frame update
    void Start()
    {
        isLethal = true;
        guy = GameObject.FindGameObjectWithTag("Player").GetComponent<fryingPanStuffV1>();
        rb = GetComponent<Rigidbody2D>();
        SecondaryCollider = GetComponent<CapsuleCollider2D>();
        ReturnedToRegularCollider = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWithinProximity(transform.position, guy.transform.position, PickupRange) && !isLethal)
        {
            print("player is close enough");
            guy.hasPan = true;
            Destroy(gameObject);
        }
        if (!ReturnedToRegularCollider && !isLethal)
        {
            SecondaryCollider.isTrigger = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //do cool stuff here
        }
        else
        {
            isLethal = false;
        }
        rb.gravityScale = 1;

    }
    private bool IsWithinProximity(Vector2 positionA, Vector2 positionB, float threshold)
    {
        return Vector2.Distance(positionA, positionB) <= threshold;
    }
}
