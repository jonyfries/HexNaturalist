using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOverHighlight : MonoBehaviour
{
    public HighlightPlus.HighlightEffect highlight;

    void OnMouseEnter()
    {
        highlight.SetHighlighted(true);
    }

    void OnMouseExit()
    {
        highlight.SetHighlighted(false);
    }
}
