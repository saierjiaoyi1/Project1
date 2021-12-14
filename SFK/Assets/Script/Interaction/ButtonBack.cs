using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBack : MonoBehaviour
{
    public GameObject thisPanel;
    public GameObject lastPanel;

    public void Back()
    {
        thisPanel.SetActive(false);
        lastPanel.SetActive(true);
    }
}
