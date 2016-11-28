namespace AStarAlgorithm
{
    using System;
    using System.Collections.Generic;

    public class AStar
    {
        private readonly char[,] map;
        private PriorityQueue<Node> openNodesByFCost;
        private HashSet<Node> closedSet;
        private Node[,] graph;

        public AStar(char[,] map)
        {
            this.map = map;
            this.openNodesByFCost = new PriorityQueue<Node>();
            this.closedSet = new HashSet<Node>();
            this.graph = new Node[map.GetLength(0), map.GetLength(1)];
        }

        public List<int[]> FindShortestPath(int[] startCoords, int[] endCoords)
        {
            var startNode = this.GetNode(startCoords[0], startCoords[1]);
            startNode.GCost = 0;
            this.openNodesByFCost.Enqueue(startNode);
            while (this.openNodesByFCost.Count > 0)
            {
                var currNode = this.openNodesByFCost.ExtractMin();
                this.closedSet.Add(currNode);
                if (currNode.Row == endCoords[0] && currNode.Col == endCoords[1])
                {
                    return ReconstructPath(currNode);
                }

                List<Node> neighbours = this.GetNeighbours(currNode);
                foreach (Node neighbour in neighbours)
                {
                    if (this.closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    var newGCost = currNode.GCost + CalculateGCost(neighbour, currNode);
                    if (newGCost < neighbour.GCost)
                    {
                        neighbour.GCost = newGCost;
                        neighbour.Parent = currNode;
                        if (!this.openNodesByFCost.Contains(neighbour))
                        {
                            neighbour.HCost = CalculateHCost(neighbour, endCoords);
                            this.openNodesByFCost.Enqueue(neighbour);
                        }
                        else
                        {
                            this.openNodesByFCost.DecreaseKey(neighbour);
                        }
                    }
                }
            }

            return new List<int[]>(0);
        }

        private int CalculateHCost(Node node, int[] endCoords)
        {
            return this.GetDistance(node.Row, node.Col, endCoords[0], endCoords[1]);
        }

        private int CalculateGCost(Node node, Node prev)
        {
            return this.GetDistance(node.Row, node.Col, prev.Row, prev.Col);
        }

        private static List<int[]> ReconstructPath(Node node)
        {
            var cells = new List<int[]>();
            while (node != null)
            {
                cells.Add(new []{node.Row, node.Col});
                node = node.Parent;
            }

            return cells;
        }

        private int GetDistance(int r1, int c1, int r2, int c2)
        {
            var deltaC = Math.Abs(c1 - c2);
            var deltaR = Math.Abs(r1 - r2);
            if (deltaC > deltaR)
            {
                return 14 * deltaR + 10 * (deltaC - deltaR);
            }

            return 14 * deltaC + 10 * (deltaR - deltaC);
        }

        private List<Node> GetNeighbours(Node node)
        {
            var neighbours = new List<Node>();
            var maxRow = this.graph.GetLength(0);
            var maxCol = this.graph.GetLength(1);
            int[] rowMod = {-1, -1, -1, 0, 0, 1, 1, 1};
            int[] colMod = {-1, 0, 1, -1, 1, -1, 0, 1};
            for (int i = 0; i < 8; i++)
            {
                int row = node.Row + rowMod[i];
                int col = node.Col + colMod[i];
                if (row < 0 || row >= maxRow ||
                    col < 0 || col >= maxCol ||
                    this.map[row, col] == 'W')
                {
                    continue;
                }

                var neighbour = this.GetNode(row, col);
                neighbours.Add(neighbour);
            }

            return neighbours;
        }

        private Node GetNode(int row, int col)
        {
            if (this.graph[row, col] == null)
            {
                this.graph[row, col] = new Node(row, col);
            }

            return this.graph[row, col];
        }
    }
}
