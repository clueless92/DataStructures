namespace Pr01PlayWithTrees
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        private static readonly Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        private static Tree<int> GetTreeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        static void Main(string[] args)
        {
            int nodesCount = int.Parse(Console.ReadLine());
            for (int i = 1; i < nodesCount; i++)
            {
                string[] edge = Console.ReadLine().Split();
                int parrentValue = int.Parse(edge[0]);
                Tree<int> parrentNode = GetTreeByValue(parrentValue);
                int childValue = int.Parse(edge[1]);
                Tree<int> childNode = GetTreeByValue(childValue);
                parrentNode.Children.Add(childNode);
                childNode.Parrent = parrentNode;
            }

            int pathSum = int.Parse(Console.ReadLine());
            int subTreeSum = int.Parse(Console.ReadLine());

            StringBuilder outputBuilder = new StringBuilder();
            Tree<int> rootNode = FindRootNode();
            IEnumerable<Tree<int>> leafNodes = FindLeafNodes();
            IEnumerable<Tree<int>> middleNodes = FindMiddleNodes();
            outputBuilder.AppendFormat("Root node: {0}{1}", rootNode.Value, Environment.NewLine);
            outputBuilder.Append("Leaf nodes: ");
            foreach (var leafNode in leafNodes)
            {
                outputBuilder.AppendFormat("{0}, ", leafNode.Value);
            }
            outputBuilder.Remove(outputBuilder.Length - 2, 2);
            outputBuilder.AppendLine();
            outputBuilder.Append("Middle nodes: ");
            foreach (var midNode in middleNodes)
            {
                outputBuilder.AppendFormat("{0}, ", midNode.Value);
            }
            outputBuilder.Remove(outputBuilder.Length - 2, 2);
            outputBuilder.AppendLine();
            Console.Write(outputBuilder);
        }

        private static IEnumerable<Tree<int>> FindLeafNodes()
        {
            var leafNodes = nodeByValue.Values
                .Where(node => node.Children.Count == 0)
                .OrderBy(node => node.Value).ToList();

            return leafNodes;
        }

        private static Tree<int> FindRootNode()
        {
            Tree<int> rootNode = nodeByValue.Values.FirstOrDefault(node => node.Parrent == null);

            return rootNode;
        }

        private static IEnumerable<Tree<int>> FindMiddleNodes()
        {
            var middleNodes = nodeByValue.Values
                .Where(node => node.Children.Count > 0 && node.Parrent != null)
                .OrderBy(node => node.Value).ToList();

            return middleNodes;
        }
    }
}
