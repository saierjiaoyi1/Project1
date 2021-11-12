using UnityEngine;
using System.Collections;

public class CatAiBehaviour : Ticker
{
    private Cat _cat;

    public CheckPointAnalyser checkPointAnalyser;

    public AiConditionFetcher aiConditionFetcher;

    public ActivityData currentActivity { get; private set; }

    private bool _isStunned;//in forced movement (jumping / in toilet/ stunned)
    private string _lastActivityId;

    private void Awake()
    {
        _cat = GetComponent<Cat>();
    }

    protected override void Tick()
    {
        if (currentActivity != null)
        {
            if (currentActivity.hasDuration)
            {
                currentActivity.durationTimer -= TickTime;
                if (currentActivity.durationTimer <= 0)
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
            if (_eatTimer > 0)
            {
                _eatTimer -= TickTime;
                if (_eatTimer <= 0)
                {
                    EndEat();
                }
            }
            if (_toiletTimer > 0)
            {
                _toiletTimer -= TickTime;
                if (_toiletTimer <= 0)
                {
                    EndToilet();
                }
            }
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
        if (currentActivity != null && currentActivity.piority >= newActivity.piority)
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
        if (currentActivity == null)
        {
            return;
        }

        switch (currentActivity.id)
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
        if (currentActivity == null)
        {
            return;
        }

        switch (currentActivity.id)
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

        currentActivity = null;
    }

    public void Flush()
    {
        _lastActivityId = "";
        ExitActivity();
    }

    protected void EnterActivity(ActivityPrototype a)
    {
        _lastActivityId = a.id;

        currentActivity = new ActivityData();
        currentActivity.id = a.id;
        currentActivity.piority = a.piority;
        currentActivity.hasDuration = a.hasDuration;
        currentActivity.durationTime = a.duration;
        currentActivity.durationTimer = currentActivity.durationTime;

        Debug.Log("--EnterActivity " + a.id);
        ProcessCurrentActivity(true);
    }

    private CatAction _lastAction;
    public void ProcessCurrentActivity(bool enter = false)
    {
        CatAction action = checkPointAnalyser.GetAction(currentActivity.id, enter, _lastAction);
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

    private float _eatTimer;
    private float _toiletTimer;

    public void StartEat()
    {
        SetStunned(true);
        _cat.cm.Stop();
        _cat.cac.doEat = true;
        _eatTimer = ConfigService.instance.catActivities.goEat.coreMovementDuration;

        SoundService.instance.Play("eat");
    }

    public void StartToilet()
    {
        SetStunned(true);
        _cat.cm.Stop();
        _cat.cac.doEat = true;
        _toiletTimer = ConfigService.instance.catActivities.goToilet.coreMovementDuration;

        _cat.cm.cc.enabled = false;
        _cat.cm.navMeshAgent.enabled = false;
        var toiletTran = LevelSystem.instance.levelBehaviour.toilet.toiletCenter;
        transform.SetPositionAndRotation(toiletTran.position, toiletTran.rotation);

        SoundService.instance.Play("eat");
    }

    void EndEat()
    {
        aiConditionFetcher.OnEatFinished();

        SetStunned(false);
        ExitActivity();
    }

    void EndToilet()
    {
        aiConditionFetcher.OnToiletFinished();
        var toiletTran = LevelSystem.instance.levelBehaviour.toilet.transform;
        transform.SetPositionAndRotation(toiletTran.position, toiletTran.rotation);

        _cat.cm.cc.enabled = true;

        SetStunned(false);
        ExitActivity();
    }
}
