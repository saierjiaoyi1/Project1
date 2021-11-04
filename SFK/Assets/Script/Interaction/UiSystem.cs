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

    public void OnClickPlay()
    {
        prePanel.SetActive(false);
        gamePanel.SetActive(true);
        GameSystem.instance.EnterGame();
    }

    public void OnClickItem(UsableItemUiBehaviour uiub)
    {
        LevelSystem.instance.TryUseItem(uiub.id);
    }
}
