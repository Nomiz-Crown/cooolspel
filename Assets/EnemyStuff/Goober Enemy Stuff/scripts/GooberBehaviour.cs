using UnityEngine;

public class GooberBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject Player;

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float LungeRange;
    [SerializeField] private float LungeCooldownTime;

    private float timer;
    private bool canReachPlayer;
    private bool facingRight;
    private bool isGrounded;
    [HideInInspector] public bool isLunging;
    private bool inChase;
    [HideInInspector] public bool isIdle;
    [HideInInspector] public bool canParry = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        facingRight = true;
        isGrounded = false;
        isLunging = false;
        timer = LungeCooldownTime;
        inChase = false;
        isIdle = false;
    }

    void Update()
    {
        CheckInRange();
        if (LungeCooldown())
        {
            CheckLunge();
            ChasePlayer();
            FacePlayer();
        }
        else if (!isLunging)
        {
            rb.velocity = new Vector2(0, 0);
            isIdle = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if (isLunging)
            {
                isLunging = false;
            }
            MakeCanParry(false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            MakeCanParry(true);
        }
    }

    void CheckIdle()
    {
        if (!isLunging && !inChase && isGrounded)
        {
            isIdle = true;
        }
        else
        {
            isIdle = false;
        }
    }

    [HideInInspector]
    public bool LungeCooldown()
    {
        if (timer >= LungeCooldownTime)
        {
            return true;
        }
        else
        {
            timer += Time.deltaTime;
            inChase = false;
            return false;
        }
    }

    void CheckLunge()
    {
        if (canReachPlayer && isGrounded)
        {
            if (facingRight)
            {
                LungeAtRight();
            }
            else
            {
                LungeAtLeft();
            }
            timer = 0; // Reset timer after lunging
        }
    }
    bool MakeCanParry(bool smeg)
    {
        canParry = smeg;
        return canParry;
    }

    void LungeAtRight()
    {
        rb.velocity = new Vector2(maxSpeed * 2, JumpHeight);
        isLunging = true;
    }

    void LungeAtLeft()
    {
        rb.velocity = new Vector2(-maxSpeed * 2, JumpHeight);
        isLunging = true;
    }

    void CheckInRange()
    {
        canReachPlayer = Mathf.Abs(transform.position.x - Player.transform.position.x) <= LungeRange;
    }

    void TurnToFace(string direction)
    {
        if (direction == "Right")
        {
            transform.rotation = Quaternion.identity;
            facingRight = true;
        }
        else if (direction == "Left")
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingRight = false;
        }
    }

    void FacePlayer()
    {
        if (transform.position.x > Player.transform.position.x)
        {
            TurnToFace("Left");
        }
        else if (transform.position.x < Player.transform.position.x)
        {
            TurnToFace("Right");
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x > Player.transform.position.x + LungeRange)
        {
            if (rb.velocity.x > -maxSpeed)
            {
                inChase = true;
                rb.velocity -= new Vector2(acceleration, 0);
            }
        }
        else if (transform.position.x < Player.transform.position.x - LungeRange)
        {
            if (rb.velocity.x < maxSpeed)
            {
                inChase = true;
                rb.velocity += new Vector2(acceleration, 0);
            }
        }
    }
}
