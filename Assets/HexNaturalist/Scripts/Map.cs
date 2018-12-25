using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : Singleton<Map>
{
    static List<Vector3Int> evenNeighbors = new List<Vector3Int>() { new Vector3Int(-1, 1, -1), new Vector3Int(0, 1, -1),
                                                                     new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0),
                                                                     new Vector3Int(-1, -1, -1), new Vector3Int(0, -1, -1)};

    static List<Vector3Int> oddNeighbors = new List<Vector3Int>() { new Vector3Int(1, 1, -1), new Vector3Int(0, 1, -1),
                                                                     new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0),
                                                                     new Vector3Int(1, -1, -1), new Vector3Int(0, -1, -1)};

    public List<Hex> hexes = new List<Hex>();

    /// <summary>
    /// Set up the map
    /// </summary>
    void Start()
    {
        Hex[,] map;
        int maxX = 0;
        int maxY = 0;

        Hex mapHex;

        //Get the size of the hexes and create a list of hexes to iterate through.
        foreach (Transform child in transform)
        {
            mapHex = child.GetComponent<Hex>();
            if (mapHex.x > maxX) maxX = mapHex.x;
            if (mapHex.y > maxY) maxY = mapHex.y;
            hexes.Add(mapHex);
        }

        //Populate the hex array so it can be processed.
        map = new Hex[maxX + 1, maxY + 1];
        foreach (Hex hex in hexes)
        {
            map[hex.x, hex.y] = hex;
        }

        //Connect the neighboring hexes together.
        foreach (Hex hex in hexes)
        {
            foreach (Vector3Int neighbor in GetNeighborPositionList(hex))
            {
                if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x > maxX || neighbor.y > maxY)
                {
                    continue;
                }

                hex.AddNeighbor(map[neighbor.x, neighbor.y]);
            }
        }
    }

    /// <summary>
    /// Get a list of all of the map positions of a hexes neighbors.
    /// </summary>
    /// <param name="hex">Hex you want to get the neighbors of.</param>
    /// <returns></returns>
    static public List<Vector3Int> GetNeighborPositionList(Hex hex)
    {
        List<Vector3Int> neighborList = new List<Vector3Int>();

        List<Vector3Int> offsetList;

        if (hex.z == 1) offsetList = oddNeighbors;
        else offsetList = evenNeighbors;

        foreach (Vector3Int neighbor in offsetList)
        {
            neighborList.Add(hex.position + neighbor);
        }

        return neighborList;
    }
}
