using UnityEngine;
using System.Collections;

public class Football : MonoBehaviour
{
    public IYouCanUse move;

    // Use this for initialization
    void Start()
    {
        move.DoThat();
        move.DoThis();
    }

    void Update()
    {

    }
}
