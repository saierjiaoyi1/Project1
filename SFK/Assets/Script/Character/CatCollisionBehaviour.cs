using UnityEngine;

public class CatCollisionBehaviour : MonoBehaviour
{
    public Checkpoint currentCheckpoint { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        var cp = other.gameObject.GetComponent<Checkpoint>();
        if (cp != null)
        {
            currentCheckpoint = cp;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var cp = other.gameObject.GetComponent<Checkpoint>();
        if (cp != null && cp == currentCheckpoint)
        {
            currentCheckpoint = null;
        }
    }
}