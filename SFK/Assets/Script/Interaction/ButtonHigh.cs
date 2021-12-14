using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHigh : MonoBehaviour
{
    public GameObject info;
    public Scrollbar scrollbar;
    float a;

    public void run()
    {
        //a = scrollbar.value;
        info.SetActive(true);
        //scrollbar.value = a;
    }

    public void unrun()
    {
        info.SetActive(false);
    }
}
