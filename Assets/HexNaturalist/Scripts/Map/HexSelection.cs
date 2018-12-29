using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HighlightPlus.HighlightEffect))]

public class HexSelection : MonoBehaviour
{
    public HighlightPlus.HighlightEffect highlight;
    [SerializeField] private Hex hex;

    void OnMouseEnter()
    {
        highlight.SetHighlighted(true);
        PlayerMovement.highlightedHex = hex;
    }

    void OnMouseExit()
    {
        highlight.SetHighlighted(false);
        if (PlayerMovement.highlightedHex == hex) PlayerMovement.highlightedHex = null;
    }
}
