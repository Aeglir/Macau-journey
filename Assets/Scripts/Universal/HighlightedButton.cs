using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class HighlightedButton : UnityEngine.UI.Button
{
    private HighlightedImage HighlightedSprite;
    private HightedButtonEventHelper helper;
    protected override void Awake()
    {
        base.Awake();
        HighlightedSprite = GetComponentInChildren<HighlightedImage>();
        helper = GetComponent<HightedButtonEventHelper>();
    }
    protected override void DoStateTransition(SelectionState state, bool instance)
    {
        base.DoStateTransition(state, instance);
        if (HighlightedSprite != null)
        {
            switch (state)
            {
                case SelectionState.Normal:
                    HighlightedSprite.SetDefault();
                    if(helper!=null&&helper.defaultevent!=null) helper.defaultevent.Invoke();
                    break;
                case SelectionState.Highlighted:
                    HighlightedSprite.SetHighlighted();
                    if(helper!=null&&helper.heightedevent!=null) helper.heightedevent.Invoke();
                    break;
                case SelectionState.Pressed:
                    HighlightedSprite.SetPressed();
                    if(helper!=null&&helper.pressedevent!=null) helper.pressedevent.Invoke();
                    break;
                case SelectionState.Selected:
                    HighlightedSprite.SetSelected();
                    if(helper!=null&&helper.selectedevent!=null) helper.selectedevent.Invoke();
                    break;
                case SelectionState.Disabled:
                    HighlightedSprite.SetDisable();
                    if(helper!=null&&helper.disableevent!=null) helper.disableevent.Invoke();
                    break;
            }
        }
    }
}
