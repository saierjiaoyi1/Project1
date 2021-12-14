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
        catmodel.GetComponent<Renderer>().material = cfg.mat;
        text.text = "Name:\t\t<size=36%><color=#55DDDD>" + cfg.title + "</color></size>\n" + cfg.desc + "\n\nSpeed:\t\t" + cfg.speed + "\n\nHunger Endurance:\t\t" + cfg.hungerEndurance
            + "\nToilet Endurance:\t\t" + cfg.toiletEndurance + "\nAlert Range:\t\t" + cfg.alertRange;
    }
}
