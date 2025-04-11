using UnityEngine;

public class AirbornePanLogic : MonoBehaviour
{
    fryingPanStuffV1 guy;
    [SerializeField] private LayerMask PlayerLayer;
    Rigidbody2D rb;
    public float PickupRange;
    [HideInInspector] public bool isLethal;
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
            guy.hasPan = true;
            Destroy(gameObject);
        }
        if (!ReturnedToRegularCollider && !isLethal)
        {
            SecondaryCollider.isTrigger = false;
        }
        CheckReturn();
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
    float timer = 0;
    [SerializeField] float timeToReturn;
    private bool IsWithinProximity(Vector2 positionA, Vector2 positionB, float threshold)
    {
        return Vector2.Distance(positionA, positionB) <= threshold;
    }
    void CheckReturn()
    {
        if (isLethal) return;
        if(Timer()) 
        {
            guy.hasPan = true;
            Destroy(gameObject); 
        }
    }
    bool Timer()
    {
        if(timer >= timeToReturn)
        {
            timer = 0;
            return true;
        }
        else
        {
            timer += Time.deltaTime;
        }
        return false;
    }
}
