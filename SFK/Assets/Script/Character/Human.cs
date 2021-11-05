using UnityEngine;
using System.Collections.Generic;

public class Human : MonoBehaviour
{
    public HumainMove hm;
    public ChatBubble chatBubble;

    public void Init()
    {
        gameObject.SetActive(true);
        //Debug.Log("Init human");
        hm.ResetMove();
        var trans = LevelSystem.instance.levelBehaviour.level.spawnPos_human;
        transform.position = trans.position;

        hm.cc.enabled = true;
        //Debug.LogWarning("ResetMove");
        //Debug.Log(trans.position);
        //Debug.Log(transform.position);
        //hm.ResetMove();
    }

    public void Caught()
    {
        hm.Caught();
        chatBubble.Say("Catch you!");
    }
}
