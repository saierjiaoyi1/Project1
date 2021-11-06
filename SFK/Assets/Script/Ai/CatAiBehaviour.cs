using UnityEngine;
using System.Collections;

public class CatAiBehaviour : Ticker
{
    private Cat _cat;

    public CheckPointAnalyser checkPointAnalyser;

    public AiConditionFetcher aiConditionFetcher;

    private ActivityData _currentActivity;

    private bool _isStunned;//in forced movement (jumping / in toilet/ stunned)
    private string _lastActivityId;

    private void Awake()
    {
        _cat = GetComponent<Cat>();
    }

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
        Debug.Log("try to " + newActivity.id);
        if (_currentActivity != null && _currentActivity.piority >= newActivity.piority)
        {
            //Debug.Log("still currentActivity");
            ProcessCurrentActivity(false);
        }
        else
        {
            EnterActivity(newActivity);
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
        if (activities.changeRoom.piority > activity.piority && (_lastActivityId == "walk") && Random.Range(0, 100) < activities.changeRoom.happenChancePercent)
        {
            activity = activities.changeRoom;
        }
        if (activities.stay.piority > activity.piority && (_lastActivityId == "walk" || _lastActivityId == "change") && Random.Range(0, 100) < activities.stay.happenChancePercent)
        {
            activity = activities.stay;
        }

        return activity;
    }

    public void OnArrived()
    {
        Debug.LogWarning("OnArrived!");
        if (_currentActivity == null)
        {
            return;
        }

        switch (_currentActivity.id)
        {
            case "run":
                if (_lastAction != null)
                {
                    if (_lastAction.isFollow)
                    {
                        var has = _cat.cab.CheckNextFollow();
                        if (!has)
                        {
                            Debug.Log("release from follow");
                            //SetStunned(false);
                        }
                    }
                }
                break;

            case "toilet":

                break;

            case "eat":

                break;

            case "item":

                break;

            case "change":
            case "walk":
                ExitActivity();
                break;
        }
    }

    public void ExitActivity()
    {
        if (_currentActivity == null)
        {
            return;
        }

        switch (_currentActivity.id)
        {
            case "run"://nav exit then nav next exit
                break;
            case "toilet"://nav targetPos to toilet
                break;
            case "eat"://nav targetPos to eat
                break;
            case "item"://nav targetPos to item
                break;
            case "change"://nav targetPos in random nearby room
                break;
            case "stay"://ok
                break;
            case "walk"://ok
                break;
        }

        _currentActivity = null;
    }

    public void Flush()
    {
        _lastActivityId = "";
        ExitActivity();
    }

    protected void EnterActivity(ActivityPrototype a)
    {
        _lastActivityId = a.id;

        _currentActivity = new ActivityData();
        _currentActivity.id = a.id;
        _currentActivity.piority = a.piority;
        _currentActivity.hasDuration = a.hasDuration;
        _currentActivity.durationTime = a.duration;
        _currentActivity.durationTimer = _currentActivity.durationTime;

        Debug.Log("--EnterActivity " + a.id);
        ProcessCurrentActivity(true);
    }

    private CatAction _lastAction;
    public void ProcessCurrentActivity(bool enter = false)
    {
        CatAction action = checkPointAnalyser.GetAction(_currentActivity.id, enter, _lastAction);
        _lastAction = action;
        _cat.Act(action);
    }

    public bool CheckNextFollow()
    {
        var res = checkPointAnalyser.CheckNextFollow(_lastAction);
        if (res == null)
        {
            return false;
        }

        _cat.Act(res);
        return true;
    }
}
