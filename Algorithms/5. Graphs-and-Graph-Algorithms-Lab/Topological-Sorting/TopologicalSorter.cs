using System;
using System.Collections.Generic;
using System.Linq;

public class TopologicalSorter
{
    private Dictionary<string, List<string>> graph;
    private HashSet<string> visitedNodes;
    private LinkedList<string> sortedNodes;
    private HashSet<string> cycleNodes;

    public TopologicalSorter(Dictionary<string, List<string>> graph)
    {
        this.graph = graph;
    }

    public ICollection<string> TopSort()
    {
//        return this.TopSortSourceRemoval();

        this.TopSortDFS();
        return this.sortedNodes;
    }

    private void TopSortDFS()
    {
        this.visitedNodes = new HashSet<string>();
        this.sortedNodes = new LinkedList<string>();
        this.cycleNodes = new HashSet<string>();

        foreach (string node in this.graph.Keys)
        {
            this.DFS(node);
        }
    }

    private void DFS(string node)
    {
        if (this.cycleNodes.Contains(node))
        {
            throw new InvalidOperationException("A cycle detected in the graph");
        }

        if (this.visitedNodes.Contains(node))
        {
            return;
        }

        this.visitedNodes.Add(node);
        this.cycleNodes.Add(node);
        if (this.graph.ContainsKey(node))
        {
            foreach (string child in this.graph[node])
            {
                this.DFS(child);
            }
        }

        this.cycleNodes.Remove(node);
        this.sortedNodes.AddFirst(node);
    }

    private ICollection<string> TopSortSourceRemoval()
    {
        var predecessorsCount = new Dictionary<string, int>();
        foreach (var node in this.graph)
        {
            if (!predecessorsCount.ContainsKey(node.Key))
            {
                predecessorsCount[node.Key] = 0;
            }

            foreach (var child in node.Value)
            {
                if (!predecessorsCount.ContainsKey(child))
                {
                    predecessorsCount[child] = 0;
                }

                predecessorsCount[child]++;
            }
        }

        var removedNodes = new List<string>();
        while (true)
        {
            string nodeToRemove = null;
            foreach (string key in this.graph.Keys)
            {
                if (predecessorsCount[key] == 0)
                {
                    nodeToRemove = key;
                    break;
                }
            }

            if (nodeToRemove == null)
            {
                break;
            }

            foreach (string child in this.graph[nodeToRemove])
            {
                predecessorsCount[child]--;
            }
            this.graph.Remove(nodeToRemove);
            removedNodes.Add(nodeToRemove);
        }

        if (this.graph.Count > 0)
        {
            throw new InvalidOperationException("A cycle detected in the graph");
        }

        return removedNodes;
    }
}
