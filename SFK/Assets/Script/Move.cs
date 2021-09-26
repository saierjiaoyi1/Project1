using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour, IYouCanUse
{
    public Transform something;

    // Start is called before the first frame update
    void Start()
    {
        P1();
    }

    void P1()
    {
        P2();
    }

    void P2()
    {
        P3();
    }

    void P3()
    {
        something.position = Vector3.one;
    }

    void Update()
    {
        //MoveTransform(transform);
        MovePosition(transform.position);
    }

    public void MoveTransform(Transform t)
    {
        t.position = t.position + new Vector3(0, 1f * Time.deltaTime, 0);
    }

    public void MovePosition(Vector3 position)
    {
        position = position + new Vector3(0, 1f * Time.deltaTime, 0);
    }

    public void DoThis()
    {
       //asdasd
    }

    public void DoThat()
    {
      //adasdasd
    }
}