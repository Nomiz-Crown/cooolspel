using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimationV2 : MonoBehaviour
{
    Animator animator;
    MCMovementv2 Player;
    [HideInInspector] public bool isPunch;
    // Start is called before the first frame update
    void Start()
    {
        isPunch = false;
        animator = GetComponent<Animator>();
        Player = GetComponent<MCMovementv2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPunch)
        {
            animator.enabled = false;
            animator.enabled = true;
            animator.Play("PunchAnimation");
            PunchTimer();
            return;
        }
        if (Player.isWalking && Player.isGrounded)
        {
            animator.Play("RunningAnimationV2");
        }
        else if (Player.isSliding && Player.isGrounded)
        {
            animator.Play("SlideAnimationV2");
        }
        else if (Player.isJumping && !Player.isGrounded)
        {
            animator.Play("JumpingAnimationV2");
        }
        else if (!Player.isGrounded && Player.isSlamming)
        {
            animator.Play("SlammingAnimation");
        }
        else if (Player.isGrinding && !Player.isGrounded)
        {
            animator.Play("GrindAnimationV2");
        }
        else if (Player.isFalling && !Player.isGrounded)
        {
            animator.Play("FallingAnimationV2");
        }
        else if (Player.isIdle && Player.isGrounded)
        {
            animator.Play("BetterIdle");
        }
    }
    float timer = 0;
        public float punchanimtime;
    void PunchTimer()
    { 
        if(timer >= punchanimtime)
        {
            isPunch = false;
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
