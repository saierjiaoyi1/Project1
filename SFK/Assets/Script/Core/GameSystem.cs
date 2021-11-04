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

    private void Start()
    {
        PrepareGame();
    }

    public void PrepareGame()
    {
        SetupLevel();
    }

    public void EnterGame()
    {
        CameraMove.instance.StartMove();
    }

    void SetupLevel()
    {
        LevelSystem.instance.CreateNewLevel(LevelSystem.instance.GetNextLevel());
    }

    public void StartGame()
    {
        EnableGameplayController();
        //cat go out

        //play sound
    }

    void EnableGameplayController()
    {
        GameSystem.instance.state = GameSystem.GameState.Playing;
    }
}
