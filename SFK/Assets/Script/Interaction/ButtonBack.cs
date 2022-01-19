using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBack : MonoBehaviour
{
    public GameObject thisPanel;
    public GameObject lastPanel;
    public GameObject bg;
    public bool ifbg;

    public void Back()
    {
        thisPanel.SetActive(false);
        lastPanel.SetActive(true);

        if(ifbg)
        {
            bg.SetActive(false);
        }
    }
}
