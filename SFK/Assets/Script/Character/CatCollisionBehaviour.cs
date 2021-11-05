using UnityEngine;
using System.Collections.Generic;

public class CatCollisionBehaviour : MonoBehaviour
{
    public List<Checkpoint> currentCps;

    private void OnTriggerEnter(Collider other)
    {
        var cp = other.gameObject.GetComponent<Checkpoint>();
        if (cp == null)
            return;

        if (currentCps.Contains(cp))
            return;

        currentCps.Add(cp);
    }

    private void OnTriggerExit(Collider other)
    {
        var cp = other.gameObject.GetComponent<Checkpoint>();
        if (cp == null)
            return;

        if (!currentCps.Contains(cp))
            return;

        currentCps.Remove(cp);
    }
}