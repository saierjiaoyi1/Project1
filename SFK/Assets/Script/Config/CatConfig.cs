using UnityEngine;
using System;

[CreateAssetMenu]
[Serializable]
public class CatConfig : ScriptableObject
{
    public string id;
    public string title;
    public string desc;

    [Range(1, 5)]
    public float speed;
    [Range(30, 180)]
    public float hungerEndurance;
    [Range(30, 180)]
    public float toiletEndurance;
    [Range(2, 10)]
    public float alertRange;

    public Material mat;
}
