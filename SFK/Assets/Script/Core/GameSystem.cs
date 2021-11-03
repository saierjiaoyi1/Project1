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

    public GameState state { get; private set; }

    public void PrepareGame()
    {
        SetupLevel();
    }

    void SetupLevel()
    {

    }

    public void StartGame()
    {
        EnableGameplayController();
    }

    void CatFlee()
    {
        //cat go out

        //play sound
        Cat.cats[0].Test();
    }

    void EnableGameplayController()
    {
        GameSystem.instance.state = GameSystem.GameState.Playing;
    }
}
