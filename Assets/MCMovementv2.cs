using Unity.VisualScripting;
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
    [HideInInspector] public bool isGrinding;
    [HideInInspector] public bool FacingRight;

    private int wallJumpCounter;
    [SerializeField] private float JumpHeight;
    [SerializeField] private int maxWallJumps;
    private float timer = 0;
    [SerializeField] private float timerDuration;

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
        isGrinding = false;
        FacingRight = true;
        //lägg till deaccel when grounded, slamming
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();
        CheckJump();
        ResetJumpToFallAfterDelay();
        CheckSlide();
        UpdateFacingDirection();
        UpdateIsIdle();
        print($"isSliding is {isSliding} and isGrounded is {isGrounded}");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            wallJumpCounter = 0;
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
            if (isWalking)
            {
                isWalking = false;
            }
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isGrinding = true;
            if (isJumping)
            {
                isJumping = false;
            }
            if (isFalling)
            {
                isFalling = false;
            }
            if (isSliding)
            {
                isSliding = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isGrinding = false;
            if (!isJumping)
            {
                isFalling = true;
            }
        }
    }
    void ResetJumpToFallAfterDelay()
    {
        if (isJumping)
        {
            if (timer >= timerDuration)
            {
                isJumping = false;
                isFalling = true;
                timer = 0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
    void UpdateIsIdle()
    {
        if(isGrounded && !isFalling && !isGrinding && !isJumping && !isSliding && !isWalking)
        {
            isIdle = true;
        }
        else { isIdle = false; }
    }
    void CheckSlide()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                ExecuteSlide();
            }
        }
    }
    void ExecuteSlide()
    {
        if (FacingRight)
        {
            rb.velocity = new Vector2(maxSpeed * 2, rb.velocity.y);
        }
        else if (!FacingRight)
        {
            rb.velocity = new Vector2(-maxSpeed * 2, rb.velocity.y);

        }
        isSliding = true;
        isWalking = false;
    }
    void CheckJump()
    {
        if (isGrounded || isGrinding)
        {
            if(isGrounded && !isGrinding)
            {
                ExecuteGroundJump();
            }
            else if (!isGrounded && isGrinding)
            {
                ExecuteWallJump();
            }
        }
    }
    void ExecuteGroundJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rb.velocity += new Vector2(0, JumpHeight);
        }
    }
    void ExecuteWallJump()
    {
        if(maxWallJumps > wallJumpCounter)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (FacingRight)
                {
                    rb.velocity = new Vector2(-maxSpeed, JumpHeight);
                }
                else if (!FacingRight)
                {
                    rb.velocity = new Vector2(maxSpeed, JumpHeight);
                }
                isJumping = true;
                wallJumpCounter++;
            }
        }
    }
    void UpdateFacingDirection()
    {
        if (FacingRight)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (!FacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
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
        if (isGrounded)
        {
            isWalking = true;
        }
        FacingRight = true;
    }
    void AccelerateLeft()
    {
        if (rb.velocity.x > -maxSpeed)
        {
            rb.velocity -= new Vector2(acceleration, 0f);
        }
        if (isGrounded)
        {
            isWalking = true;
        }
        FacingRight = false;
    }
}
