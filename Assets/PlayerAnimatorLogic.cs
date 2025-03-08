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

    float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<MovementSystem>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IdleAnimationCanOccur())
        {
            LoopIdle();
        }
        else if (WalkAnimationCanOccur())
        {
            LoopWalk();
        }
    }
    void UpdateTimer()
    {
        timer += Time.deltaTime;
        print("updated timer, it is now " + timer);
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
        if (timer >= 0.333)
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
        if (timer >= 0.333)
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
}
