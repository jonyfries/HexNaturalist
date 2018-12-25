using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HighlightPlus.HighlightEffect))]

public class HexSelection : MonoBehaviour
{
    public HighlightPlus.HighlightEffect highlight; 

    void OnMouseEnter()
    {
        highlight.SetHighlighted(true);
        PlayerMovement.highlightedHex = gameObject.GetComponent<Hex>();
    }

    void OnMouseExit()
    {
        highlight.SetHighlighted(false);
    }
}
