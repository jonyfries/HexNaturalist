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
        PlayerController.highlightedHex = hex;
    }

    void OnMouseExit()
    {
        highlight.SetHighlighted(false);
        if (PlayerController.highlightedHex == hex) PlayerController.highlightedHex = null;
    }
}
