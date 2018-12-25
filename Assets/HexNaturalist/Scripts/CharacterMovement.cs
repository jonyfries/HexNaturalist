using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Hex location;
    public MentalMap mentalMap;

    void Start()
    {
        transform.position = location.GetWorldPosition();
        Invoke("LateStart", .1f);
    }

    /// <summary>
    /// Creates the MentalMap for this character.
    /// </summary>
    void LateStart()
    {
        mentalMap = new MentalMap(this);
    }
}
