using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool hasActions;
    public int actionPoints;
    public int remainingActionPoints;

    /// <summary>
    /// Setup character controller.
    /// </summary>
    void Start()
    {
        TurnManager.Instance.SubscribeCharacter(this);
    }

    /// <summary>
    /// Handles start of turn processes.
    /// </summary>
    public void StartTurn()
    {
        hasActions = true;
        enabled = true;
        remainingActionPoints = actionPoints;
        OnStartTurn();
    }

    /// <summary>
    /// Handle character specific start turn behavior.
    /// </summary>
    virtual public void OnStartTurn()
    {

    }

    /// <summary>
    /// Handles end of turn processes.
    /// </summary>
    public void EndTurn()
    {
        remainingActionPoints = 0;
        enabled = false;
        TurnManager.Instance.EndTurn(this);
        OnEndTurn();
    }

    /// <summary>
    /// Handle character specific end turn behavior.
    /// </summary>
    virtual public void OnEndTurn()
    {

    }
}
