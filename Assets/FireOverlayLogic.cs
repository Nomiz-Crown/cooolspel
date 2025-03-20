using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireOverlayLogic : MonoBehaviour
{
    private bool isAnimating;
    private Image img;
    private Animator animator;
    public GameObject temp;
    TempScoreLogic tempScoreLogic;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        animator = GetComponent<Animator>();
        tempScoreLogic = temp.GetComponent<TempScoreLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIsAnimating();
        Animate();
    }
    void Animate()
    {
        if (!isAnimating)
        {
            animator.SetTrigger("FireAnimation");
            isAnimating = true;
        }
    }
    void UpdateIsAnimating()
    {
        if(tempScoreLogic.rend.sprite != tempScoreLogic.overHeat)
        {
            isAnimating = false;
        }
    }
}
