﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MentalMap
{
    Dictionary<Vector3Int, HexNode> map = new Dictionary<Vector3Int, HexNode>();
    public CharacterMovement character;

    public MentalMap(CharacterMovement characterMovement)
    {
        this.character = characterMovement;
        AddHex(character.location, 0);
    }

    /// <summary>
    /// Adds a new hex to the personal map the character has. 
    /// </summary>
    /// <param name="hex">The new hex the character can think about pathing to.</param>
    /// <returns></returns>
    private HexNode AddHex(Hex hex, int depth)
    {
        HexNode hexNode;

        if (!map.ContainsKey(hex.position))
        {
            hexNode = new HexNode(hex);
            map[hex.position] = hexNode;
        } else
        {
            hexNode = map[hex.position];
        }

        if (hex.isExplored)
        {
            foreach (Hex neighbor in hex.neighbors)
            {
                if (!map.ContainsKey(neighbor.position))
                {
                    AddHex(neighbor, depth);
                }

                if (!hexNode.HasConnection(map[neighbor.position]))
                {
                    hexNode.SetConnection(map[neighbor.position]);
                }
            }
        }

        return map[hex.position];
    }

    /// <summary>
    /// Returns the path from the character's current hex to the goal hex.
    /// </summary>
    /// <param name="goal">The goal of the path.</param>
    /// <returns></returns>
    public List<Hex> GetPath(Hex goal)
    {
        // Find the path to the goal.
        HexNode goalNode = map[goal.position];
        List<Hex> path = new List<Hex>();
        foreach (HexNode hexNode in Pathfinding.AStar(map[character.location.position], goalNode).Cast<HexNode>())
        {
            path.Add(hexNode.hex);
        }

        // Reset the search graph so it can be cleanly searched again.
        foreach (HexNode hexNode in map.Values)
        {
            hexNode.Reset();
        }

        // Return the path.
        return path;
    }
}
