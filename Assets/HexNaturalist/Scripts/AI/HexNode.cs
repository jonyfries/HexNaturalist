using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexNode : Node
{
    public Hex hex;

    public Vector3Int cubicPosition;

    public HexNode(Hex hex)
    {
        this.hex = hex;
        SetCubicPosition();
    }
    
    /// <summary>
    /// Creates an Edge to this HexNode from each neighbor.  
    /// </summary>
    /// <param name="neighbors">The list of new hexes this HexNode is connected to.</param>
    public void SetConnection(HexNode neighbor)
    {
        new Edge(this, neighbor, hex.entryCost, false);
    }

    /// <summary>
    /// Converts offset map position to cubic position.
    /// </summary>
    void SetCubicPosition()
    {
        int x = hex.x - (hex.y - hex.z) / 2;
        int z = hex.y;
        int y = -x - z;
        cubicPosition = new Vector3Int(x, y, z);
    }
    
    /// <summary>
    /// Calculates the Manhattan distance between this hex and the goal hex.
    /// </summary>
    /// <param name="goal">The goal Node.</param>
    /// <returns></returns>
    override public float CalculateHeuristic(Node goal)
    {
        HexNode goalHex = (HexNode)goal;
        return (Mathf.Abs(cubicPosition.x - goalHex.cubicPosition.x) 
                + Mathf.Abs(cubicPosition.y - goalHex.cubicPosition.y) 
                + Mathf.Abs(cubicPosition.z - goalHex.cubicPosition.z)) / 2;
    }
}
