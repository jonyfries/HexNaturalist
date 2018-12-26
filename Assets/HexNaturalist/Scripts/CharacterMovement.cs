using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
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
    public MentalMap mentalMap;
    public float moveTime = .5f;

    [SerializeField] private Hex _location;
    private List<Hex> movementPath;

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

    /// <summary>
    /// Move character to next location in the path.
    /// </summary>
    public void Move()
    {
        location = movementPath[0];
        movementPath.RemoveAt(0);
        if (movementPath.Count != 0)
        {
            Invoke("Move", moveTime);
        }
    }

    /// <summary>
    /// Sets the path for the character to follow.
    /// </summary>
    /// <param name="newPath">Path for character to follow.</param>
    public void SetPath(List<Hex> newPath)
    {
        if (newPath.Count == 0) return;

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
