using UnityEngine;
using System.Collections.Generic;

public class HumanCollisionBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cat")
        {
            other.GetComponentInParent<Cat>().Caught();
            LevelSystem.instance.levelBehaviour.human.Caught();
            //Debug.Log("catch!");
        }
    }
}