using System.Collections.Generic;

public class Edge
{
    public Node tail;
    public Node head;
    public float totalCost { get; private set; }
    float marginalCost;
    float heuristicCost = -1;

    public Edge(Node tail, Node head, float cost, bool inverted)
    {
        this.tail = tail;
        this.head = head;
        this.marginalCost = cost;
        tail.AddEdge(this);

        if (inverted)
        {
            new Edge(head, tail, cost, false);
        }
    }


    /// <summary>
    /// Returns the cost to the head of this edge plus the heuristic of the head.
    /// </summary>
    /// <param name="goal">The node that is being searched for.</param>
    /// <returns></returns>
    public float GetHeuristicCost(Node goal)
    {
        if (heuristicCost == -1)
        {
            heuristicCost = head.CalculateHeuristic(goal);
        }

        return totalCost + heuristicCost;
    }

    /// <summary>
    /// Resets the Edge so the search can be performed again.
    /// </summary>
    public void Reset()
    {
        totalCost = new float();
        heuristicCost = -1;
    }

    /// <summary>
    /// Sets the total cost to get to the head of this Edge.
    /// </summary>
    /// <param name="costToEdge">The cost to get to the tail of this Edge.</param>
    public void SetTotalCost(float costToEdge)
    {
        totalCost = marginalCost + costToEdge;
    }
}