using UnityEngine;
using UnityEngine.UI;

public class LevelBehaviour : MonoBehaviour
{
    public Level level;

    public UsableItemUiBehaviour uiub_bug;
    public UsableItemUiBehaviour uiub_catSnack;
    public UsableItemUiBehaviour uiub_catToiletAcc;

    public Cat cat1;
    public Cat cat2;
    public Cat cat3;
    public Cat cat4;

    public Human human;

    public Toilet toilet;
    public EatPlace eatPlace;

    public Text levelTitle;
    public Text levelTitle_static;
    public Text levelDesc;

    public void Recreate(LevelConfig cfg)
    {
        levelTitle_static.text = cfg.title;
        levelTitle.text = cfg.title;
        levelDesc.text = cfg.desc;

        SyncItems(cfg.usableItemsCount);

        human.Init();
        cat1.Init(cfg.cat1, level.spawnPos_cat1);
        cat2.Init(cfg.cat2, level.spawnPos_cat2);
        cat3.Init(cfg.cat3, level.spawnPos_cat3);
        cat4.Init(cfg.cat4, level.spawnPos_cat4);
    }

    public void StartPlay()
    {
        //cat go out
        //play sound
        cat1.RestartAi();
        cat2.RestartAi();
        cat3.RestartAi();
        cat4.RestartAi();
    }

    public void SyncItems(UsableItemsCount usableItemsCount)
    {
        uiub_bug.SetCount(usableItemsCount.bug);
        uiub_catSnack.SetCount(usableItemsCount.catSnack);
        uiub_catToiletAcc.SetCount(usableItemsCount.catToiletAcc);
    }
}
