namespace Dijkstra
{
    using System;
    using System.Collections.Generic;

    public static class DijkstraWithPriorityQueue
    {
        public static List<int> DijkstraAlgorithm(
            Dictionary<Node, Dictionary<Node, int>> graph, Node sourceNode, Node destinationNode)
        {
            int?[] prev = new int?[graph.Count];
            bool[] visited = new bool[graph.Count];
            PriorityQueue<Node> priorityQueue = new PriorityQueue<Node>();
            foreach (Node node in graph.Keys)
            {
                node.DistanceFromStart = Double.PositiveInfinity;
            }

            sourceNode.DistanceFromStart = 0;
            priorityQueue.Enqueue(sourceNode);
            visited[sourceNode.Id] = true;
            while (priorityQueue.Count > 0)
            {
                Node currNode = priorityQueue.ExtractMin();
                if (currNode.Equals(destinationNode))
                {
                    break;
                }

                foreach (Node child in graph[currNode].Keys)
                {
                    if (!visited[child.Id])
                    {
                        priorityQueue.Enqueue(child);
                        visited[child.Id] = true;
                    }

                    double distance = currNode.DistanceFromStart + graph[currNode][child];
                    if (distance < child.DistanceFromStart)
                    {
                        child.DistanceFromStart = distance;
                        prev[child.Id] = currNode.Id;
                        priorityQueue.DecreaseKey(child);
                    }
                }
            }

            if (Double.IsInfinity(destinationNode.DistanceFromStart))
            {
                return null;
            }

            List<int> path = new List<int>();
            int? current = destinationNode.Id;
            while (current != null)
            {
                path.Add(current.Value);
                current = prev[current.Value];
            }

            path.Reverse();
            return path;
        }
    }
}
