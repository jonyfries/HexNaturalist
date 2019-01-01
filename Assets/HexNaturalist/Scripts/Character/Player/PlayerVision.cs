using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : CharacterVision
{
    /// <summary>
    /// Updates the hex as explored for the character.
    /// </summary>
    /// <param name="hex">Hex to explore</param>
    override public void ExploreHex(Hex hex)
    {
        mentalMap.UpdateMap(hex);
        hex.Explore();
    }
}
