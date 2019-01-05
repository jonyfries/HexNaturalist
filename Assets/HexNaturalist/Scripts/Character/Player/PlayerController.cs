using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    const int primaryMouse = 0;
    const int middleMouse = 1;
    const int secondaryMouse = 2;
    static public Hex highlightedHex { get; set; }

    public MenuHandler menu;
    public CharacterMovement movement;
    public UnityEngine.UI.Text actionText;

    void Update()
    {
        if (Input.GetMouseButtonDown(primaryMouse))
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                if (highlightedHex != null && highlightedHex.walkable && highlightedHex.isExplored) movement.SetPath(movement.mentalMap.GetPath(highlightedHex));
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            menu.GameMenu(true);
        }

        actionText.text = "Actions: " + remainingActionPoints.ToString() + "/" + actionPoints.ToString();
    }
}
