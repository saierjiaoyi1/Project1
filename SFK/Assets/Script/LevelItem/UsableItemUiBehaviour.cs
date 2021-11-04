using UnityEngine;
using UnityEngine.UI;

public class UsableItemUiBehaviour : MonoBehaviour
{
    public Text countTxt;
    public GameObject view;
    public int count { get; private set; }
    public string id;

    public void SetCount(int i)
    {
        count = i;
        if (i <= 0)
        {
            view.SetActive(false);
            return;
        }

        view.SetActive(true);
        countTxt.text = i + "";
    }

    public void OnClick()
    {
        UiSystem.instance.OnClickItem(this);
    }
}
