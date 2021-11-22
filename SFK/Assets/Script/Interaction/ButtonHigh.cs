using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHigh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject info;
    public string opt;

    public void run()
    {
        info.SetActive(true);
    }

    public void unrun()
    {
        info.SetActive(false);
    }
}
