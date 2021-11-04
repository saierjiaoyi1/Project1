using UnityEngine;
using System.Collections;

public class AiConditionFetcher : Ticker
{
    public int hungerToleranceMax;
    public int toiletToleranceMax;
    private int _hungerValue;
    private int _toiletValue;
    private float _alertRange;

    private Cat _cat;

    private void Awake()
    {
        _cat = GetComponent<Cat>();
    }
    public Transform NearbyHuman
    {
        get
        {
            return null;//TODO
        }
    }

    public Transform NearbyItem
    {
        get
        {
            return null;//TODO
        }
    }

    protected override void Tick()
    {
        if (GameSystem.instance.state != GameSystem.GameState.Playing)
        {
            return;
        }

        _hungerValue += 1;
        _toiletValue += 1;
        _cat.cub.SetHunger(HungryRatio);
        _cat.cub.SetToilet(NatureCallRatio);
    }

    public bool HasDetectedHuman()
    {
        return NearbyHuman != null;
    }

    public bool HasDetectedItem()
    {
        return NearbyItem != null;
    }

    public bool IsHungry
    {
        get
        {
            return HungryRatio >= 1;
        }
    }

    public bool HasNatureCall
    {
        get
        {
            return NatureCallRatio >= 1;
        }
    }

    public float HungryRatio
    {
        get
        {
            return (float)_hungerValue / hungerToleranceMax;
        }
    }

    public float NatureCallRatio
    {
        get
        {
            return (float)_toiletValue / toiletToleranceMax;
        }
    }

    public void SetHungerMax(int v)
    {
        _hungerValue = 0;
        hungerToleranceMax = v;
    }

    public void SetToiletMax(int v)
    {
        _toiletValue = 0;
        toiletToleranceMax = v;
    }

    public void SetAlertRange(float v)
    {
        _alertRange = v;
    }
}
