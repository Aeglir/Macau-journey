using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HighlightedButton : UnityEngine.UI.Button
{
    private HighlightedImage HighlightedSprite;
    protected override void Awake()
    {
        base.Awake();
        HighlightedSprite = GetComponentInChildren<HighlightedImage>();
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
                    break;
                case SelectionState.Highlighted:
                    HighlightedSprite.SetHighlighted();
                    break;
                case SelectionState.Pressed:
                    HighlightedSprite.SetDefault();
                    break;
                case SelectionState.Selected:
                    HighlightedSprite.SetDefault();
                    break;
                case SelectionState.Disabled:
                    HighlightedSprite.SetDefault();
                    break;
            }
        }
    }
}
