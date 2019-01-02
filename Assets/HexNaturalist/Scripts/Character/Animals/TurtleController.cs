using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurtleController : CharacterController
{
    public CharacterMovement characterMovement;
    public MentalMap mentalMap;
    public bool isDeciding;

    /// <summary>
    /// Controller setup.
    /// </summary>
	void Start ()
    {
        TurnManager.Instance.SubscribeCharacter(this);
        Invoke("LateStart", .11f);
        enabled = false;
    }

    /// <summary>
    /// Handle mentalMap setup.
    /// </summary>
    void LateStart()
    {
        mentalMap = characterMovement.mentalMap;
        enabled = true;
    }

    /// <summary>
    /// Handle start of turn behavior.
    /// </summary>
    override public void OnStartTurn()
    {
        Move();
    }

    /// <summary>
    /// Check current state.
    /// </summary>
    void Update()
    {
        if (!hasActions || characterMovement.movementPath.Count == 0)
        {
            EndTurn();
        }
    }

    /// <summary>
    /// Start character movement.
    /// </summary>
    void Move()
    {
        List<Hex> path = mentalMap.GetPath(mentalMap.map.ElementAt(Random.Range(0, mentalMap.map.Count)).Value.hex);
        characterMovement.SetPath(path);
        isDeciding = false;
    }
}
