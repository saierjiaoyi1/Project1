using UnityEngine;
using System.Collections;

[System.Serializable]
public class ActivityPrototype
{
    public string title;
    public string desc;

    public int piority;
    public int happenChancePercent;

    public float durationMax;
    public float durationMin;
    public float duration
    {
        get
        {
            return Random.Range(durationMin, durationMax);
        }
    }
}
