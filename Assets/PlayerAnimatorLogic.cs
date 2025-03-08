using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorLogic : MonoBehaviour
{
    MovementSystem Player;
    Animator anim;

    [SerializeField] private string nameOfIdleAnim;
    [SerializeField] private float lengthOfIdleAnim;
                     private bool isAnimatingIdle = false;

    [SerializeField] private string nameOfWalkAnim;
    [SerializeField] private float lengthOfWalkAnim;
                     private bool isAnimatingWalk = false;

    [SerializeField] private string nameOfSlideAnim;
    [SerializeField] private float lengthOfSlideAnim;
                     private bool isAnimatingSlide = false;

    [SerializeField] private string nameOfFallAnim;
    [SerializeField] private float lengthOfFallAnim;
                     private bool isAnimatingFall = false;

    [SerializeField] private string nameOfJumpAnim;
    [SerializeField] private float lengthOfJumpAnim;
                     private bool isAnimatingJump = false;

    [SerializeField] private string nameOfGrindAnim;
    [SerializeField] private float lengthOfGrindAnim;
                     private bool isAnimatingGrind = false;

    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<MovementSystem>();
        Player.durationOfJump = lengthOfJumpAnim;
        anim = GetComponent<Animator>();
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
    void UpdateTimer()
    {
        timer += Time.deltaTime;
    }

    //idle animation stuff
    void LoopIdle()
    {
        if (!isAnimatingIdle)
        {
            anim.Play(nameOfIdleAnim);
            isAnimatingIdle = true;
            timer = 0;
            print($"played idle animation, {isAnimatingIdle} should be true, ({timer}) should be 0");
        }
        else
        {
            UpdateTimer();
            UpdateIsAnimatingIdle();
        }
    }
    void UpdateIsAnimatingIdle()
    {
        if (timer >= lengthOfIdleAnim)
        {
            print("enough time has passed, isAnimatingIdle is now " + isAnimatingIdle);
            isAnimatingIdle = false;
        }
        else
        {
            print("time has not passed, isAnimatingIdle remains " + isAnimatingIdle);
        }
    }
    bool IdleAnimationCanOccur()
    {
        if (Player.isGrounded && !Player.isSliding && !Player.isWalking && !Player.inSlam)
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
            timer = 0;
            print($"played Walk animation, {isAnimatingWalk} should be true, ({timer}) should be 0");
        }
        else
        {
            UpdateTimer();
            UpdateIsAnimatingWalk();
        }
    }
    void UpdateIsAnimatingWalk()
    {
        if (timer >= lengthOfWalkAnim)
        {
            print("enough time has passed, isAnimatingWalk is now " + isAnimatingWalk);
            isAnimatingWalk = false;
        }
        else
        {
            print("time has not passed, isAnimatingWalk remains " + isAnimatingWalk);
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
            timer = 0;
            print($"played Slide animation, {isAnimatingSlide} should be true, ({timer}) should be 0");
        }
        else
        {
            UpdateTimer();
            UpdateIsAnimatingSlide();
        }
    }
    void UpdateIsAnimatingSlide()
    {
        if (timer >= lengthOfSlideAnim)
        {
            print("enough time has passed, isAnimatingSlide is now " + isAnimatingSlide);
            isAnimatingSlide = false;
        }
        else
        {
            print("time has not passed, isAnimatingSlide remains " + isAnimatingSlide);
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
            timer = 0;
            print($"played Fall animation, {isAnimatingFall} should be true, ({timer}) should be 0");
        }
        else
        {
            UpdateTimer();
            UpdateIsAnimatingFall();
        }
    }
    void UpdateIsAnimatingFall()
    {
        if (timer >= lengthOfFallAnim)
        {
            print("enough time has passed, isAnimatingFall is now " + isAnimatingFall);
            isAnimatingFall = false;
        }
        else
        {
            print("time has not passed, isAnimatingFall remains " + isAnimatingFall);
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
    }
    bool JumpAnimationCanOccur()
    {
        if(!Player.isGrounded && !Player.isWalking && !Player.inSlam && !Player.isSliding && Player.isJumping && !Player.isFalling)
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
            timer = 0;
            print($"played grind animation, {isAnimatingGrind} should be true, ({timer}) should be 0");
        }
        else
        {
            UpdateTimer();
            UpdateIsAnimatingGrind();
        }
    }
    void UpdateIsAnimatingGrind()
    {
        if (timer >= lengthOfGrindAnim)
        {
            print("enough time has passed, isAnimatingGrind is now " + isAnimatingGrind);
            isAnimatingGrind = false;
        }
        else
        {
            print("time has not passed, isAnimatingGrind remains " + isAnimatingGrind);
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
