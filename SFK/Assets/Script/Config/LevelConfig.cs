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

    public void Set(string s, int i)
    {
        if (s == "bug")
        {
            bug = i;
        }
        if (s == "catSnack")
        {
            catSnack = i;
        }
        if (s == "catToiletAcc")
        {
            catToiletAcc = i;
        }
    }

    public int Get(string s)
    {
        if (s == "bug")
        {
            return bug;
        }
        if (s == "catSnack")
        {
            return catSnack;
        }
        if (s == "catToiletAcc")
        {
            return catToiletAcc;
        }
        return 0;
    }
}
