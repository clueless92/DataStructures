using System;
using System.Collections.Generic;

namespace BacktrackingQueens
{
    class BacktrackingQueens
    {
        static int solutionsFound = 0;
        const int SIZE = 8;
        static bool[,] chessboard = new bool[SIZE, SIZE];
        //static HashSet<int> attackedRows = new HashSet<int>();
        static HashSet<int> attackedCols = new HashSet<int>();
        static HashSet<int> attackedLeftDiags = new HashSet<int>();
        static HashSet<int> attackedRightDiags = new HashSet<int>();

        static void PutQueens(int row)
        {
            if (row == 8)
            {
                PrintSolution();
            }
            else
            {
                for (int col = 0; col < 8; col++)
                {
                    if (CanPlaceQueen(row, col))
                    {
                        MarkAllAttackedPositions(row, col);
                        PutQueens(row + 1);
                        UnmarkAllAttackedPostitions(row, col);
                    }
                }
            }
        }

        static void PrintSolution()
        {
            for (int row = 0; row < SIZE; row++)
            {
                for (int col = 0; col < SIZE; col++)
                {
                    if (chessboard[row, col])
                    {
                        Console.Write("# ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            solutionsFound++;
        }
        static void Main(string[] args)
        {
            PutQueens(0);
        }

        static bool CanPlaceQueen(int row, int col)
        {
            int leftDiag = col - row;
            int rightDiag = col + row;
            bool positionOccupied = /* attackedRows.Contains(row) || */ attackedCols.Contains(col) 
                || attackedLeftDiags.Contains(leftDiag) || attackedRightDiags.Contains(rightDiag);
            return !positionOccupied;
        }

        static void MarkAllAttackedPositions(int row, int col)
        {
            int leftDiag = col - row;
            int rightDiag = col + row;
            //attackedRows.Add(row);
            attackedCols.Add(col);
            attackedLeftDiags.Add(leftDiag);
            attackedRightDiags.Add(rightDiag);
            chessboard[row, col] = true;
        }

        static void UnmarkAllAttackedPostitions(int row, int col)
        {
            int leftDiag = col - row;
            int rightDiag = col + row;
            //attackedRows.Remove(row);
            attackedCols.Remove(col);
            attackedLeftDiags.Remove(leftDiag);
            attackedRightDiags.Remove(rightDiag);
            chessboard[row, col] = false;
        }
    }
}
