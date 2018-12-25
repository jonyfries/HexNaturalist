using System.Collections.Generic;

public class Pathfinding
{
    /// <summary>
    /// Returns the best path between start and goal using a Dijkstra search.
    /// </summary>
    /// <param name="start">The starting state</param>
    /// <param name="goal">The ending state</param>
    /// <returns>Returns the path including the starting and ending state.</returns>
    static public List<Node> Dijkstra(Node start, Node goal)
    {
        Edge currentEdge;
        int fringe_end;

        Node currentNode = start;
        currentNode.StartPath();
        List<Edge> fringe = start.Expand(0f);
        List<Node> expanded = new List<Node> { start };

        while (fringe.Count != 0)
        {
            fringe.Sort((x, y) => y.totalCost.CompareTo(x.totalCost));

            fringe_end = fringe.Count - 1;
            currentEdge = fringe[fringe_end];
            fringe.RemoveAt(fringe_end);
            currentNode = currentEdge.head;
            currentNode.SetPath(currentEdge.tail);
            if (currentNode == goal) return currentNode.path;

            expanded.Add(currentNode);
            foreach (Edge edge in currentNode.Expand(currentEdge.totalCost))
            {
                if (!expanded.Contains(edge.head))
                {
                    fringe.Add(edge);
                }
            }
        }

        return new List<Node>();
    }

    /// <summary>
    /// Returns the best path between start and goal using an A* search.
    /// </summary>
    /// <param name="start">The starting state</param>
    /// <param name="goal">The ending state</param>
    /// <returns>Returns the path including the starting and ending state.</returns>
    static public List<Node> AStar(Node start, Node goal)
    {
        Edge currentEdge;
        int fringe_end;

        Node currentNode = start;
        currentNode.StartPath();
        List<Edge> fringe = start.Expand(0f);
        List<Node> expanded = new List<Node> { start };

        while (fringe.Count != 0)
        {
            fringe.Sort((x, y) => y.GetHeuristicCost(goal).CompareTo(x.GetHeuristicCost(goal)));

            fringe_end = fringe.Count - 1;
            currentEdge = fringe[fringe_end];
            fringe.RemoveAt(fringe_end);
            currentNode = currentEdge.head;
            currentNode.SetPath(currentEdge.tail);
            if (currentNode == goal) return currentNode.path;

            expanded.Add(currentNode);
            foreach (Edge edge in currentNode.Expand(currentEdge.totalCost))
            {
                if (!expanded.Contains(edge.head))
                {
                    fringe.Add(edge);
                }
            }
        }

        return new List<Node>();
    }

    /// <summary>
    /// Returns the set of nodes that can be reached from start using no more than maxCost.
    /// </summary>
    /// <param name="start">The starting state</param>
    /// <param name="maxCost">The maximum cost</param>
    /// <returns>Set of nodes that can be reached from start given maxCost</returns>
    static public List<Node> GetArea(Node start, float maxCost)
    {
        int fringe_end;
        Edge currentEdge;
        List<Edge> fringe = new List<Edge>();
        Node currentNode = start;
        List<Node> expanded = new List<Node>() { start };

        foreach (Edge edge in currentNode.Expand(0))
        {
            if (edge.totalCost <= maxCost && !expanded.Contains(edge.head))
            {
                fringe.Add(edge);
            }
        }

        while (fringe.Count != 0)
        {
            fringe_end = fringe.Count - 1;
            currentEdge = fringe[fringe_end];
            fringe.RemoveAt(fringe_end);

            currentNode = currentEdge.head;

            if (expanded.Contains(currentNode))
            {
                continue;
            }

            expanded.Add(currentNode);

            foreach (Edge edge in currentNode.Expand(currentEdge.totalCost))
            {
                if (edge.totalCost <= maxCost && !expanded.Contains(edge.head))
                {
                    fringe.Add(edge);
                }
            }
        }

        return expanded;
    }
}