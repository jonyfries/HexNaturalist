﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    const int primaryMouse = 0;
    const int middleMouse = 1;
    const int secondaryMouse = 2;
    static public Hex highlightedHex { get; set; }

    void Update()
    {
        if (Input.GetMouseButtonDown(primaryMouse))
        {
            if (highlightedHex != null && highlightedHex.walkable) SetPath(mentalMap.GetPath(highlightedHex));
        }

        if (Input.GetMouseButtonDown(middleMouse))
            Debug.Log("Pressed secondary button.");

        if (Input.GetMouseButtonDown(secondaryMouse))
            Debug.Log("Pressed middle click.");
    }
}