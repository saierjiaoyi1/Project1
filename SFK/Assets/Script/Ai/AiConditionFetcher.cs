using UnityEngine;
using System.Collections;

public class AiConditionFetcher : MonoBehaviour
{
    public int hungerToleranceMax;
    public int toiletToleranceMax;
    private int _hungerValue;
    public int _toiletValue;

    private float _alertRannge;

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
            return (float)_hungerValue/ hungerToleranceMax;
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
        _alertRannge = v;
    }
}
