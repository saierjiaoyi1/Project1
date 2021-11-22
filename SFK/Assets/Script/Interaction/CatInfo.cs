using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatInfo : MonoBehaviour
{
    public CatConfig cfg;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Name:" + cfg.id + "\nName:" + cfg.speed + "\nMax hunger:" + cfg.hungerEndurance
            + "\nMax toilet:" + cfg.toiletEndurance + "\nAlert range:" + cfg.alertRange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
