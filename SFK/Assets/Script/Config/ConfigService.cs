using UnityEngine;
using System.Collections.Generic;

public class ConfigService : MonoBehaviour
{
    public static ConfigService instance;

    public List<LevelConfig> levels;

    public ActivityReference catActivities;

    private void Awake()
    {
        instance = this;
    }
}
