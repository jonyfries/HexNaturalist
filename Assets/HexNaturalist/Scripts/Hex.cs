using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Vector2Int position;

    void Start()
    {
        transform.position = MapPositionToWorldPosition(position);
    }

    public List<Vector2Int> GetNeighborPositions()
    {
        return new List<Vector2Int>() { new Vector2Int(position.x - 1, position.y), new Vector2Int(position.x + 1, position.y) };
    }

    static public Vector2Int WorldPositionToMapPosition(Vector3 worldPosition)
    {
        int y = (int)(worldPosition.z / .85f);
        int x = (int)(worldPosition.x - .5f * (y % 2));

        return new Vector2Int(x, y);
    }

    static public Vector3 MapPositionToWorldPosition(Vector2Int mapPosition)
    {
        float y = mapPosition.y * .85f;
        float x = mapPosition.x + .5f * (mapPosition.y % 2);

        return new Vector3(x, 0, y);
    }
}
