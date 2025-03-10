using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCAnimationV2 : MonoBehaviour
{
    Animator animator;
    MCMovementv2 Player;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Player = GetComponent<MCMovementv2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.isWalking && Player.isGrounded)
        {
            animator.Play("RunningAnimation");
        }
        else if (Player.isSliding && Player.isGrounded)
        {
            animator.Play("SlideAnimation");
        }
        else if (Player.isJumping && !Player.isGrounded)
        {
            animator.Play("JumpingAnimation");
        }
        else if (Player.isFalling && !Player.isGrounded)
        {
            animator.Play("FallAnimation");
        }
        else if (Player.isGrinding && !Player.isGrounded)
        {
            animator.Play("GrindAnimation");
        }
        else if (Player.isIdle && Player.isGrounded)
        {
            animator.Play("IdleAnimation");
        }
    }
}
