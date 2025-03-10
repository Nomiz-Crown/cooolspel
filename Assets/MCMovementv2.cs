using UnityEngine;

public class MCMovementv2 : MonoBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;

    [HideInInspector] public bool isWalking;
    [HideInInspector] public bool isIdle;
    [HideInInspector] public bool isSliding;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isFalling;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isSlaming;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isWalking = false;
        isIdle = false;
        isSliding = false;
        isGrounded = false;
        isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        print(isWalking);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if (isFalling)
            {
                isFalling = false;
            }
            if(isJumping)
            {
                isJumping = false;
            }
            if (isSlaming)
            {
                isSlaming = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    void CheckMove()
    {
        if (!isSliding)
        {
            if (Input.GetKey(KeyCode.D))
            {
                AccelerateRight();
            }
            else if (Input.GetKey(KeyCode.A))
            {
                AccelerateLeft();
            }
            else
            {
                isWalking = false;
            }
        }
    }
    void AccelerateRight()
    {
        if (rb.velocity.x < maxSpeed)
        {
            rb.velocity += new Vector2(acceleration, 0f);
        }
    }
    void AccelerateLeft()
    {
        if (rb.velocity.x > -maxSpeed)
        {
            rb.velocity -= new Vector2(acceleration, 0f);
        }
        isWalking = true;
    }
}
