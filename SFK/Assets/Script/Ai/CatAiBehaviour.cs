using UnityEngine;
using System.Collections;

public class CatAiBehaviour : Ticker
{
    public CheckPointAnalyser checkPointAnalyser;

    public AiConditionFetcher aiConditionFetcher;

    private ActivityData _currentActivity;

    private bool _isStunned;//in forced movement (jumping / in toilet/ stunned)

    protected override void Tick()
    {
        if (CanThink())
        {
            Think();
        }
    }

    bool CanThink()
    {
        //game end(catched)/game start
        //in forced movement (jumping / in toilet/ stunned)
        if (GameSystem.instance.state != GameSystem.GameState.Playing)
        {
            return false;
        }
        if (_isStunned)
        {
            return false;
        }
        return true;
    }

    public void SetStunned(bool b)
    {
        _isStunned = b;
    }

    private void Think()
    {
        ActivityPrototype newActivity = GetNewActivity();
        if (newActivity != null)
        {
            if (_currentActivity != null && _currentActivity.piority >= newActivity.piority)
            {
                Debug.Log("still doing _currentActivity");
            }
            else
            {
                EnterActivity(newActivity);
            }
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected ActivityPrototype GetNewActivity()
    {
        var activities = ConfigService.instance.catActivities;
        var activity = activities.walkaround;

        var NearbyHuman = aiConditionFetcher.NearbyHuman;
        if (NearbyHuman != null)
        {
            activity = activities.alertRun;
        }


        return activity;
    }

    protected void EnterActivity(ActivityPrototype a)
    {
        _currentActivity = new ActivityData();
        _currentActivity.id = a.id;
        _currentActivity.piority = a.piority;
        _currentActivity.durationTime = a.duration;
        _currentActivity.durationTimer = _currentActivity.durationTime;

        Debug.Log("EnterActivity " + a.id);
    }

    public void FlushActivity()
    {
        _currentActivity = null;
    }
}
