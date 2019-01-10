using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public MentalMap mentalMap;
    public float moveTime = .5f;
    public CharacterVision vision;
    public CharacterController controller;

    public List<Hex> movementPath { get; private set; }
    [SerializeField] private Hex _location;

    public Hex location
    {
        get
        {
            return _location;
        }

        set
        {
            _location = value;
            transform.position = location.GetWorldPosition();
        }
    }

    void Start()
    {
        movementPath = new List<Hex>();
        transform.position = location.GetWorldPosition();
        Invoke("LateStart", .1f);
    }

    /// <summary>
    /// Creates the MentalMap for this character.
    /// </summary>
    void LateStart()
    {
        mentalMap = new MentalMap(this);
        vision.mentalMap = mentalMap;
        vision.OnMove(location);
    }

    /// <summary>
    /// Move character to next location in the path.
    /// </summary>
    public void Move()
    {
        Hex nextHex = movementPath[0];

        if (nextHex.entryCost > controller.remainingActionPoints)
        {
            controller.hasActions = false;
            return;
        } else
        {
            controller.remainingActionPoints -= nextHex.entryCost;
        }

        location = nextHex;
        movementPath.RemoveAt(0);
        vision.OnMove(location);
        if (movementPath.Count != 0)
        {
            Invoke("Move", moveTime);
        } else
        {
            controller.hasActions = false;
        }
    }

    /// <summary>
    /// Sets the path for the character to follow.
    /// </summary>
    /// <param name="newPath">Path for character to follow.</param>
    public void SetPath(List<Hex> newPath)
    {
        if (newPath.Count == 0)
        {
            movementPath = newPath;
            return;
        }

        if (newPath[0] == location)
        {
            newPath.RemoveAt(0);
        }

        movementPath = newPath;

        if (movementPath.Count != 0)
        {
            Move();
        }
    }
}
