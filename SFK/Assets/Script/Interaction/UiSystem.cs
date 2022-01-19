using UnityEngine;
using System.Collections;

public class UiSystem : MonoBehaviour
{
    public static UiSystem instance;
    public GameObject gamePanel;
    public GameObject prePanel;
    public GameObject wikiPanel;
    public GameObject catInfoPanel;
    public GameObject bg;


    private void Awake()
    {
        instance = this;
    }

    public void OnPreplay()
    {
        prePanel.SetActive(true);
        gamePanel.SetActive(false);
        wikiPanel.SetActive(false);

        catInfoPanel.SetActive(false);
        bg.SetActive(false);
    }

    public void OnClickPlay()
    {
        prePanel.SetActive(false);
        GameSystem.instance.EnterGame();
    }

    public void OnClickWiki()
    {
        prePanel.SetActive(false);
        wikiPanel.SetActive(true);
        bg.SetActive(true);
    }

    public void OnClickCatInfo()
    {
        wikiPanel.SetActive(false);
        catInfoPanel.SetActive(true);
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
