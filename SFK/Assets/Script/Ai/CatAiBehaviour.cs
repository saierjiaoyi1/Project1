using UnityEngine;
using System.Collections;

public class CatAiBehaviour : Ticker
{
    public ActivityReference activities;

    public CheckPointAnalyser checkPointAnalyser;

    public AiConditionFetcher aiConditionFetcher;

    private ActivityData _currentActivity;

    protected override void Tick()
    {
        if (GameSystem.instance.state != GameSystem.GameState.Playing)
        {
            return;
        }

        Think();
    }

    private void Think()
    {
        ActivityPrototype newActivity = GetNewActivity();
        if (newActivity != null)
        {
            if (_currentActivity == null)
            {
                EnterActivity(newActivity);
            }
            else
            {
                if (_currentActivity.piority >= newActivity.piority)
                {
                    Debug.Log("still doing _currentActivity");
                }
                else
                {
                    EnterActivity(newActivity);
                }
            }
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    protected ActivityPrototype GetNewActivity()
    {
        return null;
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
}
