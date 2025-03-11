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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        facingRight = true;
        isGrounded = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckLunge();
        RedefineStuff();
        CheckInRange();
        ChasePlayer();
        print(facingRight);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
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
            timer = 0;
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
        }
    }
    void LungeAtRight()
    {
        rb.velocity += new Vector2(maxSpeed*2, 2);
        if (isGrounded)
        {
            isGrounded = false;
        }
        print("grahh i jumped to the right");
    }
    void LungeAtLeft()
    {
        rb.velocity += new Vector2(-maxSpeed * 2, 2);
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
        if (transform.position.x <= playerXValue + LungeRange || transform.position.x >= playerXValue + LungeRange)
        {
            canReachPlayer = true;
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
    void ChasePlayer()
    {
        if (transform.position.x > playerXValue + LungeRange)
        {
            canReachPlayer = false;
            if(rb.velocity.x > -maxSpeed)
            {
                rb.velocity -= new Vector2(acceleration, 0);
                TurnToFace("Left");
            }
        }
        else if (transform.position.x < playerXValue - LungeRange)
        {
            canReachPlayer = false;
            if (rb.velocity.x < maxSpeed)
            {
                rb.velocity += new Vector2(acceleration, 0);
                TurnToFace("Right");
            }
        }
    }
}
