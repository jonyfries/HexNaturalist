using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Map : Singleton<Map>
{
    static public List<List<Vector3Int>> neighborDirections = new List<List<Vector3Int>>() {
                        new List<Vector3Int>() { new Vector3Int(-1, 1, -1), new Vector3Int(0, 1, -1),
                                                 new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0),
                                                 new Vector3Int(-1, -1, -1), new Vector3Int(0, -1, -1)},
                        new List<Vector3Int>() { new Vector3Int(1, 1, -1), new Vector3Int(0, 1, -1),
                                                 new Vector3Int(-1, 0, 0), new Vector3Int(1, 0, 0),
                                                 new Vector3Int(1, -1, -1), new Vector3Int(0, -1, -1)}};

    public List<Hex> hexes = new List<Hex>();

    public int generatedMapSize;

    void Start()
    {
        ProcessMapSeed();
        GenerateMap(null);
    }

    /// <summary>
    /// Creates a new hex.
    /// </summary>
    /// <param name="prefab">The prefab to use to create the hex.</param>
    /// <param name="location">The map location of the hex.</param>
    /// <returns>The created hex.</returns>
    Hex CreateHex(GameObject prefab, Vector3Int location)
    {
        GameObject hexObject = Instantiate(prefab) as GameObject;
        Hex newHex = hexObject.GetComponent<Hex>();
        newHex.SetPosition(location);
        hexObject.transform.parent = transform;

        return newHex;
    }

    /// <summary>
    /// Starting with the existing map generate a random island.
    /// <param name="seed">The seed to use to generate the island. Set to null if no specific seed needed.</param>
    /// </summary>
    void GenerateMap(int? seed)
    {
        List<Hex> mapFringe = new List<Hex>();
        List<Hex> waterFringe = new List<Hex>();

        Dictionary<Vector3Int, Hex> mapDict = new Dictionary<Vector3Int, Hex>();
        System.Random random;
        if (seed != null) random = new System.Random((int)seed);
        else random = new System.Random();

        // Generate map fringe
        foreach (Hex mapHex in hexes)
        {
            if (mapHex.neighbors.Count != 6)
            {
                if (!mapHex.isWater)
                {
                    mapFringe.Add(mapHex);
                }
                else
                {
                    waterFringe.Add(mapHex);
                }
            }
            mapDict[mapHex.position] = mapHex;
        }

        // Generate the land mass
        List<GameObject> prefabList = Resources.LoadAll<GameObject>("Prefabs/Hexes/Land").ToList();

        for (int i = 0; i < generatedMapSize; ++i)
        {
            int hexIndex = random.Next(mapFringe.Count);
            Hex hex = mapFringe[hexIndex];
            Hex newHex = CreateHex(prefabList[random.Next(prefabList.Count)], hex.OpenNeighbors()[random.Next(6 - hex.neighbors.Count)]);
            mapDict[newHex.position] = newHex;

            foreach (Vector3Int direction in neighborDirections[newHex.z])
            {
                Hex neighborHex;
                if (mapDict.TryGetValue(newHex.Plus(direction), out neighborHex))
                {
                    newHex.AddNeighborBidirectional(neighborHex);
                    if (neighborHex.neighbors.Count == 6)
                    {
                        mapFringe.Remove(neighborHex);
                    }
                }
            }

            if (newHex.neighbors.Count < 6)
            {
                mapFringe.Add(newHex);
            }
        }

        // Generate shallow water around land mass
        prefabList = Resources.LoadAll<GameObject>("Prefabs/Hexes/Water/Shallow").ToList();

        while (mapFringe.Count != 0)
        {
            Hex fringeHex = mapFringe[mapFringe.Count - 1];
            foreach (Vector3Int location in fringeHex.OpenNeighbors())
            {
                Hex newHex = CreateHex(prefabList[random.Next(prefabList.Count)], location);
                mapDict[newHex.position] = newHex;

                foreach (Vector3Int direction in neighborDirections[newHex.z])
                {
                    Hex neighborHex;
                    if (mapDict.TryGetValue(newHex.Plus(direction), out neighborHex))
                    {
                        newHex.AddNeighborBidirectional(neighborHex);
                        if (neighborHex.neighbors.Count == 6)
                        {
                            mapFringe.Remove(neighborHex);
                        }
                    }
                }

                if (newHex.neighbors.Count < 6)
                {
                    waterFringe.Add(newHex);
                }
            }
        }

        // Generate deep water around land mass
        prefabList = Resources.LoadAll<GameObject>("Prefabs/Hexes/Water/Deep").ToList();

        while (waterFringe.Count != 0)
        {
            Hex fringeHex = waterFringe[waterFringe.Count - 1];
            foreach (Vector3Int location in fringeHex.OpenNeighbors())
            {
                Hex newHex = CreateHex(prefabList[random.Next(prefabList.Count)], location);
                mapDict[newHex.position] = newHex;

                foreach (Vector3Int direction in neighborDirections[newHex.z])
                {
                    Hex neighborHex;
                    if (mapDict.TryGetValue(newHex.Plus(direction), out neighborHex))
                    {
                        newHex.AddNeighborBidirectional(neighborHex);
                        if (neighborHex.neighbors.Count == 6)
                        {
                            waterFringe.Remove(neighborHex);
                        }
                    }
                }
            }

            waterFringe.Remove(fringeHex);
        }
    }

    /// <summary>
    /// Get a list of all of a hex's neighbors.
    /// </summary>
    /// <param name="hex">Hex to get neighbors of</param>
    /// <returns>List of neighbors</returns>
    List<Hex> FindNeighbors(Hex hex)
    {
        List<Hex> neighbors = new List<Hex>();
        List<Vector3Int> neighborPositions = new List<Vector3Int>();
        foreach (Vector3Int direction in neighborDirections[hex.position.z])
        {
            neighborPositions.Add(direction + hex.position);
        }

        foreach (Hex checkHex in hexes)
        {
            if (neighborPositions.Contains(checkHex.position))
            {
                neighbors.Add(checkHex);
            }
        }

        return neighbors;
    }

    /// <summary>
    /// Process the existing map created in the editor
    /// </summary>
    void ProcessMapSeed()
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
                if (neighbor.x < 0 || neighbor.y < 0 || neighbor.x > maxX || neighbor.y > maxY || map[neighbor.x, neighbor.y] == null)
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

        foreach (Vector3Int neighbor in neighborDirections[hex.z])
        {
            neighborList.Add(hex.Plus(neighbor));
        }

        return neighborList;
    }
}
