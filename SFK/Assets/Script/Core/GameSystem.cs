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
        CameraMove.instance.ResetPosition();
        GameSystem.instance.state = GameSystem.GameState.None;
        LevelSystem.instance.CreateNewLevel(LevelSystem.instance.GetNextLevel());
        UiSystem.instance.OnPreplay();
    }

    public void EnterGame()
    {
        CameraMove.instance.StartMove();
    }

    public void OnEnterPlayDone()
    {
        EnableGameplayController();
        UiSystem.instance.OnEnterPlayDone();
        //cat go out
        //play sound
    }

    void EnableGameplayController()
    {
        GameSystem.instance.state = GameSystem.GameState.Playing;
    }

    public void CheckWin()
    {
        var cat1Valid = LevelSystem.instance.levelBehaviour.cat1.isActiveAndEnabled;
        var cat2Valid = LevelSystem.instance.levelBehaviour.cat2.isActiveAndEnabled;
        var cat3Valid = LevelSystem.instance.levelBehaviour.cat3.isActiveAndEnabled;
        var cat4Valid = LevelSystem.instance.levelBehaviour.cat4.isActiveAndEnabled;
        if (cat1Valid || cat2Valid || cat3Valid || cat4Valid)
        {
            return;
        }

        Win();
    }

    public void Win()
    {
        LevelSystem.instance.RegisterLevelPass();
        PrepareGame();
    }
}
