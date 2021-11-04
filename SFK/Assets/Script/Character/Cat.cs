using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Cat : MonoBehaviour
{
    public static List<Cat> cats = new List<Cat>();

    public string catName;

    public CatAnimationController cac;
    public CatAiBehaviour cab;
    public CatAppearanceBehaviour catAppearanceBehaviour;
    public CatConfig cfg { get; private set; }
    public CatMove cm;
    public CatUiBehaviour cub;

    private void Awake()
    {
        cats.Add(this);
        cub.Hide();
    }

    public void Caught()
    {
        gameObject.SetActive(false);
        cub.SetCaught();
    }

    public void Init(CatConfig cc, Transform spawnTrans)
    {
        Debug.Log("cat Init");
        cfg = cc;

        if (cc == null)
        {
            gameObject.SetActive(false);
            cub.Hide();
            return;
        }

        cub.SetFree();
        gameObject.SetActive(true);
        catAppearanceBehaviour.mr.material = cc.mat;

        cm.ResetMove(cfg.speed);
        cab.aiConditionFetcher.SetHungerMax(cfg.hungerEndurance);
        cab.aiConditionFetcher.SetToiletMax(cfg.toiletEndurance);
        cab.aiConditionFetcher.SetAlertRange(cfg.alertRange);

        transform.SetPositionAndRotation(spawnTrans.position, spawnTrans.rotation);
    }

    private void OnDestroy()
    {
        cats.Remove(this);
    }
}
