using UnityEngine;
using System;

[CreateAssetMenu]
[Serializable]
public class CatConfig : ScriptableObject
{
    public string id;
    public string title;
    public string desc;

    public Sprite catImage;

    [Range(1, 5)]
    public float speed;
    [Range(50, 250)]
    public int hungerEndurance;
    [Range(50, 250)]
    public int toiletEndurance;
    [Range(2, 5)]
    public float alertRange;

    public Material mat;
}
