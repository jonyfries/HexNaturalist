using System.Collections.Generic;

public class Node
{
    private List<Edge> connections = new List<Edge>();
    public List<Node> path;
    public bool isExpanded = false;

    virtual public float CalculateHeuristic(Node goal)
    {
        return 0f;
    }

    /// <summary>
    /// Adds a new Edge to this node.
    /// </summary>
    /// <param name="newEdge">The edge to add.</param>
    public void AddEdge(Edge newEdge)
    {
        connections.Add(newEdge);
    }

    /// <summary>
    /// Removes the edge between this and another node.
    /// </summary>
    /// <param name="connectedNode">The Node that should no longer be connected to this node.</param>
    /// <param name="destroyInverse">Set to true if the the Edge from connectedNode to this node should be destroyed as well.</param>
    public void DestroyConnection(Node connectedNode, bool destroyInverse)
    {
        foreach (Edge edge in connections)
        {
            if (edge.head == connectedNode)
            {
                connections.Remove(edge);
            }
        }

        if (destroyInverse)
        {
            connectedNode.DestroyConnection(this, false);
        }
    }

    /// <summary>
    /// Return a list of all of this nodes edges, set the minimum cost to this node, and mark it as expanded. Used when searching the graph.
    /// </summary>
    /// <param name="costToNode">The minimum cost to get to this node.</param>
    /// <returns></returns>
    public List<Edge> Expand(float costToNode)
    {
        isExpanded = true;
        foreach (Edge connection in connections)
        {
            connection.SetTotalCost(costToNode);
        }
        return new List<Edge>(connections);
    }

    /// <summary>
    /// Determine if this node is directly connected to another node.
    /// </summary>
    /// <param name="node">The other node</param>
    /// <returns>Returns true if the other node is the head of any of this nodes edges.</returns>
    public bool HasConnection(Node node)
    {
        foreach (Edge edge in connections)
        {
            if (edge.head == node)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Get the edges that start at this node without expanding it for a search.
    /// </summary>
    /// <returns>A list of edges.</returns>
    public List<Edge> PeakEdges()
    {
        return new List<Edge>(connections);
    }

    /// <summary>
    /// Resets this edge so that it can be used cleanly in a new search.
    /// </summary>
    public void Reset()
    {
        isExpanded = false;
        path = new List<Node>();
        foreach (Edge edge in connections)
        {
            edge.Reset();
        }
    }

    /// <summary>
    /// Sets this node as the beginning of the search path.
    /// </summary>
    public void StartPath()
    {
        path = new List<Node> { this };
    }

    /// <summary>
    /// Adds this node to an existing search path.
    /// </summary>
    /// <param name="parent">The node that is before this node in the search path.</param>
    public void SetPath(Node parent)
    {
        path = new List<Node>(parent.path);
        path.Add(this);
    }
}