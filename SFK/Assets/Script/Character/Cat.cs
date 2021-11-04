using UnityEngine;
using System.Collections.Generic;

public class Cat : MonoBehaviour
{
    public static List<Cat> cats = new List<Cat>();

    public string catName;

    public CatAnimationController cac;
    public CatAiBehaviour cab;
    public CatAppearanceBehaviour catAppearanceBehaviour;
    public CatConfig cfg { get; private set; }
    public CatMove cm;

    private void Awake()
    {
        cats.Add(this);
    }

    public void Init(CatConfig cc, Transform spawnTrans)
    {
        Debug.Log("cat Init");
        cfg = cc;

        if (cc == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        catAppearanceBehaviour.mr.material = cc.mat;

        cm.speed = cfg.speed;
        cm.ResetMove();
        cab.aiConditionFetcher.SetHungerMax(cfg.hungerEndurance);
        cab.aiConditionFetcher.SetHungerMax(cfg.toiletEndurance);
        cab.aiConditionFetcher.SetAlertRange(cfg.alertRange);

        transform.SetPositionAndRotation(spawnTrans.position, spawnTrans.rotation);
    }

    private void OnDestroy()
    {
        cats.Remove(this);
    }
}
