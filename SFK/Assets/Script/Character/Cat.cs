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
    }

    public void Caught()
    {
        gameObject.SetActive(false);
        cub.SetCaught();
    }

    public void RestartAi()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        cab.SetStunned(false);
        cab.FlushActivity();
    }

    public void Init(CatConfig cc, Transform spawnTrans)
    {
        //Debug.Log("cat Init");
        //Debug.Log(cc);
        cfg = cc;

        if (cc == null)
        {
            gameObject.SetActive(false);
            cub.Hide();
            return;
        }
        cm.cc.enabled = false;
        cub.Show();
        gameObject.SetActive(true);
        catAppearanceBehaviour.mr.material = cc.mat;

        cm.ResetMove(cfg.speed);
        cab.SetStunned(true);
        cab.aiConditionFetcher.SetHungerMax(cfg.hungerEndurance);
        cab.aiConditionFetcher.SetToiletMax(cfg.toiletEndurance);
        cab.aiConditionFetcher.SetAlertRange(cfg.alertRange);

        transform.SetPositionAndRotation(spawnTrans.position, spawnTrans.rotation);
        cm.cc.enabled = true;
    }

    private void OnDestroy()
    {
        cats.Remove(this);
    }
}
