using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField] public Vector3Int position;
    static private float verticalOffset = 1.5f * (1 / Mathf.Sqrt(3));
    public int x { get { return position.x;  } }
    public int y { get { return position.y;  } }
    public int z { get { return position.z;  } }

    void Start()
    {
        transform.position = MapPositionToWorldPosition(position);
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    static public Vector2Int WorldPositionToMapPosition(Vector3 worldPosition)
    {
        int y = (int)(worldPosition.z / verticalOffset);
        int x = (int)(worldPosition.x - .5f * (y % 2));

        return new Vector2Int(x, y);
    }

    static public Vector3 MapPositionToWorldPosition(Vector2Int mapPosition)
    {
        float y = mapPosition.y * verticalOffset;
        float x = mapPosition.x + .5f * Mathf.Abs(mapPosition.y % 2);

        return new Vector3(x, 0, y);
    }
}
