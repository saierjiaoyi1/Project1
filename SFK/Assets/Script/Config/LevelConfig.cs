using UnityEngine;
using System;

[CreateAssetMenu]
[Serializable]
public class LevelConfig : ScriptableObject
{
    public string id;

    public CatConfig cat1;
    public CatConfig cat2;
    public CatConfig cat3;
    public CatConfig cat4;

    public UsableItemsCount usableItemsCount;
}

public struct UsableItemsCount
{
    public int bug;
    public int catSnack;
    public int catToiletAcc;
}
