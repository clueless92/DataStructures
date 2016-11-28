namespace Dijkstra
{
    using System.Collections.Generic;

    public static class DijkstraWithoutQueue
    {
        const int UNKNOWN = int.MaxValue >> 1;

        public static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode)
        {
            int n = graph.GetLength(0);

            int[] distance = new int[n];
            for (int i = 0; i < n; i++)
            {
                distance[i] = UNKNOWN;
            }

            distance[sourceNode] = 0;
            bool[] visited = new bool[n];
            int?[] previus = new int?[n];

            while (true)
            {
                // find the nearest unvisited node from the source (in place of priority queue -> dequeue)
                int minDistance = UNKNOWN;
                int minNode = 0;
                for (int node = 0; node < n; node++)
                {
                    if (!visited[node] && distance[node] < minDistance)
                    {
                        minDistance = distance[node];
                        minNode = node;
                    }
                }

                if (minDistance == UNKNOWN)
                {
                    // no minDistance node found -> algorithm finished
                    break;
                }

                visited[minNode] = true;
                for (int i = 0; i < n; i++) // improve the distance [0 ... n-1] through minNode
                {
                    if (graph[minNode, i] > 0) // no 'i' is connected to minNode
                    {
                        int newDistance = distance[minNode] + graph[minNode, i];
                        if (newDistance < distance[i])
                        {
                            distance[i] = newDistance;
                            previus[i] = minNode;
                        }
                    }
                }
            }

            if (distance[destinationNode] == UNKNOWN)
            {
                // no path found from source to destination
                return null;
            }

            // recunstruct path from source to destination
            List<int> path = new List<int>();
            int? currNode = destinationNode;
            while (currNode != null)
            {
                path.Add(currNode.Value);
                currNode = previus[currNode.Value];
            }

            path.Reverse();
            return path;
        }
    }
}
