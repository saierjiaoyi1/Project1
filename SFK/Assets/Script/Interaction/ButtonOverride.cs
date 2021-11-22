using UnityEngine;

using System.Collections;

using System;

using UnityEngine.UI;

using UnityEngine.EventSystems;

public class ButtonOverride : Button

{
    public GameObject info;
    public GameObject opt;
    protected override void DoStateTransition(SelectionState state, bool instant)
    {

        base.DoStateTransition(state, instant);

        switch (state)

        {

            case SelectionState.Disabled:

                break;

            case SelectionState.Highlighted:

                //Info.SetActive(true);
                //Opt.transform.SetAsFirstSibling();

                break;

            case SelectionState.Normal:

                //Info.SetActive(false);

                break;

            case SelectionState.Pressed:

                break;

            default:

                break;

        }

    }

}
