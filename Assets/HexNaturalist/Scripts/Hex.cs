using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField] public Vector3Int position;
    [SerializeField] public int entryCost;
    public List<Hex> neighbors { get; private set; }
    public bool isExplored = false;

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
    /// Add a new neighbor to the hex.
    /// </summary>
    /// <param name="neighbor">Hex to add as a neighbor.</param>
    public void AddNeighbor(Hex neighbor)
    {
        if (neighbors == null) neighbors = new List<Hex>();
        neighbors.Add(neighbor);
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
