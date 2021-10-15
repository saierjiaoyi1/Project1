using UnityEngine;
using System.Collections;

public class AiConditionFetcher : MonoBehaviour
{
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
            return HungryRatio <= 0;
        }
    }

    public bool HasNatureCall
    {
        get
        {
            return HungryRatio <= 0;
        }
    }

    public float HungryRatio
    {
        get
        {
            return 100;//TODO
        }
    }

    public float NatureCallRatio
    {
        get
        {
            return 100;//TODO
        }
    }
}
