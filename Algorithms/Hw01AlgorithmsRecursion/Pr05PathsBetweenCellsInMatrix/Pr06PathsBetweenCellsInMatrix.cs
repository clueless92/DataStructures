using System;
using System.Collections.Generic;

namespace Pr05PathsBetweenCellsInMatrix
{
    class Pr06PathsBetweenCellsInMatrix
    {
//        static readonly char[,] lab = 
//        {
//            {'s', ' ', ' ', ' ', ' ', ' '},
//            {' ', '*', '*', ' ', '*', ' '},
//            {' ', '*', '*', ' ', '*', ' '},
//            {' ', '*', 'e', ' ', ' ', ' '},
//            {' ', ' ', ' ', '*', ' ', ' '},
//        };

        static readonly char[,] lab = 
        {
            {'s', ' ', ' ', ' '},
            {' ', '*', '*', ' '},
            {' ', '*', '*', ' '},
            {' ', '*', 'e', ' '},
            {' ', ' ', ' ', ' '},
        };

        static List<char> path = new List<char>();
        static int totalPathsCount = 0;

        static void Main()
        {
            FindExit(0, 0, 'S');
            Console.WriteLine("Total paths found: {0}", totalPathsCount);
        }

        static bool InRange(int row, int col)
        {
            bool rowInRange = row >= 0 && row < lab.GetLength(0);
            bool colInRange = col >= 0 && col < lab.GetLength(1);
            return rowInRange && colInRange;
        }

        static void FindExit(int row, int col, char way)
        {
            if (!InRange(row, col))
            {
                return;
            }

            if (way != 'S')
            {
                path.Add(way);
            }

            if (lab[row, col] == 'e')
            {
                PrintPath(path);
            }

            if (lab[row, col] == ' ' || lab[row, col] == 's')
            {
                lab[row, col] = 'x';

                FindExit(row, col - 1, 'L');
                FindExit(row - 1, col, 'U');
                FindExit(row, col + 1, 'R');
                FindExit(row + 1, col, 'D');

                lab[row, col] = ' ';
            }

            if (path.Count > 0)
            {
                path.RemoveAt(path.Count - 1);
            }
        }

        static void PrintPath(List<char> path)
        {
            foreach (var p in path)
            {
                Console.Write(p);
            }
            Console.WriteLine();
            totalPathsCount++;
        }
    }
}