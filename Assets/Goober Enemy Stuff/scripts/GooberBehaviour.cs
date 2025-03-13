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
    [HideInInspector] public bool isLunging;
    private bool inChase;
    [HideInInspector] public bool isIdle;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        RedefineStuff();
        CheckInRange();
        if (LungeCooldown())
        {
            CheckLunge();
            ChasePlayer();
        }
        else if(!isLunging)
        {
            rb.velocity = new Vector2(0, 0);
            isIdle = true;
        }
        FacePlayer();
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
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
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
    bool LungeCooldown()
    {
        if(timer >= LungeCooldownTime)
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
    }
    void LungeAtLeft()
    {
        rb.velocity += new Vector2(-maxSpeed * 2, 2);
        isLunging = true;
        if (isGrounded)
        {
            isGrounded = false;
        }
    }
    void RedefineStuff()
    {
        playerXValue = Player.transform.position.x;
        playerYValue = Player.transform.position.y;
    }
    void CheckInRange()
    {
        if (Mathf.Abs(transform.position.x - playerXValue) <= LungeRange)
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
                inChase = true;
                rb.velocity -= new Vector2(acceleration, 0);
            }
        }
        else if (transform.position.x < playerXValue - LungeRange)
        {
            canReachPlayer = false;
            if (rb.velocity.x < maxSpeed)
            {
                inChase = true;
                rb.velocity += new Vector2(acceleration, 0);
            }
        }
    }
}
