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

    //flags
    private bool isGrounded = false;
    private bool inSlam = false;
    
    // define comp
    private Rigidbody2D rb;
    void Start()
    {
        //comps
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGrounded)
        {
            inSlam = false;
            checkAndExecuteJump();
        }
        else if (isGrounded == false)
        {
            checkAndExecuteSlam();
        }
        checkAndMoveRight();
        checkAndMoveLeft();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void checkAndExecuteSlam()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            inSlam = true;
            rb.velocity = new Vector2(rb.velocity.x, -20);
        }
    }
    private void checkAndExecuteJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity += new Vector2(0, jumpHeight);
        }
    }
    private void checkAndMoveRight()
    {
        if (inSlam) //right movement strength if mid-slam
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (rb.velocity.x < maxSpeed / 3)
                {
                    rb.velocity += new Vector2(Acceleration, 0) / 3;
                }
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                if (rb.velocity.x > 0)
                {
                    rb.velocity -= new Vector2(Deacceleration, 0);
                }
            }
        }
        else if (inSlam == false) //grounded and airborne right movement strength
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (rb.velocity.x < maxSpeed)
                {
                    rb.velocity += new Vector2(Acceleration, 0);
                }
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                if (rb.velocity.x > 0)
                {
                    rb.velocity -= new Vector2(Deacceleration, 0);
                }
            }
        }
    }
    private void checkAndMoveLeft()
    { 
        if (inSlam)
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (rb.velocity.x > -maxSpeed / 3 )
                {
                    rb.velocity -= new Vector2(Acceleration, 0) / 3;
                }
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                if (rb.velocity.x < 0)
                {
                    rb.velocity += new Vector2(Deacceleration, 0);
                }
            }
        }
        else if (inSlam == false) //grounded and airborne left movement strength
        {
            if (Input.GetKey(KeyCode.A))
            {
                if (rb.velocity.x > -maxSpeed)
                {
                    rb.velocity -= new Vector2(Acceleration, 0);
                }
            }
            else if (Input.GetKeyUp(KeyCode.A))
            {
                if (rb.velocity.x < 0)
                {
                    rb.velocity += new Vector2(Deacceleration, 0);
                }
            }
        }
    }
}
