using System.Collections.Generic;
using UnityEngine;

public class MovementSystem : MonoBehaviour
{
    // Movement parameters
    public float maxSpeed = 8f;
    public float acceleration = 0.7f;
    public float deacceleration = 2f;
    public float jumpHeight = 8f;

    // Jumping parameters
    public int maxWallJumpCount = 2;
    private int wallJumpsUsed = 0;

    // State flags
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool isWalking = false;
    [HideInInspector] public bool inSlam = false;
    [HideInInspector] public bool isSliding = false;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool isFalling = false;
    [HideInInspector] public bool isGrinding = false;
    [HideInInspector] public bool isIdle = true;

    private bool facingRight = true;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateStateFlags();
        HandleMovement();
        HandleJumping();
        HandleSliding();
        HandleSlam();
        TurnToLook();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            ResetJump();
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isGrinding = true;
            isFalling = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            isSliding = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isGrinding = false;
        }
    }

    private void UpdateStateFlags()
    {
        isIdle = !(isGrounded || inSlam || isSliding || isJumping || isFalling || isGrinding);
    }

    private void HandleMovement()
    {
        if (isGrounded)
        {
            CheckMove();
        }
        else if (isGrinding && wallJumpsUsed < maxWallJumpCount)
        {
            WallJump();
        }
    }

    private void CheckMove()
    {
        if (Input.GetKey(KeyCode.D)) Move(Vector2.right);
        if (Input.GetKey(KeyCode.A)) Move(Vector2.left);
    }

    private void Move(Vector2 direction)
    {
        facingRight = direction == Vector2.right;
        float targetSpeed = direction.x * maxSpeed;

        if (Mathf.Abs(rb.velocity.x) < maxSpeed)
        {
            rb.velocity += new Vector2(direction.x * acceleration, 0);
        }
        isWalking = true;
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                ExecuteJump();
            }
            else if (isGrinding && wallJumpsUsed < maxWallJumpCount)
            {
                WallJump();
            }
        }
    }

    private void ExecuteJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        isJumping = true;
        isSliding = false;
    }

    private void HandleSliding()
    {
        if (isGrounded && Input.GetKey(KeyCode.LeftControl))
        {
            ExecuteSlide();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isSliding = false;
        }
    }

    private void ExecuteSlide()
    {
        isSliding = true;
        rb.velocity = new Vector2(facingRight ? maxSpeed * 2f : -maxSpeed * 2f, rb.velocity.y);
    }

    private void HandleSlam()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isGrounded)
        {
            ExecuteSlam();
        }
    }

    private void ExecuteSlam()
    {
        isFalling = true;
        rb.velocity = new Vector2(rb.velocity.x, -20);
    }

    private void WallJump()
    {
        wallJumpsUsed++;
        rb.velocity = new Vector2(facingRight ? -maxSpeed * 2 : maxSpeed * 2, jumpHeight);
    }

    private void TurnToLook()
    {
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void ResetJump()
    {
        wallJumpsUsed = 0;
        isGrounded = true;
    }
}
