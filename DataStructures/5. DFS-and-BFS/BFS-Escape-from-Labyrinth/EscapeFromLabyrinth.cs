namespace Escape_from_Labyrinth
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class EscapeFromLabyrinth
    {
        class Point
        {
            public Point(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public string Direction { get; set; }
            public Point PrevPoint { get; set; }
        }

        const char VisitedCell = 's';

        private static int width = 9;
        private static int height = 7;

        private static char[][] labyrinth;

        public static void Main()
        {
            ReadLabyrinth();
            string shortestPathToExit = FindShortestPathToExit();
            if (shortestPathToExit == null)
            {
                Console.WriteLine("No exit!");
            }
            else if(shortestPathToExit == "")
            {
                Console.WriteLine("Start is at the exit.");
            }
            else
            {
                Console.WriteLine("Shortest exit: {0}", shortestPathToExit);
            }
        }

        private static void ReadLabyrinth()
        {
            width = int.Parse(Console.ReadLine());
            height = int.Parse(Console.ReadLine());
            labyrinth = new char[height][];
            for (int i = 0; i < height; i++)
            {
                labyrinth[i] = Console.ReadLine().ToCharArray();
            }
        }

        private static string FindShortestPathToExit()
        {
            Queue<Point> queue = new Queue<Point>();
            Point startPosition = FindStartPosition();
            if (startPosition == null)
            {
                // No start position -> no exit
                return null;
            }

            queue.Enqueue(startPosition);
            while (queue.Count > 0)
            {
                Point currentCell = queue.Dequeue();
                //Console.WriteLine("Visited cell: ({0}, {1})", currentCell.X, currentCell.Y);
                if (IsExit(currentCell))
                {
                    return TracePathBack(currentCell);
                }

                TryDirection(queue, currentCell, "U", 0, -1);
                TryDirection(queue, currentCell, "R", +1, 0);
                TryDirection(queue, currentCell, "D", 0, +1);
                TryDirection(queue, currentCell, "L", -1, 0);
            }

            return null;
        }

        private static string TracePathBack(Point currentCell)
        {
            StringBuilder pathBuilder = new StringBuilder();
            while (currentCell.PrevPoint != null)
            {
                pathBuilder.Append(currentCell.Direction);
                currentCell = currentCell.PrevPoint;
            }

            StringBuilder reversedPath = new StringBuilder(pathBuilder.Length);
            for (int i = pathBuilder.Length - 1; i >= 0; i--)
            {
                reversedPath.Append(pathBuilder[i]);
            }

            return reversedPath.ToString();
        }

        private static void TryDirection
            (Queue<Point> queue, Point currentCell, string direction, int deltaX, int deltaY)
        {
            int newX = currentCell.X + deltaX;
            int newY = currentCell.Y + deltaY;
            if (newX >= 0 && newX < width && newY >= 0 && newY < height && labyrinth[newY][newX] == '-')
            {
                labyrinth[newY][newX] = VisitedCell;
                Point nextCell = new Point(newX, newY)
                {
                    Direction = direction,
                    PrevPoint = currentCell
                };
                queue.Enqueue(nextCell);
            }
        }

        private static bool IsExit(Point currentCell)
        {
            bool isOnBorderX = currentCell.X == 0 || currentCell.X == width - 1;
            bool isOnBorderY = currentCell.Y == 0 || currentCell.Y == height - 1;
            return isOnBorderX || isOnBorderY;
        }

        private static Point FindStartPosition()
        {
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    if (labyrinth[h][w] == VisitedCell)
                    {
                        return new Point(w, h);
                    }
                }
            }

            return null;
        }
    }
}
