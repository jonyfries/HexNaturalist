using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVision : MonoBehaviour
{
    [SerializeField] private int distance;

    public MentalMap mentalMap;

    /// <summary>
    /// Updates the characters mental map as the character moves about.
    /// </summary>
    /// <param name="location">The character's current location</param>
    public void OnMove(Hex location)
    {
        LookHex(location, new List<Hex>(), distance);
    }

    /// <summary>
    /// Look at all hexes within look distance hexes.
    /// </summary>
    /// <param name="hex">Hex to look at.</param>
    /// <param name="seen">List of hexes already looked at, should start as an empty list.</param>
    /// <param name="lookDistance">The distance in hexes to look</param>
    public void LookHex(Hex hex, List<Hex> seen, int lookDistance)
    {
        seen.Add(hex);
        ExploreHex(hex);

        if (lookDistance-- <= 0)
        {
            return;
        }

        foreach (Hex neighbor in hex.neighbors)
        {
            if (!seen.Contains(neighbor))
            {
                LookHex(neighbor, new List<Hex>(seen), lookDistance);
            }
        }
    }

    /// <summary>
    /// Updates the hex as explored for the character.
    /// </summary>
    /// <param name="hex">Hex to explore</param>
    virtual public void ExploreHex(Hex hex)
    {
        mentalMap.UpdateMap(hex);
    }
}
