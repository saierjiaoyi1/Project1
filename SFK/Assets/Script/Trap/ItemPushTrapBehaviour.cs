using UnityEngine;
using System.Collections.Generic;

public class ItemPushTrapBehaviour : MonoBehaviour
{
    public List<GameObject> items;
    public Transform forceOrigin;
    public float force;

    private bool _triggered;

    private void OnTriggerEnter(Collider other)
    {
        if (_triggered)
        {
            return;
        }


        if (other.tag == "DemoCat")
        {
            _triggered = true;
            foreach (var item in items)
            {
                var rb = item.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;

                var deltaVec = item.transform.position - forceOrigin.position;
                var forceVector = deltaVec.normalized * force;

                rb.AddForceAtPosition(forceVector, forceOrigin.position);
            }
        }
    }
}
