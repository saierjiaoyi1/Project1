using UnityEngine;
using System.Collections.Generic;

public class Human : MonoBehaviour
{
    public HumainMove hm;

    private void Awake()
    {
    }

    public void Init()
    {
        Debug.Log("Init human");
        var trans = LevelSystem.instance.levelBehaviour.level.spawnPos_human;
        hm.ResetMove();
        transform.SetPositionAndRotation(trans.position, trans.rotation);
    }

    public void Caught()
    {
        hm.Caught();
    }
}
