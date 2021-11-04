using UnityEngine;
using System.Collections;

public class UiSystem : MonoBehaviour
{
    public static UiSystem instance;
    public GameObject gamePanel;
    public GameObject prePanel;

    private void Awake()
    {
        instance = this;
    }

    public void OnPreplay()
    {
        prePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    public void OnClickPlay()
    {
        prePanel.SetActive(false);
        GameSystem.instance.EnterGame();
    }

    public void OnEnterPlayDone()
    {
        gamePanel.SetActive(true);
    }

    public void OnClickItem(UsableItemUiBehaviour uiub)
    {
        LevelSystem.instance.TryUseItem(uiub.id);
    }
}
