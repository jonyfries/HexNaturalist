using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    const int primaryMouse = 0;
    const int middleMouse = 1;
    const int secondaryMouse = 2;
    static public Hex highlightedHex { get; set; }

    public int supplies;
    public GameMenu menu;
    public CharacterMovement movement;
    public UnityEngine.UI.Text actionText;
    public UnityEngine.UI.Text suppliesText;

    void Start()
    {
        TurnManager.Instance.SubscribeCharacter(this);
        TurnManager.Instance.player = this;
    }

    /// <summary>
    /// Check for player input
    /// </summary>
    void Update()
    {
        if (Input.GetMouseButtonDown(primaryMouse))
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                if (highlightedHex != null && highlightedHex.walkable && highlightedHex.isExplored) movement.SetPath(movement.mentalMap.GetPath(highlightedHex));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            menu.ShowGameMenu(true);
        }

        actionText.text = "Actions: " + remainingActionPoints.ToString() + "/" + actionPoints.ToString();
    }

    /// <summary>
    /// Process end of turn
    /// </summary>
    public override void OnEndTurn()
    {
        if (movement.location.aspectList.Contains(Hex.Aspect.Town))
        {
            supplies += 4;
        }

        if (supplies-- <= 0)
        {
            OutOfSupplies();
        }
    }

    /// <summary>
    /// End the game when the player runs out of supplies.
    /// </summary>
    void OutOfSupplies()
    {
        TurnManager.Instance.DestroyCharacter(this);
        menu.GameOver();
    }
}
