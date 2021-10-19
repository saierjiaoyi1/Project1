using UnityEngine;
using System.Collections.Generic;

public class Cat : MonoBehaviour
{
    public static List<Cat> cats = new List<Cat>();

    public string catName;

    public CatAnimationController cac;
    public CatAiBehaviour cab;


    private void Awake()
    {
        cats.Add(this);
    }

    public void Test()
    {
        Debug.Log("flee and jump");
    }
}
