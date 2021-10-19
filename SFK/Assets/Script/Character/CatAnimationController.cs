using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool doJump;
    public bool doSound;
    public bool doEat;
    public bool startWalk;
    public bool stopWalk;

    public Animator animator;

    void Update()
    {
        if (doJump)
        {
            doJump = false;
            //play jump animation
            animator.SetTrigger("Jump");
        }
        if (doSound)
        {
            doSound = false;
            animator.SetTrigger("Sound");
        }
        if (doEat)
        {
            doEat = false;
            animator.SetTrigger("Eat");
        }
        if (startWalk)
        {
            startWalk = false;
            animator.SetBool("Walk", true);
        }
        if (stopWalk)
        {
            stopWalk = false;
            animator.SetBool("Walk", false);
        }
    }
}
