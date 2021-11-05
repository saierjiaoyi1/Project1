using UnityEngine;
using System.Collections;

[System.Serializable]
public class ActivityPrototype
{
    public string id;
    public string desc;

    public int piority;
    public int happenChancePercent;

    public bool hasDuration;

    public float durationMin;
    public float durationMax;
    public float duration
    {
        get
        {
            return Random.Range(durationMin, durationMax);
        }
    }
}
