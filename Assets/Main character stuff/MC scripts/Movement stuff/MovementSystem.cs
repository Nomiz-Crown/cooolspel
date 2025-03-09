using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementSystem : MonoBehaviour
{
    // movement stuff
    public float maxSpeed; //5-8 funkar bra
    public float Acceleration; // 0.7f
    public float Deacceleration; // 2
    public float jumpHeight; // 8??

    //jumpcount
    public int maxWallJumpCount;
    private int wallJumpsUsed = 0;

    //flags
    [HideInInspector] public bool isGrounded = false;
    [HideInInspector] public bool isWalking = false;
    [HideInInspector] public bool inSlam = false;
    [HideInInspector] public bool isSliding = false;
    [HideInInspector] public bool isJumping = false;
    [HideInInspector] public bool isFalling = false;
    [HideInInspector] public bool isGrinding = false;
    [HideInInspector] public bool isIdle = false;


    private bool facingRight = true;
    //listy list
    private List<bool> isBlanking = new List<bool>();
    //misc jump logic
    [HideInInspector] public float durationOfJump;
    [HideInInspector] public float jumpTimer;

    // define comp
    private Rigidbody2D rb;

    // i guess i need this? 
    private GameObject objectLastTouched;
    void Start()
    {
        isBlanking = new List<bool> { isGrounded, inSlam, isSliding, isJumping, isFalling, isGrounded, isIdle };
        //comps
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckIdle();
        if (isJumping)
        {
            JumpTimerLogic();
        }
        // Check if grounded before executing slide
        if (isGrounded)
        {
            CheckSlide();
            CheckJump();
        }
        else if (isGrinding)
        {
            if (!isGrounded && maxWallJumpCount - wallJumpsUsed > 0)
            {
                WallJump();
            }
        }
        else
        {
            CheckSlam();
        }

        TurnToLook();
        CheckMoveRight();
        CheckMoveLeft();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        objectLastTouched = collision.gameObject;
        if (objectLastTouched.CompareTag("Ground"))
        {
            wallJumpsUsed = 0;
            ResetSlideToIdle();
            DisableAllExceptAndTurnOn(isGrounded);
        }
        else if (objectLastTouched.CompareTag("Wall"))
        {
            DisableAllExceptAndTurnOn(isGrinding);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        objectLastTouched = null;
        if (collision.gameObject.CompareTag("Ground") && isSliding == true)
        {
            ResetSlideToIdle();
            isGrounded = false;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        else if (collision.gameObject.CompareTag("Wall"))
        {
            isGrinding = false;
        }
    }
    void CheckIdle()
    {
        int total = 0;
        foreach (bool smeg in isBlanking)
        {
            if (smeg == true && smeg != isIdle)
            {
                total++;
            }
        }
        if (total == 0)
        {
            isIdle = true;
        }
    }
    void DisableAllExceptAndTurnOn(bool exception)
    {
        for (int i = 0; i < isBlanking.Count; i++)
        {
            if (isBlanking[i] != exception)
            {
                isBlanking[i] = false;
            }
            else
            {
                isBlanking[i] = true;
            }
        }
    }
    private void WallJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (facingRight)
            {
                wallJumpsUsed++;
                rb.velocity = new Vector2(-maxSpeed*2, jumpHeight * 1);
            }
            else if (!facingRight)
            {
                wallJumpsUsed++;
                rb.velocity = new Vector2(maxSpeed*2, jumpHeight * 1);
            }
            DisableAllExceptAndTurnOn(isJumping);
        }
    }
    private void TurnToLook()
    {
        if (facingRight)
        {
            transform.rotation = Quaternion.identity;
        }
        else if (!facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void CheckSlam()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded == false)
        {
            ExecuteSlam();
        }
    }
    private void ExecuteSlam()
    {
        DisableAllExceptAndTurnOn(inSlam);
        isFalling = true;
        rb.velocity = new Vector2(rb.velocity.x, -20);
    }
    private void CheckSlide()
    {
        if (isGrounded && Input.GetKey(KeyCode.LeftControl)) // Ensure sliding only when grounded
        {
            ExecuteSlide();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            ResetSlideToIdle();
        }
    }
    void ExecuteSlide()
    {
        if (facingRight)
        {
            rb.velocity = new Vector2(maxSpeed * 2f, rb.velocity.y);
        }
        else if (!facingRight)
        {
            rb.velocity = new Vector2(maxSpeed * -2f, rb.velocity.y);
        }
        DisableAllExceptAndTurnOn(isSliding);
        isGrounded = true;
    }
    private void ResetSlideToIdle()
    {
        // Reset posture only if not sliding
        if (!isSliding)
        {
            transform.rotation = Quaternion.identity; // Reset to default rotation
        }
        isSliding = false; // Reset sliding state
    }
    private void CheckJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isSliding)
            {
                ResetSlideToIdle(); // Transition to idle state before jumping
            }
            if (isGrounded) // Ensure the character can only jump when grounded
            {
                ExecuteJump(); // Allow jumping
            }
        }
    }
    void ExecuteJump()
    {
        rb.velocity += new Vector2(0, jumpHeight);
        DisableAllExceptAndTurnOn(isJumping);
    }
    private void CheckMoveRight()
    {
        if (inSlam) //right movement strength if mid-slam
        {
            MoveRightWithMultiplier(1 / 3);
        }
        else if (!inSlam && isSliding) //grounded and airborne right movement strength
        {

        }
        else if (!inSlam && !isSliding)
        {
            MoveRightWithMultiplier(1);
        }
    }
    void MoveRightWithMultiplier(float mult)
    {
        if (Input.GetKey(KeyCode.D))
        {
            facingRight = true;
            if (rb.velocity.x < maxSpeed * mult)
            {
                rb.velocity += new Vector2(Acceleration, 0) * mult;
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            facingRight = true;
            rb.velocity = new Vector2(Mathf.Max(rb.velocity.x - Deacceleration * Time.deltaTime, 0), rb.velocity.y);
        }
        DisableAllExceptAndTurnOn(isWalking);
        isGrounded = true;
    }
    private void CheckMoveLeft()
    { 
        if (inSlam)
        {
            MoveLeftWithMultiplier(1 / 3);
        }
        else if (!inSlam && isSliding) //grounded and airborne right movement strength
        {

        }
        else if (!inSlam && !isSliding)
        {
            MoveLeftWithMultiplier(1);
        }
    }
    void MoveLeftWithMultiplier(float mult)
    {
        if (Input.GetKey(KeyCode.A))
        {
            facingRight = false;
            if (rb.velocity.x > -maxSpeed * mult)
            {
                rb.velocity -= new Vector2(Acceleration, 0) * mult;
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            facingRight = false;
            rb.velocity = new Vector2(Mathf.Min(rb.velocity.x + Deacceleration * Time.deltaTime, 0), rb.velocity.y);
        }
        DisableAllExceptAndTurnOn(isWalking);
        isGrounded = true;
    }
    void JumpTimerLogic()
    {
        print("hi");
        if(durationOfJump <= jumpTimer)
        {
            print($"{durationOfJump} <= {jumpTimer}");
            isJumping = false;
            isFalling = true;
            jumpTimer = 0;
        }
        else
        {
            print($"{durationOfJump} > {jumpTimer}");
            jumpTimer += Time.deltaTime;
        }
    }
}
