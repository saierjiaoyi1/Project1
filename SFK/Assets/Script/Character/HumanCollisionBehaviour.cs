using UnityEngine;
using System.Collections.Generic;

public class HumanCollisionBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (GameSystem.instance.state != GameSystem.GameState.Playing)
        {
            return;
        }
        if (other.tag == "Cat")
        {
            other.GetComponentInParent<Cat>().Caught();
            LevelSystem.instance.levelBehaviour.human.Caught();
            //Debug.Log("catch!");
        }
    }
}