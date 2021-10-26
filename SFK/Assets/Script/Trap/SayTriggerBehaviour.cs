using UnityEngine;
using System.Collections.Generic;

public class SayTriggerBehaviour : MonoBehaviour
{
    public string text;
    public bool checkCat;
    public bool checkHuman;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Human" && checkHuman)
        {

            var cb = other.gameObject.GetComponentInChildren<ChatBubble>();
            cb.Say(text);
        }

        if (other.tag == "Cat" && checkCat)
        {

            var cb = other.gameObject.GetComponentInChildren<ChatBubble>();
            cb.Say(text);
        }
    }
}