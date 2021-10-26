using UnityEngine;
using System.Collections.Generic;

public class ConfigService : MonoBehaviour
{
    public static ConfigService instance;
    public List<LevelConfig> levels;

    private void Awake()
    {
        instance = this;
    }
}
