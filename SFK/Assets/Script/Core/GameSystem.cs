using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public enum GameState
    {
        None,
        Playing,
        Win,
    }

    public GameState state;
    
}
