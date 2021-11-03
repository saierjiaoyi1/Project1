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

    private void Awake()
    {
        instance = this;
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
        LevelRuntimeData data = new LevelRuntimeData();
        data.id = cfg.id;
        data.usableItemsCount = cfg.usableItemsCount;
        data.startTime = Time.time;
    }
}

public class LevelRuntimeData
{
    public string id;

    public UsableItemsCount usableItemsCount;

    public float startTime;
}