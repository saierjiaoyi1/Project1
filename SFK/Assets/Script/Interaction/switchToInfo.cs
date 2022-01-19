using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class switchToInfo : MonoBehaviour
{
    public GameObject wikiPanel;
    public GameObject catInfoPanel;
    public GameObject catmodel;
    public GameObject go;

    public CatConfig cfg;
    public Text text;

    public void switchPanel()
    {
        setConfig();
        wikiPanel.SetActive(false);
        catInfoPanel.SetActive(true);
    }

    public void setConfig()
    {
        go.transform.localEulerAngles = new Vector3(0, -145, 0);
        go.GetComponent<Animator>().Play("Idle", 0, 0);
        catmodel.GetComponent<Renderer>().material = cfg.mat;
        text.text = 
            "Name:<size=36%><color=#55DDDD>" + cfg.title + "</color></size>\n\n" + 
            cfg.desc + 
            "\n\nSpeed:" + cfg.speed + 
            "\nHunger Endurance:" + cfg.hungerEndurance + 
            "\nToilet Endurance:" + cfg.toiletEndurance + 
            "\nAlert Range:" + cfg.alertRange;
    }
}
