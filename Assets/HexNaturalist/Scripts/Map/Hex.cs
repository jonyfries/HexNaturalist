﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField] public Vector3Int position;
    [SerializeField] public int entryCost;
    [SerializeField] public bool walkable;
    public List<Hex> neighbors { get; private set; }
    public bool isExplored = false;
    public bool isWater = false;

    static private float verticalOffset = 1.5f * (1 / Mathf.Sqrt(3));
    static private float horizontalOffset = 0.5f;

    public int x { get { return position.x; } }
    public int y { get { return position.y; } }
    public int z { get { return position.z; } }


    /// <summary>
    /// Setup hex on creation.
    /// </summary>
    void Awake()
    {
        position.z = position.y & 1;
        transform.position = MapPositionToWorldPosition(position);
        neighbors = new List<Hex>();
    }

    /// <summary>
    /// Add a new neighbor to the hex.
    /// </summary>
    /// <param name="neighbor">Hex to add as a neighbor.</param>
    public void AddNeighbor(Hex neighbor)
    {
        neighbors.Add(neighbor);
    }

    /// <summary>
    /// Add a new neighbor to the hex and add this hex as a neighbor to it.
    /// </summary>
    /// <param name="neighbor">The neighboring hex</param>
    public void AddNeighborBidirectional(Hex neighbor)
    {
        AddNeighbor(neighbor);
        neighbor.AddNeighbor(this);
    }

    /// <summary>
    /// Return the position when a vector is added to the hexes position.
    /// </summary>
    /// <param name="vector">Vector to add.</param>
    /// <returns>The resulting map position as Vector3Int</returns>
    public Vector3Int Plus(Vector3Int vector)
    {
        int newY = y + vector.y;
        return new Vector3Int(x + vector.x, newY, newY & 1);
    }

    /// <summary>
    /// Returns the world position of this hex.
    /// </summary>
    /// <returns>Hex position</returns>
    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    /// <summary>
    /// Provides a list of neighboring locations that do not have a hex in them.
    /// </summary>
    /// <returns>List of map locations without hexes.</returns>
    public List<Vector3Int> OpenNeighbors()
    {
        int count = neighbors.Count;
        List<Vector3Int> openList = new List<Vector3Int>();

        foreach (Vector3Int hexPosition in Map.GetNeighborPositionList(this))
        {
            if (count == 6) break;
            bool exists = false;

            foreach (Hex hex in neighbors)
            {
                if (hex.position == hexPosition)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                count += 1;
                openList.Add(hexPosition);
            }
        }

        return openList;
    }

    /// <summary>
    /// Give a new position in the map.
    /// </summary>
    /// <param name="newPosition">The new hex position</param>
    public void SetPosition(Vector3Int newPosition)
    {
        position = newPosition;
        position.z = position.y & 1;
        transform.position = MapPositionToWorldPosition(position);
    }

    /// <summary>
    /// Get the world position for the given map position.
    /// </summary>
    /// <param name="mapPosition">The given map position</param>
    /// <returns></returns>
    static public Vector3 MapPositionToWorldPosition(Vector3Int mapPosition)
    {
        float y = mapPosition.y * verticalOffset;
        float x = mapPosition.x + horizontalOffset * mapPosition.z;

        return new Vector3(x, 0, y);
    }

    /// <summary>
    /// Get the map position for a given world position.
    /// </summary>
    /// <param name="worldPosition">The world position to get the map position for.</param>
    /// <returns></returns>
    static public Vector3Int WorldPositionToMapPosition(Vector3 worldPosition)
    {
        int y = (int)(worldPosition.z / verticalOffset);
        int z = y & 1;
        int x = (int)(worldPosition.x - horizontalOffset * z);

        return new Vector3Int(x, y, z);
    }
}