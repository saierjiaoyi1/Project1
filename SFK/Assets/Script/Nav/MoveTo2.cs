using UnityEngine;
using UnityEngine.AI;

public class MoveTo2 : MoveTo
{
    public bool resetGoal;

    protected virtual void Update()
    {
        if (resetGoal)
        {
            resetGoal = false;
            SetGoal();
        }
    }
}