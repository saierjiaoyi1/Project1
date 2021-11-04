using UnityEngine;
using System.Collections.Generic;

public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;

    public List<string> levelsPassed { get; private set; }

    private List<LevelConfig> levels
    {
        get
        {
            return ConfigService.instance.levels;
        }
    }

    public LevelConfig GetNextLevel()
    {
        var ls = levels;
        foreach (var l in ls)
        {
            bool levelPassed = false;
            foreach (var passed in levelsPassed)
            {
                if (l.id == passed)
                {
                    levelPassed = true;
                    break;
                }
            }
            if (!levelPassed)
            {
                return l;
            }
        }

        return null;
    }

    public LevelConfig GetCurrentLevel()
    {
        if (data == null)
        {
            return null;
        }

        var ls = levels;
        foreach (var l in ls)
        {
            if (l.id == data.id)
            {
                return l;
            }
        }

        return null;
    }

    public LevelRuntimeData data;

    public LevelBehaviour levelBehaviour;

    private void Awake()
    {
        instance = this;
        levelsPassed = new List<string>();
    }

    public void RegisterLevelPass()
    {
        if (!levelsPassed.Contains(data.id))
        {
            levelsPassed.Add(data.id);
        }
    }

    public void CreateNewLevel(LevelConfig cfg)
    {
        data = new LevelRuntimeData();
        data.id = cfg.id;
        data.usableItemsCount = cfg.usableItemsCount;
        data.startTime = Time.time;

        levelBehaviour.Recreate(cfg);
    }

    public void TryUseItem(string id)
    {
        if (CanUseItem(id))
        {
            UseItem(id);
        }
        else
        {
            Debug.Log("can not use item");
        }
    }

    bool CanUseItem(string id)
    {
        if (data.usableItemsCount.Get(id) <= 0)
            return false;

        return true;
    }

    public void UseItem(string id)
    {
        data.usableItemsCount.Set(id, data.usableItemsCount.Get(id) - 1);
        levelBehaviour.SyncItems(data.usableItemsCount);
    }
}

public class LevelRuntimeData
{
    public string id;

    public UsableItemsCount usableItemsCount;

    public float startTime;
}