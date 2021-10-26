using UnityEngine;
using System.Collections.Generic;

public class SayTriggerBehaviour : MonoBehaviour
{
    public string text;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Human")
        {

            var cb = other.gameObject.GetComponentInChildren<ChatBubble>();
            cb.Say(text);
        }
    }
}