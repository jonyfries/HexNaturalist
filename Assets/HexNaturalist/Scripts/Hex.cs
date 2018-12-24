using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField] public Vector3Int position;
    static private float verticalOffset = 1.5f * (1 / Mathf.Sqrt(3));
    static private float horizontalOffset = 0.5f;
    public int x { get { return position.x;  } }
    public int y { get { return position.y;  } }
    public int z { get { return position.z;  } }

    void Start()
    {
        position.z = Mathf.Abs(position.y % 2);
        transform.position = MapPositionToWorldPosition(position);
    }

    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    static public Vector3Int WorldPositionToMapPosition(Vector3 worldPosition)
    {
        int y = (int)(worldPosition.z / verticalOffset);
        int z = Mathf.Abs(y % 2);
        int x = (int)(worldPosition.x - .5f * (y % 2));

        return new Vector3Int(x, y, z);
    }

    static public Vector3 MapPositionToWorldPosition(Vector3Int mapPosition)
    {
        float y = mapPosition.y * verticalOffset;
        float x = mapPosition.x + horizontalOffset * mapPosition.z;

        return new Vector3(x, 0, y);
    }
}
