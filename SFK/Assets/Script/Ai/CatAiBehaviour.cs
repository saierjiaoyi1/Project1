using UnityEngine;
using System.Collections;

public class CatAiBehaviour : Ticker
{
    public ActivityReference activities;

    public CheckPointAnalyser CheckPointAnalyser;

    public AiConditionFetcher aiConditionFetcher;

    private ActivityData _currentActivity;

    protected override void Tick()
    {
        Think();
    }

    private void Think()
    {
        ActivityPrototype newActivity = null;
    }

    protected override void Update()
    {
        base.Update();
    }
}
