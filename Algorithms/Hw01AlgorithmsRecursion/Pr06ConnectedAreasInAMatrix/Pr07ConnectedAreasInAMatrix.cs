namespace Pr06ConnectedAreasInAMatrix
{
    using System;
    using System.Collections.Generic;

    class Pr07ConnectedAreasInAMatrix
    {
//        private static readonly char[,] matrix = 
//        {
//            {' ', ' ', ' ', '*', ' ', ' ', ' ', '*', ' '},
//            {' ', ' ', ' ', '*', ' ', ' ', ' ', '*', ' '},
//            {' ', ' ', ' ', '*', ' ', ' ', ' ', '*', ' '},
//            {' ', ' ', ' ', ' ', '*', ' ', '*', ' ', ' '},
//        };

        private static readonly char[,] matrix = 
        {
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' '},
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' '},
            {'*', ' ', ' ', '*', '*', '*', '*', '*', ' ', ' '},
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' '},
            {'*', ' ', ' ', '*', ' ', ' ', ' ', '*', ' ', ' '},
        };

        private static readonly int[] modR = {1, -1, 0, 0};
        private static readonly int[] modC = {0, 0, 1, -1};
        private static int areaSize = 0;

        private static void Main(string[] args)
        {
            char fill = 'a';
            List<Area> areaList = new List<Area>();
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    if (matrix[r, c] != ' ')
                    {
                        continue;
                    }

                    FindAreas(r, c, fill);
                    fill++;
                    if (areaSize != 0)
                    {
                        areaList.Add(new Area(areaSize, r, c));
                        areaSize = 0;
                    }
                }
            }

            Console.WriteLine("Total areas found: {0}", areaList.Count);
            areaList.Sort();
            for (int i = 0; i < areaList.Count; i++)
            {
                Console.WriteLine("Area #{0} at ({1}, {2}), size: {3}",
                    i + 1, areaList[i].Row, areaList[i].Col, areaList[i].Size);
            }

            PrintMatrix();
        }

        private static void PrintMatrix()
        {
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    Console.Write("{0} ", matrix[r, c]);
                }
                Console.WriteLine();
            }
        }

        private static void FindAreas(int r, int c, char fill)
        {
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            stack.Push(new Tuple<int, int>(r, c));
            while (stack.Count > 0)
            {
                int currRow = stack.Peek().Item1;
                int currCol = stack.Pop().Item2;
                if (currCol < 0 ||
                    currRow < 0 ||
                    currRow >= matrix.GetLength(0) ||
                    currCol >= matrix.GetLength(1) ||
                    matrix[currRow, currCol] != ' ')
                {
                    continue;
                }

                matrix[currRow, currCol] = fill;
                for (int i = 0; i < modR.Length; i++)
                {
                    int row = currRow + modR[i];
                    int col = currCol + modC[i];
                    stack.Push(new Tuple<int, int>(row, col));
                }

                areaSize++;
            }
        }
    }

    internal class Area : IComparable<Area>
    {
        public Area(int size, int row, int col)
        {
            this.Size = size;
            this.Row = row;
            this.Col = col;
        }

        public int Size { get; set; }

        public int Row { get; set; }

        public int Col { get; set; }

        public int CompareTo(Area other)
        {
            int result = other.Size.CompareTo(this.Size);
            if (result == 0)
            {
                result = this.Row.CompareTo(other.Row);
                if (result == 0)
                {
                    result = this.Col.CompareTo(other.Col);
                }
            }

            return result;
        }
    }
}
