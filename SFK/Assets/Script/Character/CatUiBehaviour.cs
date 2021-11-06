using UnityEngine;
using UnityEngine.UI;

public class CatUiBehaviour : MonoBehaviour
{
    public GameObject view;
    public GameObject barView;
    public GameObject caughtView;
    public Slider barHunger;
    public Slider barToilet;
    public Text catName;

    public void Hide()
    {
        //Debug.Log("CatUiBehaviour Hide");
        view.SetActive(false);
    }

    public void SetCaught()
    {
        view.SetActive(true);
        barView.SetActive(false);
        caughtView.SetActive(true);
    }

    public void Show(string s)
    {
        //Debug.Log("CatUiBehaviour Show");
        catName.text = s;
        view.SetActive(true);
        barView.SetActive(true);
        caughtView.SetActive(false);
        SetHunger(0);
        SetToilet(0);
    }

    public void SetHunger(float v)
    {
        barHunger.value = v;
    }

    public void SetToilet(float v)
    {
        barToilet.value = v;
    }
}
