using Unity.VisualScripting;
using UnityEngine;

public class MCMovementv2 : MonoBehaviour
{
    //speed definers
    [SerializeField] private float acceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float JumpHeight;

    //endless tags
    [HideInInspector] public bool isWalking;
    [HideInInspector] public bool isIdle;
    [HideInInspector] public bool isSliding;
    [HideInInspector] public bool isGrounded;
    [HideInInspector] public bool isFalling;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isSlamming;
    [HideInInspector] public bool isGrinding;
    [HideInInspector] public bool FacingRight;

    [HideInInspector] private bool canSlamJump;
    [HideInInspector] protected bool checkSlamJumpInput = false;

    //misc values that are neccesary for some reason
                     private int wallJumpCounter;
    [SerializeField] private int maxWallJumps;
                     private float timer = 0;
                     private float otherTimer = 0;
    [SerializeField] private float timerForJumpDuration;
    [SerializeField] private float slideMult;
    [HideInInspector] private float slamJumpTimerMult = 0;
    [SerializeField] private float slamJumpWindow;

    //unneccesary values for jag ar dalig pa programmering
    private Vector2 slidingColliderSize;
    private Vector2 slidingColliderOffset;

    private Vector2 defaultColliderSize;
    private Vector2 defaultColliderOffset;

    //components
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        isWalking = false;
        isIdle = false;
        isSliding = false;
        isGrounded = false;
        isFalling = true;
        isGrinding = false;
        FacingRight = true;
        isSlamming = false;
        canSlamJump = false;


        defaultColliderOffset = coll.offset;
        defaultColliderSize = coll.size;

        slidingColliderOffset = new Vector2(coll.offset.x, 0.18f);
        slidingColliderSize = new Vector2(coll.size.y, coll.size.x);


        //To do list;
        //1. slam
        //2-10. goon
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove();

        CheckJump();
        ResetJumpToFallAfterDelay();

        CheckSlam();
        SlamJump();

        CheckSlide();
        ChangeCollider();

        UpdateFacingDirection();

        UpdateIsIdle();

        DeaccelIfIdle();
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
            if (isJumping)
            {
                isJumping = false;
            }
            if (isSlamming)
            {
                isSlamming = false;
                canSlamJump = true;
            }
            if (isWalking)
            {
                isWalking = false;
            }
            if (isGrinding)
            {
                isGrinding = false;
            }
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            if (!isGrounded)
            {
                isGrinding = true;
            }
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
            if (isWalking)
            {
                isWalking = false;
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isGrinding = false;
        }
    }
    private void CheckSlam()
    {
        if (!isGrounded && !isSlamming)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                ExecuteSlam();
            }
        }
        else if(!isGrounded && isSlamming)
        {
            slamJumpTimerMult += Time.deltaTime;
        }
    }
    private void ExecuteSlam()
    {
        rb.velocity = new Vector2(rb.velocity.x / 4, -20);
        isSlamming = true;
        if (isJumping)
        {
            isJumping = false;
        }
        if (isFalling)
        {
            isFalling = false;
        }
    }
    private void ChangeCollider()
    {
        if (isSliding)
        {
            coll.size = slidingColliderSize;
            coll.offset = slidingColliderOffset;
        }
        else
        {
            coll.size = defaultColliderSize;
            coll.offset = defaultColliderOffset;
        }
    }
    void DeaccelIfIdle()
    {
        if(isIdle)
        {
            if (rb.velocity.x != 0)
            {
                if (rb.velocity.x > 0)
                {
                    rb.velocity -= new Vector2(rb.velocity.x/10, 0);
                }
                else if (rb.velocity.x < 0)
                {
                    rb.velocity += new Vector2(rb.velocity.x/-10, 0);
                }
            }
        }
    }
    void ResetJumpToFallAfterDelay()
    {
        if (isJumping)
        {
            if (timer >= timerForJumpDuration)
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
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (isGrounded)
            {
                ExecuteSlide();
            }
            else
            {
                isSliding = false;
                if (!isJumping)
                {
                    isFalling = true;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isSliding = false;
        }
    }
    void ExecuteSlide()
    {
        
        if (FacingRight)
        {
            rb.velocity = new Vector2(maxSpeed * slideMult, rb.velocity.y);
        }
        else if (!FacingRight)
        {
            rb.velocity = new Vector2(-maxSpeed * slideMult, rb.velocity.y);
        }
        isSliding = true;
        isWalking = false;
    }
    void CheckJump()
    {
        if (isGrounded || isGrinding)
        {
            if (isGrounded && canSlamJump)
            {
                if (checkSlamJumpInput)
                {
                    ExecuteSlamJump();
                }
            }
            else if(isGrounded && !isGrinding)
            {
                ExecuteGroundJump();
            }
            else if (!isGrounded && isGrinding)
            {
                ExecuteWallJump();
            }
        }
    }
    void ExecuteSlamJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rb.velocity += new Vector2(0, JumpHeight * (1 + slamJumpTimerMult));
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
                    rb.velocity = new Vector2(-maxSpeed / 2, JumpHeight);
                }
                else if (!FacingRight)
                {
                    rb.velocity = new Vector2(maxSpeed / 2, JumpHeight);
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
        if (rb.velocity.x < maxSpeed && isGrounded)
        {
            rb.velocity += new Vector2(acceleration, 0f);
        }
        else if (rb.velocity.x < maxSpeed && !isGrounded)
        {
            rb.velocity += new Vector2(acceleration / 2, 0f);
        }

        if (isGrounded)
        {
            isWalking = true;
        }
        else if (!isGrounded)
        {
            if (!isJumping)
            {
                isFalling = true;
            }
            isWalking = false;
        }
        FacingRight = true;
    }
    void AccelerateLeft()
    {
        if (rb.velocity.x > -maxSpeed && isGrounded)
        {
            rb.velocity -= new Vector2(acceleration, 0f);
        }
        else if (rb.velocity.x > -maxSpeed && !isGrounded)
        {
            rb.velocity -= new Vector2(acceleration / 2, 0f);
        }

        if (isGrounded)
        {
            isWalking = true;
        }
        else if (!isGrounded)
        {
            if (!isJumping)
            {
                isFalling = true;
            }
            isWalking = false;
        }
        FacingRight = false;
    }
    void SlamJump()
    {
        if (canSlamJump)
        {
            if(otherTimer >= slamJumpWindow)
            {
                canSlamJump = false;
                checkSlamJumpInput = false;
                otherTimer = 0;
                slamJumpTimerMult = 0;
            }
            else
            {
                otherTimer += Time.deltaTime;
                checkSlamJumpInput = true;
            }
        }
    }
}
