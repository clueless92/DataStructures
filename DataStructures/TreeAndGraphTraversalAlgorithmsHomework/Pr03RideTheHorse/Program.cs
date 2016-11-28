namespace Pr03RideTheHorse
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        private static int[][] field;
        private static int rows;
        private static int cols;
        private static readonly int[] neighbouringC = {  1,  2, 2, 1, -1, -2, -2, -1 };
        private static readonly int[] neighbouringR = { -2, -1, 1, 2,  2,  1, -1, -2 };

        static void Main(string[] args)
        {
            rows = int.Parse(Console.ReadLine());
            cols = int.Parse(Console.ReadLine());
            field = new int[rows][];
            for (int r = 0; r < rows; r++)
            {
                field[r] = new int[cols];
            }

            int startRow = int.Parse(Console.ReadLine());
            int startCol = int.Parse(Console.ReadLine());

            BFS(startCol, startRow);

            for (int r = 0; r < rows; r++)
            {
                Console.WriteLine(field[r][cols / 2]);
            }
        }

        private static void BFS(int startC, int startR)
        {
            Queue<Tuple<int, int>> queue = new Queue<Tuple<int, int>>();
            queue.Enqueue(new Tuple<int, int>(startR, startC));
            field[startR][startC] += 1;

            while (queue.Count > 0)
            {
                Tuple<int, int> currCoords = queue.Dequeue();
                int currR = currCoords.Item1;
                int currC = currCoords.Item2;
                int currVal = field[currR][currC];
                for (int i = 0; i < 8; i++)
                {
                    int r = currCoords.Item1 + neighbouringR[i];
                    int c = currCoords.Item2 + neighbouringC[i];
                    if (c < cols && c >= 0 && r < rows && r >= 0 && field[r][c] == 0)
                    {
                        Tuple<int, int> horseCoords = new Tuple<int, int>(r, c);
                        queue.Enqueue(horseCoords);
                        field[r][c] = currVal + 1;
                    }
                }

//                PrintMatrix();
            }
        }

        private static void PrintMatrix()
        {
            for (int r = 0; r < rows; r++)
            {
                Console.WriteLine(string.Join(" ", field[r]));
            }

            Console.WriteLine();
        }
    }
}
