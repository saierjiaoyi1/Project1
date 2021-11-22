using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonOverride : Button
{
    public GameObject info;
    public GameObject opt;
    public string optt;
    protected override void DoStateTransition(SelectionState state, bool instant)
    {

        base.DoStateTransition(state, instant);

        switch (state)

        {

            case SelectionState.Disabled:

                break;

            case SelectionState.Highlighted:

                this.GetComponent<ButtonHigh>().run();
                //transform.parent.parent.transform.FindChild("Opt1Information")
                //GameObject.Find(optt).transform.SetAsFirstSibling();
                //GameObject.Find("Opt1").transform.SetAsFirstSibling();
                //GameObject.Find("Opt1").

                //Info.SetActive(true);
                //Opt.transform.SetAsFirstSibling();

                break;

            case SelectionState.Normal:

                this.GetComponent<ButtonHigh>().unrun();
                //Info.SetActive(false);

                break;

            case SelectionState.Pressed:

                break;

            default:

                break;

        }

    }

}
