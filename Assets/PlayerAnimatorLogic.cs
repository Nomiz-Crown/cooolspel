using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorLogic : MonoBehaviour
{
    MovementSystem Player;
    Animator anim;

    [SerializeField] private string nameOfIdleAnim;
                     private bool isAnimatingIdle = false;

    [SerializeField] private string nameOfWalkAnim;
                     private bool isAnimatingWalk = false;

    [SerializeField] private string nameOfSlideAnim;
                     private bool isAnimatingSlide = false;

    [SerializeField] private string nameOfFallAnim;
                     private bool isAnimatingFall = false;

    [SerializeField] private string nameOfJumpAnim;
                     private bool isAnimatingJump = false;

    [SerializeField] private string nameOfGrindAnim;
                     private bool isAnimatingGrind = false;

    private List<bool> isAnimating = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<MovementSystem>();
        anim = GetComponent<Animator>();
        isAnimating = new List<bool> { isAnimatingFall, isAnimatingGrind, isAnimatingIdle, isAnimatingJump, isAnimatingSlide, isAnimatingWalk };
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.isJumping)
        {
            isAnimatingJump = false;
        }

        if (IdleAnimationCanOccur())
        {
            LoopIdle();
        }
        else if (JumpAnimationCanOccur())
        {
            PlayJump();
        }
        else if (GrindAnimationCanOccur())
        {
            LoopGrind();
        }
        else if (FallAnimationCanOccur())
        {
            LoopFall();
        }
        else if (SlideAnimationCanOccur())
        {
            LoopSlide();
        }
        else if (WalkAnimationCanOccur())
        {
            LoopWalk();
        }
    }
    //idle animation stuff
    void DisableAllExcept(bool exception)
    {
        for (int i = 0; i < isAnimating.Count; i++)
        {
            if(isAnimating[i] != exception)
            {
                isAnimating[i] = false;
            }
        }
    }

    void LoopIdle()
    {
        if (!isAnimatingIdle)
        {
            anim.Play(nameOfIdleAnim);
            isAnimatingIdle = true;
            DisableAllExcept(isAnimatingIdle);
            print("playing idle animation");
        }
    }
    bool IdleAnimationCanOccur()
    {
        if (Player.isGrounded && !Player.isSliding && !Player.isWalking && !Player.inSlam && !Player.isJumping)
        {
            return true;
        }
        return false;
    }

    //walk animation stuff
    void LoopWalk()
    {
        if (!isAnimatingWalk)
        {
            anim.Play(nameOfWalkAnim);
            isAnimatingWalk = true;
            DisableAllExcept(isAnimatingWalk);
            print("playing Walk animation");
        }
    }
    bool WalkAnimationCanOccur()
    {
        if (Player.isGrounded && !Player.isSliding && Player.isWalking && !Player.inSlam)
        {
            return true;
        }
        return false;
    }
    //Slide animation stuff
    void LoopSlide()
    {
        if (!isAnimatingSlide)
        {
            anim.Play(nameOfSlideAnim);
            isAnimatingSlide = true;
            DisableAllExcept(isAnimatingSlide);
            print($"Playing slide anim");
        }
    }
    bool SlideAnimationCanOccur()
    {
        if (Player.isGrounded && !Player.isWalking && !Player.inSlam)
        {
            return true;
        }
        return false;
    }
    
    //fall animation stuff
    void LoopFall()
    {
        if (!isAnimatingFall)
        {
            anim.Play(nameOfFallAnim);
            isAnimatingFall = true;
            DisableAllExcept(isAnimatingFall);
            print($"playing slide animation");
        }
    }
    bool FallAnimationCanOccur()
    {
        if (!Player.isGrounded && !Player.isWalking && !Player.inSlam && !Player.isSliding && !Player.isJumping && Player.isFalling)
        {
            return true;
        }
        return false;
    }

    //jump animation stuff
    void PlayJump()
    {
        anim.Play(nameOfJumpAnim);
        isAnimatingJump = true;
        DisableAllExcept(isAnimatingJump);
    }
    bool JumpAnimationCanOccur()
    {
        if(!Player.isWalking && !Player.inSlam && !Player.isSliding && Player.isJumping && !Player.isFalling)
        {
            return true;
        }
        return false;
    }
    //Grind animation stuff
    void LoopGrind()
    {
        if (!isAnimatingGrind)
        {
            anim.Play(nameOfGrindAnim);
            isAnimatingGrind = true;
            DisableAllExcept(isAnimatingGrind);
            print($"playing grind anim");
        }
    }
    bool GrindAnimationCanOccur()
    {
        if (!Player.isGrounded && !Player.isWalking && !Player.inSlam && !Player.isSliding && !Player.isJumping && Player.isGrinding && !Player.isFalling)
        {
            return true;
        }
        return false;
    }

}
