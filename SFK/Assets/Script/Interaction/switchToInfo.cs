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
        text.text = "Name:" + cfg.id + "\nSpeed:" + cfg.speed + "\nMax hunger:" + cfg.hungerEndurance
            + "\nMax toilet:" + cfg.toiletEndurance + "\nAlert range:" + cfg.alertRange;
    }
}
