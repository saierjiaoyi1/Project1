using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HumanAnimationController : MonoBehaviour
{
    public bool doPick;
    public bool doCatch;
    public bool doStandup;

    public bool startWalk;
    public bool stopWalk;
    public bool startAcc;
    public bool stopAcc;

    public Animator animator;

    void Update()
    {
        if (doPick)
        {
            doPick = false;
            animator.SetTrigger("Pick");
        }
        if (doCatch)
        {
            doCatch = false;
            animator.SetTrigger("Catch");
        }
        if (doStandup)
        {
            doStandup = false;
            animator.SetTrigger("Standup");
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
        if (startAcc)
        {
            startAcc = false;
            animator.SetBool("Acc", true);
        }
        if (stopAcc)
        {
            stopAcc = false;
            animator.SetBool("Acc", false);
        }
    }
}
