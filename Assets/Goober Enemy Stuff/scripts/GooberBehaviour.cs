using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooberBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject Player;

    float playerXValue;
    float playerYValue;

    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float JumpHeight;
    [SerializeField] private float LungeRange;
    [SerializeField] private float LungeCooldownTime;

    private float timer;
    private bool canReachPlayer;
    private bool facingRight;
    private bool isGrounded;
    private bool isLunging;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        facingRight = true;
        isGrounded = false;
        isLunging = false;
        timer = LungeCooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInRange();
        if (LungeCooldown())
        {
            CheckLunge();
            RedefineStuff();
            ChasePlayer();
        }
        else if(!isLunging)
        {
            rb.velocity = new Vector2(0, 0);
        }
        FacePlayer();
        print($"icanreachyou is {canReachPlayer}, and isLunging is  {isLunging}");
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
        }
        else if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    bool LungeCooldown()
    {
        if(timer >= LungeCooldownTime)
        {
            return true;
        }
        else
        {
            timer += Time.deltaTime;
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
            else if (!facingRight)
            {
                LungeAtLeft();
            }
            timer = 0;
        }
    }
    void LungeAtRight()
    {
        rb.velocity += new Vector2(maxSpeed*2, 2);
        isLunging = true;
        if (isGrounded)
        {
            isGrounded = false;
        }
        print("grahh i jumped to the right");
    }
    void LungeAtLeft()
    {
        rb.velocity += new Vector2(-maxSpeed * 2, 2);
        isLunging = true;
        if (isGrounded)
        {
            isGrounded = false;
        }
        print("grahh i jumped to the left");
    }
    void RedefineStuff()
    {
        playerXValue = Player.transform.position.x;
        playerYValue = Player.transform.position.y;
    }
    void CheckInRange()
    {
        if (transform.position.x - playerXValue <= 3)
        {
            canReachPlayer = true;
        }
        else
        {
            canReachPlayer = false;
        }
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
        if (transform.position.x > playerXValue + LungeRange)
        {
            canReachPlayer = false;
            if(rb.velocity.x > -maxSpeed)
            {
                rb.velocity -= new Vector2(acceleration, 0);
            }
        }
        else if (transform.position.x < playerXValue - LungeRange)
        {
            canReachPlayer = false;
            if (rb.velocity.x < maxSpeed)
            {
                rb.velocity += new Vector2(acceleration, 0);
            }
        }
    }
}
