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
        if (_currentActivity != null)
        {
            if (_currentActivity.hasDuration)
            {
                _currentActivity.durationTimer -= TickTime;
                if (_currentActivity.durationTimer <= 0)
                {
                    ExitActivity();
                }
            }
        }
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
        Debug.Log("newActivity " + newActivity.id);
        if (newActivity != null)
        {
            if (_currentActivity != null && _currentActivity.piority >= newActivity.piority)
            {
                Debug.Log("still currentActivity");
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
        var isAfterIdle = _currentActivity != null
            && (_currentActivity.id == "stay" || _currentActivity.id == "walk" || _currentActivity.id == "change");

        var NearbyHuman = aiConditionFetcher.NearbyHuman;
        if (activities.alertRun.piority > activity.piority && NearbyHuman != null)
        {
            activity = activities.alertRun;
        }
        if (activities.goToilet.piority > activity.piority && aiConditionFetcher.HasNatureCall)
        {
            activity = activities.goToilet;
        }
        if (activities.goEat.piority > activity.piority && aiConditionFetcher.IsHungry)
        {
            activity = activities.goEat;
        }
        var NearbyItem = aiConditionFetcher.NearbyItem;
        if (activities.checkNearbyThings.piority > activity.piority && NearbyItem != null)
        {
            activity = activities.checkNearbyThings;
        }
        if (activities.changeRoom.piority > activity.piority && isAfterIdle && Random.Range(0, 100) <= activities.changeRoom.happenChancePercent)
        {
            activity = activities.changeRoom;
        }
        if (activities.stay.piority > activity.piority && isAfterIdle && Random.Range(0, 100) <= activities.stay.happenChancePercent)
        {
            activity = activities.stay;
        }

        return activity;
    }

    public void ExitActivity()
    {
        if (_currentActivity == null)
        {
            return;
        }

        switch (_currentActivity.id)
        {
            case "run":
                break;

            case "toilet":
                break;

            case "eat":
                break;

            case "item":
                break;

            case "change":
                break;

            case "stay":
                break;

            case "walk":
                break;
        }

        _currentActivity = null;
    }

    protected void EnterActivity(ActivityPrototype a)
    {
        _currentActivity = new ActivityData();
        _currentActivity.id = a.id;
        _currentActivity.piority = a.piority;
        _currentActivity.hasDuration = a.hasDuration;
        _currentActivity.durationTime = a.duration;
        _currentActivity.durationTimer = _currentActivity.durationTime;

        Debug.Log("EnterActivity " + a.id);
        ProcessCurrentActivity();
    }

    public void ProcessCurrentActivity()
    {
        switch (_currentActivity.id)
        {
            case "run":
                break;

            case "toilet":
                break;

            case "eat":
                break;

            case "item":
                break;

            case "change":
                break;

            case "stay":
                break;

            case "walk":
                break;
        }
    }
}
