using UnityEngine;
using System.Collections;

public class MoveTo3 : MoveTo2
{
    public float tickTime = 1;
    private float _tickTimer;

    protected override void Update()
    {
        _tickTimer -= Time.deltaTime;
        if (_tickTimer < 0)
        {
            _tickTimer = tickTime;
            resetGoal = true;
        }

        base.Update();
    }
}
