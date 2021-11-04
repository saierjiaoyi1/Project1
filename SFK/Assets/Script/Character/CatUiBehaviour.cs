using UnityEngine;
using UnityEngine.UI;

public class CatUiBehaviour : MonoBehaviour
{
    public GameObject view;
    public GameObject barView;
    public GameObject caughtView;
    public Slider barHunger;
    public Slider barToilet;

    public void Hide()
    {
        view.SetActive(false);
    }

    public void SetCaught()
    {
        view.SetActive(true);
        barView.SetActive(false);
        caughtView.SetActive(true);
    }

    public void SetFree()
    {
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
